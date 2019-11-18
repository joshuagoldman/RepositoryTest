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

    let widths = [| 1.4 ; 3.0 ; 4.2 ; 4.4 ; 4.6 ;
                    4.8 ; 5.0 ; 9.0 ; 10.0 ; 14.8 ;
                    15.0 ; 20.0 ; 5.0 ; 10.0 ; 15.0 ;
                    20.0 ; 25.0 ; 30.0 ; 40.0 ; 50.0 ;
                    60.0 ; 70.0 ; 80.0 ; 90.0 ; 100.0 ;
                    200.0 ; 400.0|]

    let anonRecSeq =
        Array.zip widthsCrosses [|0..widthsCrosses.Length - 1|]
        |> Array.map (fun (value, pos) -> {| Cross = value ;
                                             Value = widths.[pos]|})
    
    let foundSeq = 
       anonRecSeq
       |> Array.filter (fun anonRec -> anonRec.Cross <> "")

    let result =
        foundSeq
        |> fun sequence -> if sequence.Length <> 0
                           then sequence.[0].Value |> string
                           else ""

    result
    

let infoRecSeq = 
    infoFile
    |> fun str -> str.Split '''
    |> Seq.map (fun str -> str.Split ';')
    |> Seq.map (fun infoSeq ->  infoSeq
                                    |> function
                                        | _ when infoSeq.Length >= 3 ->
                                    
                                            { ProdNumber = infoSeq.[2] |> fun str -> str.Trim().Replace(" ","") ;
                                              Name = infoSeq.[0] |> fun str -> str.Trim().Replace(" ","") ;
                                              Power = infoSeq.[4] |> fun str -> str.Trim().Replace(" ","") ;
                                              FrequenceWidth = findRightFreqWidth (infoSeq.[3]
                                                                                   |> fun str -> 
                                                                                    str.Trim().Replace(" ",""))}

                                        | _ -> 
                                            { ProdNumber = "" ;
                                              Name = "" ;
                                              Power = "" ;
                                              FrequenceWidth = ""})

let getPower (case : HwPidListItems) (bands : string) =
    
    let mutable ProdName = case.Name 
    bands.Split ','
    |> Array.map (fun band -> band.Trim())
    |> Array.iter (fun band -> ProdName <- ProdName.Replace(band, ""))

    let sss = case
    let dataFoundThroughNumber =
        infoRecSeq
        |> Seq.tryFind (fun data -> data.ProdNumber.Replace(" ", "") = case.ProdNumber.Replace(" ", "").Trim())

    let dataFoundThroughName =
        infoRecSeq
        |> Seq.tryFind (fun data -> ProdName = data.Name)
    
    None
    |>function
     | _ when dataFoundThroughNumber <> None -> 
            
            {case with Power = dataFoundThroughNumber.Value.Power}
     
     | _ when dataFoundThroughName <> None &&  
              dataFoundThroughNumber = None ->
             
             { case with Power = dataFoundThroughName.Value.Power}
     
     | _ -> case
    
let getFreqWidth (case : HwPidListItems) (bands : string) =
    
    let mutable ProdName = case.Name 
    bands.Split ','
    |> Array.map (fun band -> band.Trim())
    |> Array.iter (fun band -> ProdName <- ProdName.Replace(band, ""))

    let sss = case
    let dataFoundThroughNumber =
        infoRecSeq
        |> Seq.tryFind (fun data -> data.ProdNumber.Replace(" ", "").Trim() = case.ProdNumber.Replace(" ", "").Trim())

    let dataFoundThroughName =
        infoRecSeq
        |> Seq.tryFind (fun data -> data.Name = ProdName)
    
    None
    |>function
     | _ when dataFoundThroughNumber <> None -> 
            
            {case with FrequenceWidth = dataFoundThroughNumber.Value.FrequenceWidth}
     
     | _ when dataFoundThroughName <> None &&  
              dataFoundThroughNumber = None ->
             
             { case with FrequenceWidth = dataFoundThroughName.Value.FrequenceWidth}
     
     | _ -> case