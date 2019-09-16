namespace EricssonSupportAssistance.EventHandlingFuncs 

open FsXaml
open System.Windows
open EricssonSupportAssistance
open EricssonSupportAssistance.XamlFiles
open System
open System.Windows.Media
open System.Windows.Controls
open System.Windows.Markup

type MainWindowFunctions() =

    let mutable sender = new Controls()
    let mutable mainWin = new MainWindow()
    let mutable updateDataContext = new Event<ObjectToPassEventArgs>()
    let mutable infoEv = new Event<InfoEventArgs>()

    do
        sender.OpenSolutionButton.IsEnabled <- false
        sender.FindSolutionButton.IsEnabled <- false
        sender.UploadButton.IsEnabled <- false


    member this.MainWin 
        with get() = mainWin
        and set(value) = 
            if value <> mainWin then mainWin <- value

    member this.Sender 
        with get() = sender
        and set(value) = 
            if value <> sender then sender <- value

    [<CLIEvent>]
    member this.InfoToAdd = infoEv.Publish 

    [<CLIEvent>]
    member this.UpdateDataContext = updateDataContext.Publish

    member this.OnAuthenticateButtonClicked =
        
        this.Sender.AuthenticationControl.Visibility <- Visibility.Visible
        this.Sender.SearchPageControl.Visibility <- Visibility.Hidden
        this.Sender.UploadPageControl.Visibility <- Visibility.Hidden
        infoEv.Trigger(InfoEventArgs("Entering authentization page", Brushes.DarkRed))
        updateDataContext.Trigger(ObjectToPassEventArgs(this.Sender))

    member this.OnUploadButtonClicked =

        this.Sender.AuthenticationControl.Visibility <- Visibility.Hidden
        this.Sender.SearchPageControl.Visibility <- Visibility.Hidden
        this.Sender.UploadPageControl.Visibility <- Visibility.Visible
        infoEv.Trigger(InfoEventArgs("Entering upload page", Brushes.DarkRed))
        updateDataContext.Trigger(ObjectToPassEventArgs(this.Sender))

    member this.OnSearchButtonClicked =

        this.Sender.AuthenticationControl.Visibility <- Visibility.Hidden
        this.Sender.SearchPageControl.Visibility <- Visibility.Visible
        this.Sender.UploadPageControl.Visibility <- Visibility.Hidden
        infoEv.Trigger(InfoEventArgs("Entering search page", Brushes.DarkRed))
        updateDataContext.Trigger(ObjectToPassEventArgs(this.Sender))
        
        