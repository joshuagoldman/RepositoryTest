namespace SearchKeyRep


module StringTranspose =
    
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