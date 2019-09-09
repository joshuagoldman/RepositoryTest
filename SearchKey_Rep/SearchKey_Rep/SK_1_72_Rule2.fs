namespace SearchKeyRep

open SearchKeyRep.Methods.SK_1_68_Methods


module SK_1_72_Rule2 =
    
    let size = [|1..4|].Length

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
            
            "NameNEXTX" + ((number - 1) * 2 + 1).ToString() +  "NEXTValueNEXT" + X1 + 
            "NEXTIsRegexNEXTTRUE\n" +
            "NameNEXTX" + ((number - 1) * 2 + 2).ToString() + "NEXTValueNEXT" + X2 + 
            "NEXTIsRegexNEXTTRUE\n"
     
        finalStr
    
    let varRows = 
        
        let varSeq = 
            [|1..4|]
            |> Seq.map(fun number -> number |> varBuilding)
            |> String.concat ","
            |> fun str  -> str.Replace(",","")
            |> fun str -> str.Substring(0, str.LastIndexOf("\n"))
            |> fun str -> [|str|]

        varSeq

    let searchKeys = 
        
        [|"LAT 1/-71, Rule 2, EMCA fault, Rev A"|]

    let filters = [|""|]

    let dates = [|"2019-13-08"|]

    let infoText = [|
                        "HW Fault indicated.\n" +
                        "Follow instructions in document: EEE 1/1547-KDU 137 925/31+ (Troubleshooting instruction for Baseband 5212/5216).\n" + 
                        "See chapter 5.5.2. EMCA faults"|]

    let products = [|"ProductNumberNEXTKDU137925/31" +
                     "NEXTRStateNEXT*\n" +
                     "ProductNumberNEXTKDU137925/41" +
                     "NEXTRStateNEXT*"|]

    let expression = 
        
        [|1..2..size * 2|]
        |> Array.map(fun number -> "(X" + number.ToString() + " > 0 and X" + (number + 1).ToString() + " > 0) or ")
        |> String.concat ","
        |> fun str  -> str.Replace(",","")
        |> fun str -> str.Substring(0, str.LastIndexOf(" or"))
        |> fun str -> [|str|]


    let finalArr_1_72_Rule2 = getSearchKeysAll [|searchKeys;
                              varRows;
                              filters;
                              dates;
                              infoText;
                              products;
                              expression;
                              infoText
                              |]

