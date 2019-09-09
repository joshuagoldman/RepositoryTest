namespace SearchKeyRep

open SearchKeyRep.Methods.SK_1_68_Methods


module SK_1_72_Rule1 =
    
    let size = [|1..4|].Length
    let span = [|1..4|]

    let varBuilding (number : int) = 

        let X2 = 
            [1..size]
            |> Seq.filter(fun num -> num <> number)
            |> Seq.map(fun num -> num.ToString() + "|")
            |> String.concat ","
            |> fun str -> str.Replace(",", "")
            |> fun str -> str.Substring(0, str.LastIndexOf("|"))
            |> fun str -> "(\(ALE\)|\(ALF\)|\(ALG\))  EMCA[" + str + "]"

        let X1 = 
            
            "(\(ALE\)|\(ALF\)|\(ALG\))  EMCA" + number.ToString()

        let finalStr = 
            
            "NameNEXTX1NEXTValueNEXT" + X1 + 
            "NEXTIsRegexNEXTTRUE\n" +
            "NameNEXTX2NEXTValueNEXT" + X2 + 
            "NEXTIsRegexNEXTTRUE"
     
        finalStr
    
    let varRows = 
        
        let varSeq = 
            span
            |> Array.map(fun number -> number |> varBuilding)

        varSeq

    let searchKeys = 
        span
        |> Array.map(fun number -> "LAT 1/-71, Rule 1, EMCA" + number.ToString() + " fault, Rev A")

    let filters = 
        
        span
        |> Array.map(fun _ -> "")

    let dates = 
        span
        |> Array.map(fun _ -> "2019-14-08")

    let infoText = 
        
        span
        |> Array.map(fun number -> "HW Fault indicated.\n" + 
                                   "Replace BBM/Trinity at position D1000T" + number.ToString() + ". Report as A105/59. Major Fault.")


    let products =
        
        span
        |> Array.map(fun _ -> "ProductNumberNEXTKDU137925/31" +
                               "NEXTRStateNEXT*\n" +
                               "ProductNumberNEXTKDU137925/41" +
                               "NEXTRStateNEXT*")

    let expression = 
        
        [|1..4|]
        |> Array.map(fun _ -> "X1 > 0 and X2 = 0")

    let finalArr_1_72_Rule1 = getSearchKeysAll [|searchKeys;
                              varRows;
                              filters;
                              dates;
                              infoText;
                              products;
                              expression;
                              infoText
                              |]

