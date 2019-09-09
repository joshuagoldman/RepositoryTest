namespace EricssonSupportAssistance.MainWindow 

open FsXaml
open System.Windows
open EricssonSupportAssistance
open EricssonSupportAssistance.XamlFiles
open EricssonSupportAssistance.Functions
open System
open System.Windows.Media
open System.Windows.Controls
open System.Windows.Markup

type MainWindowFunctions() as this =

    let mutable sender = new MainWindowControls()
    let mutable mainWin = new MainWindow()

    let mutable infoEv = new Event<InfoEventArgs>()

    let mutable authPage = new AuthenticatePageFunctions(sender)
    let mutable sPage = new SearchPageFunctions()
    let mutable uplPage = new UploadnPageFunctions()

    do
        mainWin.DataContext <- this.Sender
        this.AddDataContext
        this.AddAllEvents()

    member this.MainWin 
        with get() = mainWin
        and set(value) = 
            if value <> mainWin then mainWin <- value

    member this.Sender 
        with get() = sender
        and set(value) = 
            if value <> sender then sender <- value

    member this.AddDataContext =

        (this.MainWin.FindName("AuthenticatePage") :?> UserControl).DataContext <- authPage.Sender
        (this.MainWin.FindName("SearchPage") :?> UserControl).DataContext <- sPage.Sender
        (this.MainWin.FindName("UploadPage") :?> UserControl).DataContext <- uplPage.Sender
    
    [<CLIEvent>]
    member this.InfoToAdd = infoEv.Publish

    member this.AddAllEvents() =
        
        let authButton = (mainWin.FindName("Authenticate") :?> Button)
        authButton.Click.Add(fun _ -> this.OnAuthenticateButtonClicked)
        let authenCtrl = (mainWin.FindName("AuthenticatePage") :?> UserControl)
        (authenCtrl.FindName("ChooseFileButton") :?> Button).Click.Add(fun _ -> uplPage.OnUploadButtonClicked)
        (authenCtrl.FindName("AuthenticateButton") :?> Button).Click.Add(fun _ -> authPage.OnAuthenticateButtonClicked)
        (mainWin.FindName("Upload") :?> Button).Click.Add(fun _ -> this.OnUploadButtonClicked)
        (mainWin.FindName("Search") :?> Button).Click.Add(fun _ -> this.OnSearchButtonClicked)
        this.InfoToAdd.Add(fun args -> this.InfoToRegister args)
        authPage.ObjectToPass.Add(fun evArgs -> this.OnObjectToPassRegistered evArgs)

    member this.OnObjectToPassRegistered (e : ObjectToPassEventArgs) =
        
        e.ObjectsToPass
        |> Seq.iter(fun o -> o
                            |> function
                                | _ when (o :? MainWindowControls) ->
                                    this.Sender <- (o :?> MainWindowControls)

                                | _ when (o :? AuthenticatePageControls) ->
                                    authPage.Sender <-  (o :?> AuthenticatePageControls)

                                | _ when (o :? UploadPageControls) ->
                                    uplPage.Sender <-  (o :?> UploadPageControls)

                                | _ when (o :? SearchPageControls) ->
                                    sPage.Sender <-  (o :?> SearchPageControls)

                                | _ -> o |> ignore)

        |> fun _ -> infoEv.Trigger(InfoEventArgs(e.Message, Brushes.DarkRed))
        |> fun _ -> this.AddDataContext


    member this.InfoToRegister (e : InfoEventArgs) =

        this.Sender.InfoLogs.Text <- this.Sender.InfoLogs.Text + "\n> " + e.Message
        this.Sender.InfoLogs.TbForeground <- e.Foreground

    member this.OnAuthenticateButtonClicked =
        
        authPage.Sender.AuthenticationControl.Visibility <- Visibility.Visible
        sPage.Sender.SearchPageControl.Visibility <- Visibility.Hidden
        uplPage.Sender.UploadPageControl.Visibility <- Visibility.Hidden
        infoEv.Trigger(InfoEventArgs("Entering authentization page", Brushes.DarkRed))

    member this.OnUploadButtonClicked =

        authPage.Sender.AuthenticationControl.Visibility <- Visibility.Hidden
        sPage.Sender.SearchPageControl.Visibility <- Visibility.Hidden
        uplPage.Sender.UploadPageControl.Visibility <- Visibility.Visible
        infoEv.Trigger(InfoEventArgs("Entering upload page", Brushes.DarkRed))

    member this.OnSearchButtonClicked =

        authPage.Sender.AuthenticationControl.Visibility <- Visibility.Hidden
        sPage.Sender.SearchPageControl.Visibility <- Visibility.Visible
        uplPage.Sender.UploadPageControl.Visibility <- Visibility.Hidden
        infoEv.Trigger(InfoEventArgs("Entering search page", Brushes.DarkRed))
        
        

