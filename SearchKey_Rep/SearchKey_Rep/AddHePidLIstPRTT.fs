module AddHePidListPRTT

open System.IO
open System.Xml.Linq
open System.Xml.XPath
open System
open System.Text.RegularExpressions
open System.Linq
open SearchKeyRep.AddConfigKeyDefinitions


let prods2Add = File.ReadAllText("C:\Users\jogo\Documents\jogo\Ericsson\products2Add.txt")
let HwPidStream = File.Open("C:\Users\jogo\Gitrepos\Nodetest\Datapackets\HWPidList\documents\HWPidList.xml", FileMode.OpenOrCreate)
let mutable xDoc = XDocument.Load(HwPidStream)
HwPidStream.Close() 

type ProductNumber =
    {
        Value : string
    }

type PortBaseInfo = 
    {
        Band : string
        Power : string
        FrequencyWidth : string
    }

type PortLetter = 
    {
        Value : string
    }
    
type PortInfo = 
    {

        BaseInfo : seq<PortBaseInfo>
        Letter : PortLetter
    }

type Ports =
    {
        First : seq<PortInfo>
        Second : seq<PortInfo>
    }

type HwPidListPRTTBase =
    {   
        Number : ProductNumber
        Name : string
        MarketName : string
        PortSeq : seq<Ports>
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


let getPortLetter (aOrB : int) (pairNumber : int) =
    let letterNumber = aOrB + pairNumber

    int2Alphabet letterNumber

let getPortSequence (infoArr : string[]) =
    
    let numOfPorts = 
        Regex.Match(infoArr.[1], "(?<= ).*")
        |> function
        | res when not(Regex.Match(res.Value , "[1-9]\d*)").Success) -> 0
        | res -> res.Value.[0] |> int

    let numOfPortPairs =
        round(float(numOfPorts) * 0.5)
        |> int

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

    seq[0..numOfPortPairs - 1]
    |> Seq.map (fun pairNum -> seq[0..1]
                               |> Seq.map (fun portNum -> portBaseInfos
                                                          |> fun portBaseInfo ->
                                                                { BaseInfo = portBaseInfo |> Array.toSeq ;
                                                                  Letter = { Value = getPortLetter pairNum portNum } 
                                                                  }) 
                                |> fun firstsSecondsArr -> 
                                    {
                                        First = firstsSecondsArr  ;
                                        Second = firstsSecondsArr
                                    })


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

let getPortAttribs (portSeq : seq<Ports>) =
    portSeq
    |> Seq.map (fun port -> new XAttribute(XName.Get port.First))

let getProdAttribs (fullElement : FullElementType) =
    new XAttribute(XName.Get "Number", fullElement.Base.Number),
    new XAttribute(XName.Get "Name", fullElement.Base.Name),
    new XAttribute(XName.Get "MarketName", fullElement.Base.MarketName),
    new XAttribute(XName.Get "RadioTestAllowed", "YES"),
    new XAttribute(XName.Get "RequiresRadioTest", "YES"),



let getXmlTree (fullElement : FullElementType) = 
    let tree = 
        new XElement(XName.Get "Product", )

let rec msgFunc (state : HWPidListLAT) =
    match state with
    | Base(hwPidBase) ->
        let fullElementType = {Base = hwPidBase ; Tag = getTags }
        FullElement(fullElementType )

    | FullElement(fullElementType) -> 
        getXmlTree fullElementType
        ProcedureDone()

    | ProcedureDone() ->
        ProcedureDone()

    |> function
       | postState when postState = ProcedureDone() -> ignore
       | postState -> msgFunc postState 
   
getFullElement
|> Seq.map (fun case -> Base(case))
|> Seq.iter (fun case -> msgFunc case |> ignore)

