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
    
    inherit ControlBase() 

    member this.OnAuthenticateButtonClicked =
        
        this.Sender.AuthenticationPageControl.Visibility <- Visibility.Visible
        this.Sender.DocumentViewerPageControl.Visibility <- Visibility.Hidden
        this.Sender.UploadPageControl.Visibility <- Visibility.Hidden
        this.Sender.InfoLogs.Visibility <- Visibility.Visible
        this.Sender.ClearLogsButton.Visibility <- Visibility.Visible 
        this.infoEv.Trigger(InfoEventArgs("Entering authentization page", Brushes.DarkRed))

    member this.OnUploadButtonClicked =

        this.Sender.AuthenticationPageControl.Visibility <- Visibility.Hidden
        this.Sender.DocumentViewerPageControl.Visibility <- Visibility.Hidden
        this.Sender.UploadPageControl.Visibility <- Visibility.Visible
        this.Sender.InfoLogs.Visibility <- Visibility.Visible
        this.Sender.ClearLogsButton.Visibility <- Visibility.Visible
        this.infoEv.Trigger(InfoEventArgs("Entering upload page", Brushes.DarkRed))

    member this.OnDocumentViewerButtonClicked =

        this.Sender.AuthenticationPageControl.Visibility <- Visibility.Hidden
        this.Sender.DocumentViewerPageControl.Visibility <- Visibility.Visible
        this.Sender.UploadPageControl.Visibility <- Visibility.Hidden
        this.Sender.InfoLogs.Visibility <- Visibility.Hidden
        this.Sender.ClearLogsButton.Visibility <- Visibility.Hidden
        this.infoEv.Trigger(InfoEventArgs("Entering document viewer page", Brushes.DarkRed))

    member this.OnClearLogsButtonClicked =
        
        this.Sender.InfoLogs.Text <- ""

        
        