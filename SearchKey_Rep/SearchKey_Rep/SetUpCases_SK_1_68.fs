namespace SearchKey_Rep.DocumentReading

open System
open System.Windows
open System.Reflection
open System.IO
open SearchKeyRep.RepeatSearchKey
open System.Diagnostics
open System.Text.RegularExpressions

module SetUpCases_SK_1_68 = 
    
    let docRegex = 
        
        let stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("SearchKey_Rep.1_68_Update_C.docx")

        let stream2Read = new StreamReader(stream)

        let fileInStringForm = stream2Read.ReadToEnd()

        fileInStringForm

    type TrapInfos = 
        {   Conditions : string[][]
            InfoTextComponent : string[]
            }

    type KeyStringChunkInfo = 
        {   Key : string
            ChunkStart : string
            ChunkEnd : string
            }

    type TrapVars = 
        {   X1 : string
            X2 : string
            }

    let createVarsRule2 (conditions : string[]) (vars2Choose : string[][]) =
        
        let varMat = 
            vars2Choose
            |> Array.zip

    let flipArr (arr : string[])=
        
        let flipList = Array.empty
        arr
        |> Array.map(fun str -> Array.append flipList [|str|])

    let getTransposeArr (arr2Fill : string[][]) (arr : string [][])=
        
        Array.append arr2Fill arr       
        
    let TransposeStrArr (arr : string[][]) =
        
        let allArrays = 
            arr
            |> Array.map(fun sub_arr -> flipArr sub_arr)
            |> Array.skip(1)
        
        let finalArr = 
            arr
            |> Array.map(fun sub_arr -> flipArr sub_arr)
            |> Array.item(0)

        allArrays
        |> Array.map(fun sub_arr -> sub_arr |> Array.map(fun sub_sub_arr -> sub_sub_arr.[0]))
        |> getTransposeArr finalArr 
        


    let Rule2Arr (keyChunkInfos : KeyStringChunkInfo[]) (fileInStringForm : string) =
        
        
        let tableRowVarsAllFiles =
            
            let tableRowVarsFileSpecific (keyChunkInfo : KeyStringChunkInfo) = 
                
                let stringChunk = Regex.Match(String.Format("({0})(.|\n)*({1})",
                                                            keyChunkInfo.ChunkStart,
                                                            keyChunkInfo.ChunkEnd),
                                              fileInStringForm).Result();

                Regex.Split(String.Format("({0}).*", keyChunkInfo.Key), stringChunk)

            keyChunkInfos
            |> Array.map(fun info -> tableRowVarsFileSpecific info) >> 


        let trapInfos = 
            
            let RegexstringChunk = Regex.Match("(Rule 2: Valid when no ‘Trap info’ available from Step 1)(.|\n)*(2.5 Criteria Valid for)", fileInStringForm).Result()

            let tableVals = Regex.Split("(2).*", RegexstringChunk)

            let conditions = tableVals |> Array.map(fun str -> Regex.Split("(Y|N|any).*", str))

            let infoTextComponent = tableVals |> Array.map(fun str -> Regex.Match("(A1).*", str).Result())

            {Conditions = conditions ; InfoTextComponent = infoTextComponent}


        

        
        
