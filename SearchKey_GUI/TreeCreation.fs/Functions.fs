namespace TreeBuilding

open System.Windows.Media
open System.Windows
open FSharp.Collections
open System

module Functions = 
    
    let AddToArr (arr : string[]) (str2Add : string) = 
        
        let doesNotAlreadyExist = 
            arr
            |> Array.forall(fun str -> str <> str2Add)
        
        let isNotEmpty = 
            str2Add.Length > 0

        arr
        |>function
            | _ when isNotEmpty && doesNotAlreadyExist ->  Array.append arr [|str2Add|]
            | _ -> arr 
        
