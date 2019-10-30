namespace SearchKeyRep

module AddConfigKeyDefinitions =

    open System.IO
    open System.Xml.Linq
    open System.Xml.XPath
    open System
    open System.Text.RegularExpressions
    open System.Linq


    type ConfigKeyInfo =
        {
          Data : string
          Band : string
          Label : string
          ProductType : string
          ProductName : string
          Description : string
        }

    type configKeyInfoWithLetter =
        {
            Info : ConfigKeyInfo
            Letter : string
        }

    let int2Alphabet (num : int) = 
    
        let numFloat = float(num) - 1.0
        let factor = Operators.floor (numFloat/26.0)
    
        let newNum =  int(numFloat - factor * 26.0)  

        match newNum with
        | 0 -> "a" | 1 -> "b" | 2 -> "c" | 3 -> "d" | 4 -> "e"  | 5 -> "f" | 6 -> "g" | 7 -> "h"
        | 8 -> "i" | 9 -> "j" | 10 -> "k" | 11 -> "l" | 12 -> "m" | 13 -> "n" | 14 -> "o" | 15 -> "p"
        | 16 -> "q" | 17 -> "r" | 18 -> "s" | 19 -> "t" | 20 -> "u" | 21 -> "v" | 22 -> "w" | 23 -> "x"
        | 24 -> "y" | 25 -> "z" | _ -> ""

    let caseString =
        "KRC161781/1	;Radio 4417 ;B3'
        KRC161781/2	;Radio 4417 ;B3'
        KRC161779/1	;AIR 4455 ;B1 ,B3'
        KRC161740/1	;Radio 4402 ;B3 '
        KRC161737/1	;Radio 4402 ;B2,B25'
        KRC161741/1	;Radio 4402 ;B7'
        KRC161767/1	;Radio 4408 ;B42'
        KRC161739/1	;Radio 4402 ;B1'
        KRC161816/1	;AIR 4455 ;B2,B25, B66A'
        KRC161838/1	;Radio 2203 ;B14'
        KRD901160/11;	AIR 6488 ;B48'
        KRD901145/1	;AIR 4455 ;B2,B25 ,B66A"


    let createBands (str : string) =
        str.Split ','
        |> Array.map (fun band -> band.Trim() + ";")
        |> fun bands -> String.Join("kkk", bands).Replace("kkk","")
                        |> fun allBandsJoined -> allBandsJoined.Substring(0, allBandsJoined.LastIndexOf(';'))



    let elInfo =
        caseString
        |> fun str -> str.Split '''
                      |> Array.map (fun subStr -> subStr.Trim().Split ';'
                                                  |> fun infoArr -> 
                                                        { Data = infoArr.[0].Trim() ;
                                                          Band = createBands (infoArr.[2].Trim()) ;
                                                          Label = infoArr.[1].Trim() ;
                                                          ProductType = "Rrus" ;
                                                          ProductName = "" ; 
                                                          Description = ""})

    let xmlTree =
        let mutable pos = 20
        elInfo
        |> Array.map (fun configKeyInfo -> (pos <- pos + 1)
                                           { Info = configKeyInfo ; Letter = (int2Alphabet pos).ToUpper()})
        |> Array.map(fun xmlInfo -> new XElement(XName.Get "ConfigEntry" ,
                                                    XAttribute(XName.Get "Key", xmlInfo.Letter),
                                                    XAttribute(XName.Get "Data", xmlInfo.Info.Data),
                                                    XAttribute(XName.Get "Band", xmlInfo.Info.Band),
                                                    XAttribute(XName.Get "Label", xmlInfo.Info.Label),
                                                    XAttribute(XName.Get "ProductType", xmlInfo.Info.ProductType),
                                                    XAttribute(XName.Get "ProductName", xmlInfo.Info.ProductName),
                                                    XAttribute(XName.Get "Description", xmlInfo.Info.Description)))
        |> Array.map (fun xmlElement -> xmlElement.ToString() + "\n")
        |> fun bands -> String.Join("kkk", bands).Replace("kkk","")
        |> fun allBandsJoined -> allBandsJoined.Substring(0, allBandsJoined.LastIndexOf('\n')) 

    xmlTree
    |> ignore