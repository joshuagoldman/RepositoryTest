namespace EricssonSupportAssistance.EventHandlingFuncs 

open FsXaml
open System.Windows
open EricssonSupportAssistance
open EricssonSupportAssistance.XamlFiles
open System
open System.Windows.Media
open System.Windows.Controls
open System.Windows.Markup

type MainWindowFunctions() as this =
    
    inherit ControlBase() 

    do
        this.Sender.OpenSolutionButton.IsEnabled <- false
        this.Sender.FindSolutionButton.IsEnabled <- false
        this.Sender.UploadButton.IsEnabled <- false

    member this.OnAuthenticateButtonClicked =
        
        this.Sender.AuthenticationControl.Visibility <- Visibility.Visible
        this.Sender.SearchPageControl.Visibility <- Visibility.Hidden
        this.Sender.UploadPageControl.Visibility <- Visibility.Hidden
        this.infoEv.Trigger(InfoEventArgs("Entering authentization page", Brushes.DarkRed))

    member this.OnUploadButtonClicked =

        this.Sender.AuthenticationControl.Visibility <- Visibility.Hidden
        this.Sender.SearchPageControl.Visibility <- Visibility.Hidden
        this.Sender.UploadPageControl.Visibility <- Visibility.Visible
        this.infoEv.Trigger(InfoEventArgs("Entering upload page", Brushes.DarkRed))

    member this.OnSearchButtonClicked =

        this.Sender.AuthenticationControl.Visibility <- Visibility.Hidden
        this.Sender.SearchPageControl.Visibility <- Visibility.Visible
        this.Sender.UploadPageControl.Visibility <- Visibility.Hidden
        this.infoEv.Trigger(InfoEventArgs("Entering search page", Brushes.DarkRed))

        
        