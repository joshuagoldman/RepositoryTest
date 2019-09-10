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

    let mutable sender = new Controls()
    let mutable mainWin = new MainWindow()

    do
        mainWin.DataContext <- this.Sender
        this.AddAllEvents()

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

    member this.AddAllEvents() =
        
        let authButton = (mainWin.FindName("Authenticate") :?> Button)
        authButton.Click.Add(fun _ -> this.OnAuthenticateButtonClicked)
        let authenCtrl = (mainWin.FindName("AuthenticatePage") :?> UserControl)
        (authenCtrl.FindName("ChooseFileButton") :?> Button).Click.Add(fun _ -> this.OnUploadButtonClicked)
        (authenCtrl.FindName("AuthenticateButton") :?> Button).Click.Add(fun _ -> this.OnAuthenticateButtonClicked)
        (mainWin.FindName("Upload") :?> Button).Click.Add(fun _ -> this.OnUploadButtonClicked)
        (mainWin.FindName("Search") :?> Button).Click.Add(fun _ -> this.OnSearchButtonClicked)
        this.InfoToAdd.Add(fun args -> this.InfoToRegister args)

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
        
        
(*    member this.OnObjectToPassRegistered (e : ObjectToPassEventArgs) =

e.ObjectsToPass
|> Seq.iter(fun o -> o
                    |> function
                        | _ when (o :? Controls) ->
                            this.Sender <- (o :?> Controls)

                        | _ when (o :? AuthenticatePageControls) ->
                            authPage.Sender <-  (o :?> AuthenticatePageControls)

                        | _ when (o :? UploadPageControls) ->
                            uplPage.Sender <-  (o :?> UploadPageControls)

                        | _ when (o :? SearchPageControls) ->
                            sPage.Sender <-  (o :?> SearchPageControls)

                        | _ -> o |> ignore)

|> fun _ -> infoEv.Trigger(InfoEventArgs(e.Message, Brushes.DarkRed))
|> fun _ -> this.AddDataContext*)
