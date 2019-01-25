namespace FSharpProject

open System
open System
open System.Linq
open System.Reflection
open System.Text.RegularExpressions

module Say =
    open System.Collections
    
    type VariablesNEquation =
        { Variables : seq<string>
          Equation : string}


    
    let SearchNEvaluate logFileText vars eq = 
        
        let  Obj =
            {Variables = vars ; Equation = eq};
        
        Obj
        |> (fun x  y -> Regex.Match(x,y) ; Obj.Variables x ; Obj.Equation  

        let ss = 
            3
        Console.WriteLine(ss)
    
    
    Console.WriteLine()

    Console.ReadKey() |> ignore
