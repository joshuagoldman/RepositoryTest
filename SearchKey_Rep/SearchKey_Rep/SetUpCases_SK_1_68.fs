namespace SearchKeyRep.DocumentReading

open System
open System.Windows
open System.Reflection
open System.IO
open System.Diagnostics
open SearchKeyRep
open System.Text.RegularExpressions

module SetUpCases_SK_1_68 = 
    
    let DocString = 
        
        let stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("SearchKey_Rep.1_68_Update_C.docx")

        let stream2Read = new StreamReader(stream)

        let fileInStringForm = stream2Read.ReadToEnd()

        fileInStringForm

    type KeyStringChunkInfo = 
        {   Key : string
            ChunkStart : string
            ChunkEnd : string
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

    let Rule2Arr (keyChunkInfos : KeyStringChunkInfo[]) (fileInStringForm : string) =
        
        
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
                                            |> Array.map(fun var_pair -> "NameNEXTX1NEXTValueNEXT" + var_pair.X1 + "NEXTisRegexNEXTTRUE\n" + 
                                                                         "NameNEXTX2NEXTValueNEXT" + var_pair.X2 + "NEXTisRegexNEXTTRUE" ))
        ""                                                                           
        

        
        
