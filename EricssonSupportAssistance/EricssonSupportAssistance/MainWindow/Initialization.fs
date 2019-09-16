namespace EricssonSupportAssistance.EventHandlingFuncs 

open FsXaml
open System.Windows
open EricssonSupportAssistance
open EricssonSupportAssistance.XamlFiles
open System
open System.Windows.Media
open System.Windows.Controls
open System.Windows.Controls.Primitives
open System.Windows.Markup

type Initilization() as this =
    
    let mutable sender = new Controls()
    let mutable mainWin = new MainWindow()
    let mutable mainFncs = new MainWindowFunctions()
    let mutable uplFncs = new UploadFunctions()
    let mutable autFncs = new AuthenticateFunctions()
    let mutable srchFncs = new SearchPageFuncs()
    let mutable dataContextUpdateEv = new Event<ObjectToPassEventArgs>()

    do
        mainWin.DataContext <- sender
        uplFncs.Sender <- sender
        autFncs.Sender <- sender
        srchFncs.Sender <- sender
        this.AddAllEvents()

    member this.MainWin 
        with get() = mainWin
        and set(value) = 
            if value <> mainWin then mainWin <- value

    member this.UpdateDatacontext (o : ObjectToPassEventArgs) =
        
        this.MainWin.DataContext <- o.ObjectToPass
        uplFncs.Sender <- (o.ObjectToPass :?> Controls)
        autFncs.Sender <- (o.ObjectToPass :?> Controls)
        srchFncs.Sender <- (o.ObjectToPass :?> Controls)

    member this.InfoToRegister (e : InfoEventArgs) =

        mainFncs.Sender.InfoLogs.Text <- mainFncs.Sender.InfoLogs.Text + "\n> " + e.Message
        mainFncs.Sender.InfoLogs.TbForeground <- e.Foreground
        dataContextUpdateEv.Trigger(ObjectToPassEventArgs(mainFncs.Sender))

    [<CLIEvent>]
    member this.UpdateDataContext = dataContextUpdateEv.Publish

    member this.AddAllEvents() =
        
        mainFncs.InfoToAdd.Add(fun args -> this.InfoToRegister args)
        mainFncs.UpdateDataContext.Add(fun evArgs -> this.UpdateDatacontext evArgs)
        autFncs.InfoToAdd.Add(fun args -> this.InfoToRegister args)
        autFncs.UpdateDataContext.Add(fun evArgs -> this.UpdateDatacontext evArgs)
        this.UpdateDataContext.Add(fun evArgs -> this.UpdateDatacontext evArgs)

        let authButton = (mainWin.FindName("Authenticate") :?> Button)
        authButton.Click.Add(fun _ -> mainFncs.OnAuthenticateButtonClicked)
        let uplButton = (mainWin.FindName("Upload") :?> Button)
        uplButton.Click.Add(fun _ -> mainFncs.OnUploadButtonClicked)
        let srchButton = (mainWin.FindName("Search") :?> Button)
        srchButton.Click.Add(fun _ -> mainFncs.OnSearchButtonClicked)

        let authenCtrl = (mainWin.FindName("AuthenticatePage") :?> UserControl)
        let uplCtrl = (mainWin.FindName("UploadPage") :?> UserControl)

        (authenCtrl.FindName("AuthenticateButton") :?> Button).Click.Add(fun _ -> autFncs.OnAuthenticateButtonClicked )
        (uplCtrl.FindName("Upload") :?> Button).Click.Add(fun _ -> mainFncs.OnUploadButtonClicked)
        (uplCtrl.FindName("FindSolutionButton") :?> Button).Click.Add(fun _ -> mainFncs.OnSearchButtonClicked)
        (uplCtrl.FindName("TicketComboBox") :?> ComboBox).AddHandler(TextBoxBase.TextChangedEvent, TextChangedEventHandler(fun _ _ -> uplFncs.CheckIfFindSolutionAction))
        (uplCtrl.FindName("ChooseFileButton") :?> Button).Click.Add(fun _ -> uplFncs.OnChooseFileButtonClicked )
        (uplCtrl.FindName("FindSolutionButton") :?> Button).Click.Add(fun _ -> uplFncs.OnFindSolutionButtonClicked)
        (uplCtrl.FindName("Upload") :?> Button).Click.Add(fun _ -> uplFncs.OnChooseFileButtonClicked )
