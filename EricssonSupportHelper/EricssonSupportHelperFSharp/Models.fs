namespace EricssonSupportHelperFSharp

open System.Xaml
open System.Windows.Media

module Models =
    
    type ControlAttributes() =
        
        let mutable name = ""
        let mutable text = ""
        let mutable itemsSource = [|""|]
        let mutable color = Colors.Transparent

        member this.Name
            with get() = name
            and set(value) = 
                name <- value

        member this.Text
            with get() = text
            and set(value) = 
                text <- value
        
        member this.ItemsSource
            with get() = itemsSource
            and set(value) = 
                itemsSource <- value

        member this.Color
            with get() = color
            and set(value) = 
                color <- value