namespace SearchKeyRep.DocumentReading

open System
open System.Windows
open System.Reflection
open System.IO
open System.Diagnostics
open SearchKeyRep.StringTranspose
open System.Text.RegularExpressions

module SetUpCases_SK_1_68 = 
    
    let DocString = 
        
        let stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("SearchKey_Rep.1_68_Update_C.docx")

        let stream2Read = new StreamReader(stream)

        let fileInStringForm = stream2Read.ReadToEnd()

        fileInStringForm

    type SearchKeyElements = 
        { SearchKey : string 
          Variable : string
          Filter : string
          Date : string
          Infotext : string
          Product : string
          CriteriaReferenceWithRevision : string
          }

    type KeyStringChunkInfo = 
        {   Key : string
            ChunkStart : string
            ChunkEnd : string
            file : string 
            }

    type PLLInfos = 
        {   Conditions : string[][]
            InfoTextComponent : string[]
            }

    type PLLVars = 
        {   X1 : string
            X2 : string
            }

    type Options = 
        | None
        | X1
        | X2

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

    let createVarPair (conds : string []) (vars2Choose : string[]) =
        
        let valsZipped = Array.zip conds vars2Choose
        {X1 = valsZipped 
                |> Array.filter(fun (cond, _) -> isVariable cond = Options.X1)
                |> Array.map(fun (_, var) -> var + "|")
                |> fun x -> "(" + String.Concat(x ,',') + ")"
         X2 =  valsZipped 
                |> Array.filter(fun (cond, _) -> isVariable cond = Options.X2) 
                |> Array.map(fun (_, var) -> var + "|")
                |> fun x -> "(" + String.Concat(x ,',') + ")"}
        
    
    let createAllVarPairs (info : PLLInfos) (vars2Choose : string[]) =
        
        info.Conditions
        |> Array.map(fun cond -> createVarPair cond vars2Choose)
        
 
    let trapInfos = 
        
        let RegexstringChunk = Regex.Split("(Rule 2: Valid when no ‘Trap info’ available from Step 1)(.|\n)*(2.5 Criteria Valid for)", DocString).[0]

        let tableVals = Regex.Split("(2).*", RegexstringChunk)

        let conditions = tableVals |> Array.map(fun str -> Regex.Split("(Y|N|any).*", str))

        let infoTextComponent = tableVals |> Array.map(fun str -> Regex.Split("(A1).*", str).[0])

        {Conditions = conditions ; InfoTextComponent = infoTextComponent}

    let Rule2Arr (keyChunkInfos : KeyStringChunkInfo[]) 
                 (keyChunkInfosProd : KeyStringChunkInfo)
                 (fileInStringForm : string) =
        
        
        let tableRowVarsAllFiles =
            
            let tableRowVarsFileSpecific (keyChunkInfo : KeyStringChunkInfo) = 
                
                let stringChunk = Regex.Split(String.Format("({0})(.|\n)*({1})",
                                                            keyChunkInfo.ChunkStart,
                                                            keyChunkInfo.ChunkEnd),
                                              fileInStringForm).[0];

                Regex.Split(String.Format("({0}).*", keyChunkInfo.Key), stringChunk)

            keyChunkInfos
            |> Array.map(fun info -> tableRowVarsFileSpecific info)
            |> Array.map(fun arr -> createAllVarPairs trapInfos arr)
        
        let readyVarRows = 
            
            tableRowVarsAllFiles
            |> Array.collect(fun file_vars -> file_vars 
                                            |> Array.map(fun var_pair -> "NameNEXTX1NEXTValueNEXT" +
                                                                         var_pair.X1 +
                                                                         "NEXTisRegexNEXTTRUE\n" + 
                                                                         "NameNEXTX2NEXTValueNEXT" +
                                                                         var_pair.X2 +
                                                                         "NEXTisRegexNEXTTRUE" ))

        let products  = 
            
            let stringChunk = Regex.Split(String.Format("({0})(.|\n)*({1})",
                                                        keyChunkInfosProd.ChunkStart,
                                                        keyChunkInfosProd.ChunkEnd),
                                          fileInStringForm).[0];

            Regex.Split(String.Format("({0}).*", keyChunkInfosProd.Key), stringChunk)
            |> Array.map(fun serial_number -> "ProductNumberNEXT" +
                                              serial_number +
                                              "NEXTRStateNEXT*\n")
            
        
        let infoText = 
            
            let commonTxt = 
                Regex.Split("(Information text to repair center)(.|\n)*(Information text to lead repair centre (extended information))", DocString).[0]

            trapInfos.InfoTextComponent
            |> Array.map(fun txt -> commonTxt.Replace("POS", txt))

        let filters = 

            keyChunkInfos
            |> Array.collect(fun file_var -> [|1..readyVarRows.Length|]
                                            |> Array.map(fun _ -> "SearchFilesFilter" + file_var.file))

        let searchKeys = 
            
            [|1..readyVarRows.Length|]
            |> Array.map(fun num -> "" + int2Alphabet num + "; Rev D")
        
        let dates =
            
            [|1..readyVarRows.Length|]
            |> Array.map(fun _ -> "2019-08-05" )
        
        Array.append [|searchKeys|] [| readyVarRows;
                                       filters;
                                       dates;
                                       infoText;
                                       (products);
                                      |]
        |> TransposeStrArr
        |> Array.map(fun search_key -> { SearchKey = search_key.[0];
                                         Variable = search_key.[1] ;
                                         Filter = search_key.[2] ;
                                         Date = search_key.[3] ;
                                         Infotext = search_key.[4] ;
                                         Product = search_key.[5] ;
                                         CriteriaReferenceWithRevision = "1/154 51-LPA108 338-37;D"
                                        })


                                        