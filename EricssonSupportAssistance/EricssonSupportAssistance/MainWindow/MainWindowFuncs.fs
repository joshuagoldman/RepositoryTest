﻿namespace EricssonSupportAssistance.EventHandlingFuncs 

open FsXaml
open System.Windows
open EricssonSupportAssistance
open EricssonSupportAssistance.XamlFiles
open System
open System.Windows.Media
open System.Windows.Controls
open System.Windows.Markup

type MainWindowFunctions() as this =

    let mutable sender = new Controls()
    let mutable mainWin = new MainWindow()

    do
        mainWin.DataContext <- this.Sender
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

    member this.InfoEv = new Event<InfoEventArgs>()

    [<CLIEvent>]
    member this.InfoToAdd = this.InfoEv.Publish

    member this.InfoToRegister (e : InfoEventArgs) =

        this.Sender.InfoLogs.Text <- this.Sender.InfoLogs.Text + "\n> " + e.Message
        this.Sender.InfoLogs.TbForeground <- e.Foreground

    member this.OnAuthenticateButtonClicked =
        
        this.Sender.AuthenticationControl.Visibility <- Visibility.Visible
        this.Sender.SearchPageControl.Visibility <- Visibility.Hidden
        this.Sender.UploadPageControl.Visibility <- Visibility.Hidden
        this.InfoEv.Trigger(InfoEventArgs("Entering authentization page", Brushes.DarkRed))

    member this.OnUploadButtonClicked =

        this.Sender.AuthenticationControl.Visibility <- Visibility.Hidden
        this.Sender.SearchPageControl.Visibility <- Visibility.Hidden
        this.Sender.UploadPageControl.Visibility <- Visibility.Visible
        this.InfoEv.Trigger(InfoEventArgs("Entering upload page", Brushes.DarkRed))

    member this.OnSearchButtonClicked =

        this.Sender.AuthenticationControl.Visibility <- Visibility.Hidden
        this.Sender.SearchPageControl.Visibility <- Visibility.Visible
        this.Sender.UploadPageControl.Visibility <- Visibility.Hidden
        this.InfoEv.Trigger(InfoEventArgs("Entering search page", Brushes.DarkRed))
        
        