namespace SearchKeyRep.DocumentReading

open System
open System.Windows
open System.Reflection
open System.IO
open System.Diagnostics
open SearchKeyRep.Transpose
open SearchKeyRep.Methods.SK_1_68_Methods
open System.Text.RegularExpressions

module SetUpCase_SK_1_68_2 = 
    


    let Rule2Arr (keyChunkInfos : KeyStringChunkInfo[]) 
                 (keyChunkInfosProd : KeyStringChunkInfo)
                 (fileInStringForm : string) =

        let tableRowVarsAllFiles =
            
            let tableRowVarsFileSpecific (keyChunkInfo : KeyStringChunkInfo) = 
                
                let stringChunk = Regex.Match(fileInStringForm,
                                              String.Format("({0})(.|\n)*({1})",
                                                            keyChunkInfo.ChunkStart,
                                                            keyChunkInfo.ChunkEnd)).Value;

                let fileVars = 
                    createRegexMatchesArr stringChunk (String.Format("({0}).*", keyChunkInfo.Key))
                    |> Array.map(fun arr -> arr.Replace("\r", "").Replace("\n",""))
                    

                fileVars

            keyChunkInfos
            |> Array.map(fun info -> tableRowVarsFileSpecific info)
            |> Array.map(fun arr -> createAllVarPairs trapInfos arr)
        

        let varRows = readyVarRows tableRowVarsAllFiles
        
        let numOfSearchKeys = varRows.Length

        let products  = 
            
            let stringChunk = Regex.Match(fileInStringForm, String.Format("({0})(.|\n)*({1})", keyChunkInfosProd.ChunkStart, keyChunkInfosProd.ChunkEnd)).Value


            let pattern = String.Format("({0}).*( )", keyChunkInfosProd.Key)

            let products =
                createRegexMatchesArr stringChunk pattern
                |> Array.map(fun serial_number -> "ProductNumberNEXT" +
                                                  serial_number.Trim() +
                                                  "NEXTRStateNEXT*\n")

                |> fun x -> newArr4EachSearchKey x numOfSearchKeys

            products
        
        let infoText =  
                
            let commonText = 
                "HW Fault indicated. Replace component at position POS. Report as A105/59. Major Fault."

            trapInfos.InfoTextComponent
            |> Array.map(fun txt -> commonText.Replace("POS", txt))
        
        let filters = getFilters tableRowVarsAllFiles (keyChunkInfos.Length - 1)

        let expression = 

            tableRowVarsAllFiles
            |> fun arr -> SearchKeyRep.Transpose.TransposePLLArr arr 
            |> Array.map(fun sub_arr -> Array.zip sub_arr [|0..keyChunkInfos.Length - 1|]
                                        |> Array.map(fun (file_var, number) -> file_var 
                                                                               |> fun var -> expressionFunc var number)
                                        |> String.concat ","
                                        |> fun str -> str.Replace(",", "")
                                        |> fun str -> str.[0..str.LastIndexOf("or") - 1])

        let searchKeys = getSearchKeys (expression.Length - 1) "2"

        let dates = getDates (expression.Length - 1)

        getSearchKeysAll [|searchKeys;
                           varRows;
                           filters;
                           dates;
                           infoText;
                           products;
                           expression|]



                                        