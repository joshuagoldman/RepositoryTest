namespace FSharpProject

open System
open System
open System.Linq
open System.Reflection
open System.Text.RegularExpressions

module Say =
    open System.Collections
    
    type VariablesNEquation =
        { Variables : Map<List<string>, string>}

    let Dictionary = 
        Map.empty.
            Add(["mck"; "skl"; "sdf"], "x > 3 && y > 2 || z < 5").
            Add(["aks"], "x > 0")
    
    let text =
        "sjdlkkjfkmck sjldkje"

    let SearchNEvaluateVar (logFileText : string) (var : string) = 
        Regex.IsMatch(logFileText, var)

    let searchNEvaluate (logfileText : string) (dict : Map<List<string>, string>) =
        
        dict
        |> dict
            
    SearchNEvaluate text "kk"
    