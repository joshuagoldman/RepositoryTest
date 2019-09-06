namespace EricssonSupportAssistance.Models

open System

    
type ControllAttributes() = 
        
    let mutable text = ""
    let mutable items_source = [|""|]
        
    member this.Text 
        with get() = text
        and set(value) =
            text <- value

    member this.ItemsSource 
        with get() = items_source
        and set(value) =
            items_source <- value

