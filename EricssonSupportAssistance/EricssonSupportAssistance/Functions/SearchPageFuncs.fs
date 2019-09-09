namespace EricssonSupportAssistance.Functions 

open FsXaml
open System.Windows
open EricssonSupportAssistance
open EricssonSupportAssistance.XamlFiles
open EricssonSupportAssistance.MailHandling.MailActions
open System
open System.Windows.Media
open System.Windows.Controls
open System.Windows.Markup



type SearchPageFunctions() =

    let mutable sender = new SearchPageControls()

    member this.Sender 
        with get() = sender
        and set(value) = 
            if value <> sender then sender <- value

    member this.OnMailInfo (e : MailInfoEventArgs) =
        
        this.Sender.InfoLogs.Text <- this.Sender.InfoLogs.Text + "\n> " + String.Format("{0}{1}",
                                                    e.ErrorMessage, e.Message)
       
    
    
        
        

