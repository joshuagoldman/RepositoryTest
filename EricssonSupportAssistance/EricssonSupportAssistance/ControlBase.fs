namespace EricssonSupportAssistance

open FsXaml
open System.Windows
open EricssonSupportAssistance
open System
open System.Windows.Media
open System.Windows.Controls
open System.Windows.Controls.Primitives
open System.Windows.Markup
open System.Reflection


type ControlBase() =
    
    let mutable sender = new Controls()

    member val infoEv = new Event<InfoEventArgs>() with get, set

    [<CLIEvent>]
    member this.InfoToAdd = this.infoEv.Publish

    member this.Sender 
        with get() = sender
        and set(value) = 
            if value <> sender then sender <- value