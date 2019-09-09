namespace EricssonSupportAssistance.Functions 

open FsXaml
open System.Windows
open EricssonSupportAssistance
open System.Reflection
open System.Windows.Forms
open EricssonSupportAssistance.XamlFiles
open EricssonSupportAssistance.MailHandling.MailActions
open System
open System.Windows.Media
open System.Windows.Controls
open System.IO
open Microsoft.Win32


type UploadnPageFunctions() =

    let mutable sender = new UploadPageControls()
    let mutable infoEv = new Event<InfoEventArgs>()
    let mutable objPassingEv = new Event<ObjectToPassEventArgs>()

    member this.Sender 
        with get() = sender
        and set(value) = 
            if value <> sender then sender <- value

    [<CLIEvent>]
    member this.InfoToAdd = infoEv.Publish

    [<CLIEvent>]
    member this.ObjectToPass = objPassingEv.Publish

    member this.OnUploadButtonClicked =
        
        let dialog = new OpenFileDialog()


        Assembly.LoadFile(dialog.FileName)
    
        
        

