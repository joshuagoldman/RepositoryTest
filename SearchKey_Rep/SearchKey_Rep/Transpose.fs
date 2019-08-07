namespace SearchKeyRep

open System

module Transpose =

    type VarOption =
        | SpecialVar 
        | RegularVar

    type PLLVars =
        {   X1 : string 
            X2 : string 
            VarType : VarOption
            }
    
    let TransposeStrArr (arr : string[][]) =
        
        let arr2Fill = 
            arr.[0]
            |> Array.map(fun sub_arr -> [|sub_arr|])
        
        let otherArrs = 
            arr
            |> Array.skip(1)

        let transposedArr = 
            Array.zip arr2Fill [|0..arr2Fill.Length - 1|]
            |> Array.map(fun (sub_arr, index) -> Array.append sub_arr (otherArrs|> Array.map(fun other_sub_arr -> other_sub_arr.[index])))
                                                                            
        transposedArr

    let TransposePLLArr (arr : PLLVars[][]) =
        
        let arr2Fill = 
            arr.[0]
            |> Array.map(fun sub_arr -> [|sub_arr|])
        
        let otherArrs = 
            arr
            |> Array.skip(1)

        let transposedArr = 
            Array.zip arr2Fill [|0..arr2Fill.Length - 1|]
            |> Array.map(fun (sub_arr, index) -> Array.append sub_arr (otherArrs|> Array.map(fun other_sub_arr -> other_sub_arr.[index])))
                                                                            
        transposedArr