namespace FSharpProject

open System

module Say =
    let hello() =

        let list1 = ["John";"Josh";"Michael"]
        
        list1
        |> List.filter (fun x -> x.Contains("J"))
        |> List.map (fun x -> x + " Goldman")
        |>printfn "%A"

    hello()

    Console.ReadKey() |> ignore
