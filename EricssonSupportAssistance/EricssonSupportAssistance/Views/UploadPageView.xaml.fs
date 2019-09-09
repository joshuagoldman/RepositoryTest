namespace EricssonSupportAssistance.Views 

open FsXaml
open EricssonSupportAssistance.Functions
open EricssonSupportAssistance.XamlFiles
open System.Windows
open System
open System.Windows.Controls
open System.Windows.Markup

type UploadPageViewModel() =
    
    let mutable uplPage = new UploadPage()

    do
        let eventFuncs = new AuthenticatePageFunctions()
        uplPage.DataContext <- eventFuncs.Sender

    member this.UplPage
        with get() = uplPage
        and set(value) =
            if value <> uplPage
            then uplPage <- value






        

