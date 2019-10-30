namespace Vradim

open Fable.React
open Fable.React.Props

module App =
    

    type Model =
        {   
            Count : int
        }

    
    type msg =
    | Increment
    | Decrement

    let init() = 
        {
            Count = 0
        }

    let update msg model : Model=
        match msg with
        | Increment -> {Count = model.Count + 1}
        | Decrement -> {Count = model.Count - 1}

    let view model dispatch =
        div []
         [
            button [] [str "-"]
            h1 [] [ofInt model.Count]
            button [] [str "+"]
         ]

    open Elmish
    open Elmish.React

    Program.mkSimple init update view
    |> Program.withConsoleTrace
    |> Program.withReactSynchronous "App"
    |> Program.run