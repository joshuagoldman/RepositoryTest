module CheckBandAvailibility

open System.IO
open System.Xml.Linq
open System.Xml.XPath
open System
open System.Text.RegularExpressions
open System.Linq

let BandParamStream = File.Open("C:\Users\jogo\Gitrepos\Nodetest\Datapackets\BandParameters\documents\BandParameters.xml", FileMode.OpenOrCreate)
let xDocBandParam = XDocument.Load(BandParamStream)
BandParamStream.Close()


let currCases = "KRC161781/1;	Radio 4417 ;B3'
                 KRC161781/2;	Radio 4417 ;B3'
                 KRC161779/1;	AIR 4455 ;B1 ,B3'
                 KRC161740/1;	Radio 4402; B3 '
                 KRC161737/1;	Radio 4402; B2,B25'
                 KRC161741/1;	Radio 4402; B7'
                 KRC161767/1;	Radio 4408; B42'
                 KRC161739/1;	Radio 4402; B1'
                 KRC161816/1;	AIR 4455 ;B2,B25, B66A'
                 KRC161838/1;	Radio 2203; B14'
                 KRD901160/11;	AIR 6488; B48'
                 KRD901145/1;	AIR 4455; B2, B25, B66A"


let bandsCurrCase = 
    currCases
    |> fun str -> str.Split '''
                  |> Array.map (fun subStr -> subStr.Split ';')
    |> Array.map (fun arr -> arr |> Array.item 2 )
    |> Array.collect(fun arr -> arr.Split ','
                                |> Array.map (fun str -> str.Trim()))
    |> Array.toSeq

let seqOfBands = 
    let allElements = xDocBandParam.XPathSelectElements("*//*")
    let allNames = allElements
                   |> Seq.filter (fun xEl -> xEl.Name.LocalName = "BandParams")
    bandsCurrCase
    |> Seq.filter (fun band -> band
                               |> fun _ -> allNames
                                           |> Seq.forall (fun bandEl -> band <> bandEl.FirstAttribute.Value))
    |> function
        | res when res |> Seq.length = 0 ->
            "No new bands found"
        | res -> String.Join("kkk", (res|> Seq.map (fun str -> str + ", "))).Replace("kkk", "")
                 |> fun str -> str.Substring(0, str.LastIndexOf(','))

seqOfBands |> ignore


