namespace EricssonSupportAssistance.Views 

open FsXaml
open EricssonSupportAssistance.Functions
open EricssonSupportAssistance.XamlFiles
open System.Windows
open System
open System.Windows.Controls
open System.Windows.Markup

type SearchPageViewModel() =
    
    let mutable sPage = new SearchPage()

    do
        let eventFuncs = new SearchPageFunctions()
        sPage.DataContext <- eventFuncs.Sender

    member this.SPage
        with get() = sPage
        and set(value) =
            if value <> sPage
            then sPage <- value





        

