namespace SearchKeyRep.DocumentReading

open System
open System.Windows
open System.Reflection
open System.IO
open System.Diagnostics
open SearchKeyRep.Transpose
open SearchKeyRep.Methods.SK_1_68_Methods
open System.Text.RegularExpressions

module SetUpCase_SK_1_68_1 = 
    
    let stringModification (str : string) = 
        
        str
        |> fun str ->  Regex.Match(str, "(0).*").Value
        |> fun str -> "(" + str.Replace(", ", "|").Replace("\r", "") + ")"

    let getTableInfo (table : Table) = 
        
        let arrMaxIndex = table.TableInfo.Length - 1 

        let partitionSize = (table.TableInfo.Length - 1) / 2

        let firstPartition = [|0..partitionSize - 1|]

        let lastPartition = [|partitionSize..arrMaxIndex - 1|]

        {ArrMaxIndex = arrMaxIndex ; PartitionSize = partitionSize ; FirstPartition = firstPartition ; LastPartition = lastPartition}

    let Rule1Arr (keyChunkInfosTables : KeyStringChunkInfo[])
                 (logVars : string[]) =

        let allTables = 
            
            keyChunkInfosTables
            |> Array.map(fun table -> Regex.Match(DocString, "(" + table.ChunkStart + ")(\n|.)*(" + table.ChunkEnd + ")").Value
                                      |> fun match_str ->  createRegexMatchesArr match_str (table.Key +  "(\n|.)*?(A10|D20).*")
                                                           |> Array.map(fun str -> { Var = stringModification str ;
                                                                                     Info = Regex.Match(str, "(A|D).*").Value.Replace("\r","")})
                                      |> fun info -> {TableInfo = info})

        let fillVars (table : Table) (indexRange : int[]) =
            
            indexRange
            |> Array.map(fun index -> table.TableInfo.[index].Var)
            |> Array.map(fun variable -> variable.Replace("(", "").Replace(")","") + "|")
            |> String.concat ","
            |> fun str -> str.Replace(",", "")
            |> fun str -> str.Substring(0, str.LastIndexOf("|"))
            |> fun str -> "(" + str + ")"

        let getLastVar (table : Table) (firstPartition : int []) (lastPartition : int []) = 
                
                {X1 = fillVars table firstPartition ; X2 = fillVars table lastPartition} 
        
        let getVars (table : Table) =
            
            let tableInfo = getTableInfo table

            let varLogic (varIndx : VarLogicType) = 
                
                varIndx
                |> function
                    | varIndx when varIndx.Index >= 0 && varIndx.Index < tableInfo.PartitionSize -> {X1 = varIndx.Var ; X2 = fillVars table tableInfo.LastPartition}
                    | varIndx when varIndx.Index >= tableInfo.PartitionSize && varIndx.Index < tableInfo.ArrMaxIndex -> {X1 = varIndx.Var ; X2 = fillVars table tableInfo.FirstPartition}
                    | _ -> {X1 = varIndx.Var  ; X2 = "" }

            let varArr = 
                
                table.TableInfo
                |> Array.map(fun table -> table.Var)
                |> fun varArr ->  Array.zip varArr [|0..tableInfo.ArrMaxIndex|]
                |> Array.map(fun (var, index) -> varLogic {Var = var ; Index = index })
                |> fun arr -> Array.append arr [|(getLastVar table tableInfo.FirstPartition tableInfo.LastPartition)|] 

            varArr

        let specialVars (tables : Table[]) =

            let lengths = 
                tables
                |> Array.map(fun arr -> arr.TableInfo.Length)

            lengths

        let replicateForEachLogVar (varComponents : PLLVars[]) = 
            
            logVars
            |> Array.map(fun var -> varComponents
                                    |> Array.map(fun pll_var -> {X1 = var + pll_var.X1 ; X2 = var + pll_var.X2}))

        let tableRowEachTable =
            allTables
            |> Array.map(fun table -> getVars table)

        let tableRowVarsAllFiles = 
            
            tableRowEachTable
            |> Array.collect(fun sub_arr -> sub_arr)
            |> fun arr -> replicateForEachLogVar arr

        let varRows = 

            let rows = readyVarRows tableRowVarsAllFiles 

            rows

        let expression = 

            tableRowVarsAllFiles
            |> fun arr -> SearchKeyRep.Transpose.TransposePLLArr arr 
            |> Array.map(fun sub_arr -> Array.zip3 sub_arr [|0..logVars.Length - 1|] [|0..sub_arr.Length - 1|]
                                        |> Array.map(fun (file_var, log_number, arr_number) -> file_var 
                                                                                               |> function
                                                                                                    | _ when specialVars allTables
                                                                                                             |> Array.exists(fun num_compare -> num_compare = arr_number) -> expressionFuncSpecial log_number
                                                                                                    | _ -> expressionFunc file_var log_number)
                                        |> String.concat ","
                                        |> fun str -> str.Replace(",", "")
                                        |> fun str -> str.[0..str.LastIndexOf("or") - 1])

        let products = 
            

            let lengths = 
                allTables
                |> Array.map(fun arr -> arr.TableInfo.Length)

            let getRevision (str : string) = 
                
                str
                |>function
                    | _ when Regex.Match(str, "(?<=for rev).*(?= Does not include)").Length > 0 -> Regex.Match(str, "(?<=for rev ).*(?= Does not include)").Value 
                    | _ when Regex.Match(str, "(?<=for rev ).*(?= and higher)").Length > 0 -> Regex.Match(str, "(?<=for rev ).*(?= and higher)").Value
                    | _ -> "*"

            let prodNumber (str : string) = 
                
                Regex.Match(str, "(KD|KDV)[^,|\n|\r\n]*").Value
            
            let prodInfoUnDistributed = 
                keyChunkInfosTables
                |> Array.map(fun table -> Regex.Match(DocString, "(" + table.ChunkStart + ")(\n|.)*(" + table.ChunkEnd + ")").Value
                                          |> fun match_str ->  createRegexMatchesArr match_str "(KDU|KDV).*"
                                                               |> Array.map(fun str ->  { ProdNumber = prodNumber str ;
                                                                                          Revision = getRevision str})
                                          |> Array.map(fun prod_info -> "ProductNumberNEXT" + prod_info.ProdNumber + "NEXTRStateNEXT" + prod_info.Revision + "\n")
                                          |> String.concat ","
                                          |> fun str -> str.Replace("\n," , "\n").Replace(".","")
                                          |> fun str -> str.Substring(0, str.LastIndexOf("\n")))

                
            Array.zip prodInfoUnDistributed lengths 
            |> Array.collect(fun (prodInfo, table_row_num) -> [|0..table_row_num|]
                                                                |> Array.map(fun _ -> prodInfo))
            
        let infoText  =
            
            allTables
            |> Array.collect(fun sub_arr -> sub_arr.TableInfo
                                            |> Array.map(fun row -> row.Info)
                                            |> fun rows -> Array.append rows [|"D2000"|])

        
        let filters = getFilters tableRowVarsAllFiles (logVars.Length - 1)

        let searchKeys = getSearchKeys (expression.Length - 1) "1"
        
        let dates = getDates (expression.Length - 1)

        getSearchKeysAll [|searchKeys;
                           varRows;
                           filters;
                           dates;
                           infoText;
                           products;
                           expression|]