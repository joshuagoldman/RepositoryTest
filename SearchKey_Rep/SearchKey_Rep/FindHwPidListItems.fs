module FindHwPidListItems

open System.IO
open System.Xml.Linq
open System.Xml.XPath
open System
open System.Text.RegularExpressions
open System.Linq

type HwPidListItems =
    {   
        ProdNumber : string
        Name : string
        Power : string
        FrequenceWidth : string
    }

let infoFile = File.ReadAllText("C:\Users\jogo\Documents\jogo\Ericsson\Radio_Units_Info.txt")
let HwPidStream = File.Open("C:\Users\jogo\Gitrepos\Nodetest\Datapackets\HWPidList\documents\HWPidList.xml", FileMode.OpenOrCreate)
let mutable xDoc = XDocument.Load(HwPidStream)
HwPidStream.Close()

let findRightFreqWidth ( widthsStr : string )  =
    let widthsCrosses = widthsStr.Split ','

    let widths = [|5 ; 10 ; 20 ; 25|]

    let anonRecSeq =
        Array.zip widthsCrosses [|0..widthsCrosses.Length - 1|]
        |> Array.map (fun (value, pos) -> {| Cross = value ;
                                             Value = widths.[pos]|})

    anonRecSeq
    |> Array.filter (fun anonRec -> anonRec.Cross <> "")
    |> fun sequence -> if sequence.Length <> 0
                       then sequence.[0].Value |> string
                       else ""
    

let infoRecSeq = 
    infoFile
    |> fun str -> str.Split '''
    |> Seq.map (fun str -> str.Split ';')
    |> Seq.map (fun infoSeq ->  infoSeq
                                    |> function
                                        | _ when infoSeq.Length >= 3 ->
                                    
                                            { ProdNumber = infoSeq.[2] |> fun str -> str.Trim() ;
                                              Name = infoSeq.[1] |> fun str -> str.Trim().Replace(" ","") ;
                                              Power = infoSeq.[3] |> fun str -> str.Trim().Replace(" ","") ;
                                              FrequenceWidth = findRightFreqWidth (infoSeq.[4]
                                                                                   |> fun str -> 
                                                                                    str.Trim().Replace(" ","")) }

                                        | _ -> 
                                            { ProdNumber = "" ;
                                              Name = "" ;
                                              Power = "" ;
                                              FrequenceWidth = "" })

let getPower (case : HwPidListItems) =
    let dataFoundThroughNumber =
        infoRecSeq
        |> Seq.tryFind (fun data -> data.ProdNumber = case.ProdNumber)

    let dataFoundThroughName =
        infoRecSeq
        |> Seq.tryFind (fun data -> data.Name = case.Name)
    
    None
    |>function
     | _ when dataFoundThroughNumber <> None -> 
            
            {case with Power = dataFoundThroughNumber.Value.ProdNumber}
     
     | _ when dataFoundThroughName <> None &&  
              dataFoundThroughNumber = None ->
             
             { case with Power = dataFoundThroughName.Value.Power}
     
     | _ -> case
    
let getFreqWidth (case : HwPidListItems) =
    let dataFoundThroughNumber =
        infoRecSeq
        |> Seq.tryFind (fun data -> data.ProdNumber = case.ProdNumber)

    let dataFoundThroughName =
        infoRecSeq
        |> Seq.tryFind (fun data -> data.Name = case.Name)
    
    None
    |>function
     | _ when dataFoundThroughNumber <> None -> 
            
            {case with Power = dataFoundThroughNumber.Value.ProdNumber}
     
     | _ when dataFoundThroughName <> None &&  
              dataFoundThroughNumber = None ->
             
             { case with Power = dataFoundThroughName.Value.Power}
     
     | _ -> case