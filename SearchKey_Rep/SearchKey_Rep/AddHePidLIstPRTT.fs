module AddHePidListPRTT

open System.IO
open System.Xml.Linq
open System.Xml.XPath
open System
open System.Text.RegularExpressions
open System.Linq
open SearchKeyRep.AddConfigKeyDefinitions


let prods2Add = File.ReadAllText("C:\\Users\\DELL\\Documents\\Ericsson\\products2Add.txt")
//let HwPidStream = File.Open("C:\Users\jogo\Gitrepos\Nodetest\Datapackets\HWPidList\documents\HWPidList.xml", FileMode.OpenOrCreate)
//let mutable xDoc = XDocument.Load(HwPidStream)
//HwPidStream.Close() 

type ProductNumber =
    {
        Value : string
    }

type PortInfo = 
    {
        Band : string
        Power : string
        FrequencyWidth : string
    }

type PortLetter = 
    {
        Value : string
    }

type Port =
    {
        PortSeq : seq<PortInfo>
        Letter : PortLetter
    }

type HwPidListPRTTBase =
    {   
        Number : ProductNumber
        Name : string
        MarketName : string
        PortSeq : seq<Port>
    }

type Tags =
    {
        PrttSupp : {| Name : string ; Value : string|}
        LatSupp : {| Name : string ; Value : string|}
        LatCat : {| Name : string ; Value : string|}
    }

type FullElementType =
    {
        Base : HwPidListPRTTBase
        Tag : Tags
    }

type HWPidListLAT =
    | Base of HwPidListPRTTBase
    | FullElement of FullElementType
    | ProcedureDone of unit

let getPortSequence (infoArr : string[]) =
    
    let numOfPorts = 
        Regex.Match(infoArr.[1].Trim(), "[1-9]\d")
        |> function
            | res when not(res.Success) -> 0
            | res -> res.Value.Substring(0,1) |> int

    let powers =
        infoArr.[3].Split ','

    let freqWidth = 
        infoArr.[4]

    let portBaseInfos = 
        let bands = infoArr.[2].Split ','
        Array.zip bands [|0..bands.Length - 1|]
        |> Array.map (fun (band, pos) -> { Band = band ;
                                           Power = powers.[pos] ;
                                           FrequencyWidth = freqWidth })

    seq[1..numOfPorts]
    |> Seq.map (fun pairNum -> portBaseInfos
                               |> fun portBaseInfo ->
                                    { PortSeq = portBaseInfo ;
                                      Letter = { Value = int2Alphabet pairNum} })


let getFullElement =

    prods2Add
    |> fun str -> str.Split '''
                  |> Array.map (fun subStr -> subStr.Trim().Split ';'
                                              |> fun infoArr -> 
                                                    { Number = {Value = infoArr.[0].Trim()} ;
                                                      Name = infoArr.[1].Trim() ;
                                                      MarketName = "" ;
                                                      PortSeq = getPortSequence infoArr })


let getTags =
    let prttSuppTag = {|Name = "prtt-supported" ; Value = "Yes"|}
    let latSuppTag = {|Name = "lat-supported" ; Value = "Yes"|}
    let categoryTag = {|Name = "lat-category" ; Value = "radio"|}
    {PrttSupp = prttSuppTag ; LatSupp = latSuppTag ; LatCat = categoryTag }

let makePortSeqToString (portInfos : seq<PortInfo>) =
    portInfos
    |> Seq.map (fun band -> band.Band + ";" +
                            band.Power + ";" +
                            band.FrequencyWidth + ",")
    |> fun strSeq -> String.Join("kkk", strSeq).Replace("kkk", "")
    |> fun str -> str.Substring(0, str.LastIndexOf(','))

let getPortAttribs (portSeq : seq<Port>) =
    let portInfoAttrValue =
        makePortSeqToString (portSeq
                             |> Seq.item 0
                             |> fun x -> x.PortSeq)
    portSeq
    |> Seq.map (fun port -> new XAttribute(XName.Get port.Letter.Value,
                                                     portInfoAttrValue))

let getProdAttribs (fullElement : FullElementType) =
    
    let firstSeq =
        seq[new XAttribute(XName.Get "Number", fullElement.Base.Number.Value) ;
        new XAttribute(XName.Get "Name", fullElement.Base.Name) ;
        new XAttribute(XName.Get "MarketName", fullElement.Base.MarketName) ;
        new XAttribute(XName.Get "RadioTestAllowed", "YES") ;
        new XAttribute(XName.Get "RequiresRadioTest", "YES")]
    
    let secSeq = getPortAttribs fullElement.Base.PortSeq

    Seq.append firstSeq secSeq

let createTagElement (tags : Tags) = 

    new XElement(XName.Get "Tags",
        new XElement(XName.Get "Tag",
                        XAttribute(XName.Get "Name", tags.PrttSupp.Name),
                        XAttribute(XName.Get "Value", tags.PrttSupp.Value)),
        new XElement(XName.Get "Tag",
                        XAttribute(XName.Get "Name", tags.LatSupp.Name),
                        XAttribute(XName.Get "Value", tags.LatSupp.Value)),
        new XElement(XName.Get "Tag",
                        XAttribute(XName.Get "Name", tags.LatCat.Name),
                        XAttribute(XName.Get "Value", tags.LatCat.Value)))

let getXmlTree (fullElement : FullElementType) = 
    let tree = 
        new XElement(XName.Get "Product", getProdAttribs fullElement,
            createTagElement fullElement.Tag)

    tree
                        

let rec msgFunc (state : HWPidListLAT) =
    match state with
    | Base(hwPidBase) ->
        let fullElementType = {Base = hwPidBase ; Tag = getTags }
        FullElement(fullElementType )

    | FullElement(fullElementType) -> 
        let finalTree = getXmlTree fullElementType
        File.AppendAllText("C:\Users\DELL\Documents\Ericsson\Test.txt",finalTree.ToString())
        ProcedureDone()

    | ProcedureDone() ->
        ProcedureDone()

    |> function
       | postState when postState = ProcedureDone() -> ignore
       | postState -> msgFunc postState 
   
getFullElement
|> Seq.map (fun case -> Base(case))
|> Seq.iter (fun case -> msgFunc case |> ignore)

