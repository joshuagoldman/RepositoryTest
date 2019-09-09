namespace TreeBuilding

open System.Windows.Media
open System.Windows
open System.ComponentModel


module Definitions =
    
        

    type UserControlsSettings() =
        
        let mutable items_source = [|""|]
        let mutable text = ""
        let mutable background = Colors.White

        let ev = new Event<_,_>()

        interface INotifyPropertyChanged with
            [<CLIEvent>]
            member this.PropertyChanged = ev.Publish

        member this.ItemsSource
            with get() = items_source
            and set(value) =
                items_source <- value
                ev.Trigger(this, PropertyChangedEventArgs("ItemsSource"))

        member this.Text
            with get() = text
            and set(value) =
                text <- value
                ev.Trigger(this, PropertyChangedEventArgs("Text"))

        member this.Background
            with get() = background
            and set(value) =
                background <- value
                ev.Trigger(this, PropertyChangedEventArgs("Background"))
         


    type UserControls() =
        member val NewButton = new UserControlsSettings() with get, set
        member val CopyButton = new UserControlsSettings() with get, set 
        member val RemoveButton = new UserControlsSettings() with get, set
        member val NameColumn = new UserControlsSettings() with get, set
        member val TextBoxVariables = new UserControlsSettings() with get, set
    

