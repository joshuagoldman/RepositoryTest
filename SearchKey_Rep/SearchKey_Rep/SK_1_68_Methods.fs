namespace SearchKeyRep.Methods

open System
open System.Windows
open System.Reflection
open System.IO
open System.Diagnostics
open SearchKeyRep.Transpose
open SearchKeyRep
open System.Text.RegularExpressions


module SK_1_68_Methods =

    let DocString = 
        
        let stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("SearchKey_Rep.1_68_Update_C.txt")

        let stream2Read = new StreamReader(stream)

        let fileInStringForm = stream2Read.ReadToEnd().Replace("�","")

        fileInStringForm

    let isVariable (cond : string) =
        
        match cond with
        |"Y" -> Options.X1
        | "N" -> Options.X2
        | _ -> Options.None

    let int2Alphabet (num : int) = 
        
        match num with
        | 0 -> "a" | 1 -> "b" | 2 -> "c" | 3 -> "d" | 4 -> "e"  | 5 -> "f" | 6 -> "g" | 7 -> "h"
        | 8 -> "i" | 9 -> "j" | 10 -> "k" | 11 -> "l" | 12 -> "m" | 13 -> "n" | 14 -> "o" | 15 -> "p"
        | 16 -> "q" | 17 -> "r" | 18 -> "s" | 19 -> "t" | 20 -> "u" | 21 -> "v" | 22 -> "w" | _ -> ""

    type specialVarOption = 
        | Yes
        | No


    let tablePattern =
        
        let alphabet = [|"a";"b";"c";"d";"e";"f";"g";"h";"i";"j";"k";"l";"m";"n";"o";"p";"q";"r";"s";"t";"u";"v";"w";"x";"y";"z"|]

        let pattern = 
        
            alphabet
            |> Array.map(fun letter -> "1" + letter + "\r\n|")
            |> String.concat ","
            |> fun str -> str.Replace(",","")
            |> fun str -> str.Substring(0, str.LastIndexOf("|"))
            |> fun str -> "(" + str + ")"

        pattern

    let createRegexMatchesArr (input : string) (pattern : string) =
        
        let MatchesUnHandled = Regex.Matches(input, pattern)

        [|0..MatchesUnHandled.Count - 1|]
        |> Array.map(fun index -> MatchesUnHandled.[index].Value)

    let newArr4EachSearchKey (arr : string[]) (numOfSearchKeys : int) = 
        
        arr
        |> String.concat ","
        |> fun str -> str.Replace(",", "")
        |> fun str -> str.[0..str.LastIndexOf('\n') - 1]
        |> fun str -> [|1..numOfSearchKeys|]
                      |> Array.map(fun _ -> str)

        
    let createVar (valsZipped : (string * string)[] ) (option : Options) =
        
        valsZipped
        |> Array.filter(fun (cond, _) -> isVariable cond = option)
        |> Array.map(fun (_, var) -> var + "|")
        |> function
            |arr when arr.Length <> 0 -> arr 
                                         |> String.concat ","
                                         |> fun x -> x.[0..x.LastIndexOf('|') - 1]
                                         |> fun x -> "(" + x.Replace(",","") + ")"
            | _ -> ""

    let createVarPair (conds : string []) (vars2Choose : string[]) =
        
        let valsZipped = Array.zip conds vars2Choose
        {X1 = createVar valsZipped Options.X1 ;
         X2 = createVar valsZipped Options.X2}
    
    let createAllVarPairs (info : PLLInfos) (vars2Choose : string[]) =
        
        info.Conditions
        |> Array.map(fun cond -> createVarPair cond vars2Choose)
        
    
    let createSearcheKeyVarStr (varPair : PLLVars) (number : int) = 

        varPair
        |> function 
            | _ when String.IsNullOrEmpty(varPair.X1) = true -> "NameNEXTX" + (number + 1).ToString() + "NEXTValueNEXT" +
                                                                 varPair.X1+
                                                                 "NEXTisRegexNEXTTRUE\n"

            | _ when String.IsNullOrEmpty(varPair.X2) = true -> "NameNEXTX" + (number + 1).ToString() + "NEXTValueNEXT" +
                                                                 varPair.X1 +
                                                                 "NEXTisRegexNEXTTRUE\n"

            | _ when String.IsNullOrEmpty(varPair.X1 + varPair.X2) = false  -> "NameNEXTX" + ((number * 2) + 1).ToString() + "NEXTValueNEXT" +
                                                                                varPair.X1 +
                                                                                "NEXTisRegexNEXTTRUE\n" + 
                                                                                "NameNEXTX" + ((number * 2) + 2).ToString() + "NEXTValueNEXT" +
                                                                                varPair.X2 +
                                                                                "NEXTisRegexNEXTTRUE\n" 

            | _ -> ""

    let filtersFunc (varPair : PLLVars) (number : int) = 
                
        varPair
        |> function
            | var when String.IsNullOrEmpty(var.X1) = true -> "IncludedFilesNEXT" + fileIndexDict.Item(number) + "\n"

            | var when String.IsNullOrEmpty(var.X2) = true -> "IncludedFilesNEXT" + fileIndexDict.Item(number) + "\n"

            | var when String.IsNullOrEmpty(var.X1 + var.X2) = false  -> "IncludedFilesNEXT" + fileIndexDict.Item(number) + "\n" +
                                                                         "IncludedFilesNEXT" + fileIndexDict.Item(number) + "\n"

            | _ -> ""


    let getFilters (tableRowVarsAllFiles : PLLVars[][]) (len : int ) = 

        tableRowVarsAllFiles
        |> fun arr -> SearchKeyRep.Transpose.TransposePLLArr arr 
        |> Array.map(fun sub_arr -> Array.zip sub_arr [|0..len|]
                                    |> Array.map(fun (file_var, number) -> file_var 
                                                                           |> fun var -> filtersFunc var number)
                                    |> String.concat ","
                                    |> fun str -> str.Replace(",", "")
                                    |> fun str -> str.[0..str.LastIndexOf('\n') - 1])

    let getSearchKeys (len : int) (rule : string) = 
        
        [|0..len|]
        |> Array.map(fun num -> "ERS BB units with ICM CCR PLL issue, 1/-68; " + rule + int2Alphabet num + ", Rev D")

    let getDates (len : int) =
        
        [|1..len|]
        |> Array.map(fun _ -> "2019-08-05" )

    let expressionFunc (varPair : PLLVars) (number : int) = 
                
        varPair
        |> function
            | var when String.IsNullOrEmpty(var.X1) = true -> "(X" + ((number) + 1).ToString() + " = 0) or " 

            | var when String.IsNullOrEmpty(var.X2) = true -> "(X" + ((number) + 1).ToString() + " > 0) or "

            | var when String.IsNullOrEmpty(var.X1 + var.X2) = false  -> "(X" + ((number * 2) + 1).ToString() + " > 0" + " and " +
                                                                          "X" + ((number * 2) + 2).ToString() + " = 0) or "
            | _ -> ""

    let expressionFuncSpecial (number : int) = 

        "(X" + ((number * 2) + 1).ToString() + " > 0" + " and " +
        "X" + ((number * 2) + 2).ToString() + " > 0) or "

    let trapInfos = 
        
        let RegexstringChunkReg = Regex.Match(DocString,"(2.4.3.2 Rule 2: Valid when no Trap info available from Step 1)(.|\n)*(2.5 Criteria Valid for)")

        let RegexstringChunk = RegexstringChunkReg.Value

        let tableVals = Regex.Split(RegexstringChunk,"(2).*")

        let conditionsNInfoText = tableVals 
                                |> Array.map(fun str -> Regex.Split(str, "(Y|N|any).*")) 
                                |> Array.filter(fun arr -> arr.Length > 1)
                                |> Array.map(fun arr -> arr
                                                        |> Array.map(fun str -> str.Replace("\n", "").Replace("\r", ""))
                                                        |> Array.filter(fun str -> String.IsNullOrEmpty str <> true))

        let conditions = 
            
            conditionsNInfoText
            |> Array.map(fun arr -> arr
                                    |> Array.filter(fun str -> str.Contains("A1") = false && 
                                                                str.Contains("G1") = false))

        let infoTextComponent = 
            
            conditionsNInfoText
            |> Array.map(fun arr -> arr
                                    |> Array.find(fun str -> str.Contains("A1") = true || 
                                                                str.Contains("G1") = true))
        
        {Conditions = conditions ; InfoTextComponent = infoTextComponent}


    let readyVarRows (tableRowVarsAllFiles : PLLVars[][]) = 
            
        tableRowVarsAllFiles
        |> fun arr -> SearchKeyRep.Transpose.TransposePLLArr arr 
        |> Array.map(fun sub_arr -> Array.zip sub_arr [|0..tableRowVarsAllFiles.Length - 1|]
                                    |> Array.map(fun (file_var, number) -> file_var 
                                                                            |> fun var -> createSearcheKeyVarStr var number)
                                    |> String.concat ","
                                    |> fun str -> str.Replace(",", "")
                                    |> fun str -> str.[0..str.LastIndexOf('\n') - 1])

    let getSearchKeysAll (finalArr : string[][]) =
        
        finalArr
        |> fun arr -> TransposeStrArr arr
        |> Array.map(fun search_key -> { SearchKey = search_key.[0];
                                         Variable = search_key.[1] ;
                                         Filter = search_key.[2] ;
                                         Date = search_key.[3] ;
                                         Infotext = search_key.[4] ;
                                         Product = search_key.[5] ;
                                         CriteriaReferenceWithRevision = "1/154 51-LPA108 338-37;D" ;
                                         Expression = search_key.[6]
                                        })