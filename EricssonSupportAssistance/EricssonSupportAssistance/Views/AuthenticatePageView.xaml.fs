namespace EricssonSupportAssistance.Views 

open FsXaml
open EricssonSupportAssistance.Functions
open EricssonSupportAssistance.XamlFiles
open EricssonSupportAssistance
open System.Windows
open System
open System.Windows.Controls
open System.Windows.Markup


type AuthenticateViewModel() =
    
    let mutable authPage = new AuthenticatePage()

    do
        let eventFuncs = new AuthenticatePageFunctions()
        authPage.DataContext <- eventFuncs.Sender

    member this.Authpage
        with get() = authPage
        and set(value) =
            if value <> authPage
            then authPage <- value




        

