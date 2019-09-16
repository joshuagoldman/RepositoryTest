namespace EricssonSupportAssistance.EventHandlingFuncs 

open FsXaml
open System.Windows
open EricssonSupportAssistance
open EricssonSupportAssistance.XamlFiles
open EricssonSupportAssistance.MailHandling.MailActions
open System
open System.Windows.Media
open System.Windows.Controls
open System.Windows.Markup


type SearchPageFuncs() =
        
        let mutable sender = new Controls()

        member this.Sender 
            with get() = sender
            and set(value) = 
                if value <> sender then sender <- value

        member this.InfoEv = new Event<InfoEventArgs>()

        [<CLIEvent>]
        member this.InfoToAdd = this.InfoEv.Publish
    
        member internal this.Method = 
            ""
    
        
        

