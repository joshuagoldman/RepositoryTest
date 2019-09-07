namespace EricssonSupportAssistance

open System.Windows.Media
open System
open System.ComponentModel

type ControlAtributes() =
    
    let mutable text = ""
    let mutable itemsSource = [|""|]
    let mutable color = Colors.White
    let ev = new Event<_,_>()

    member this.Text 
        with get() = text 
        and set(value) =
            text <- value
            ev.Trigger(this, PropertyChangedEventArgs("Text"))

    member this.ItemsSource 
        with get() = itemsSource 
        and set(value) =
            itemsSource <- value
            ev.Trigger(this, PropertyChangedEventArgs("ItemsSource"))

    member this.Color 
        with get() = color 
        and set(value) =
            color <- value
            ev.Trigger(this, PropertyChangedEventArgs("Color"))
    
    interface INotifyPropertyChanged with
        [<CLIEvent>]
        member this.PropertyChanged = ev.Publish

