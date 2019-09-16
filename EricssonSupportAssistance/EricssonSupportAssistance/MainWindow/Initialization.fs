namespace EricssonSupportAssistance.EventHandlingFuncs 

open FsXaml
open System.Windows
open EricssonSupportAssistance
open EricssonSupportAssistance.XamlFiles
open System
open System.Windows.Media
open System.Windows.Controls
open System.Windows.Markup

type Initilization() as this =
    
    let mutable sender = new Controls()
    let mutable mainWin = new MainWindow()
    let mutable mainFncs = new MainWindowFunctions()
    let mutable uplFncs = new UploadFunctions()
    let mutable autFncs = new UploadFunctions()
    let mutable srchFncs = new SearchPageFuncs()

    do
        mainWin.DataContext <- sender
        mainFncs.Sender <- sender
        uplFncs.Sender <- sender
        autFncs.Sender <- sender
        srchFncs.Sender <- sender
        this.AddAllEvents()


    member this.MainWin 
        with get() = mainWin
        and set(value) = 
            if value <> mainWin then mainWin <- value

    member this.AddAllEvents() =
        
        let authButton = (mainWin.FindName("Authenticate") :?> Button)
        authButton.Click.Add(fun _ -> mainFncs.OnAuthenticateButtonClicked)
        let authenCtrl = (mainWin.FindName("AuthenticatePage") :?> UserControl)
        (authenCtrl.FindName("ChooseFileButton") :?> Button).Click.Add(fun _ -> mainFncs.OnUploadButtonClicked)
        (authenCtrl.FindName("AuthenticateButton") :?> Button).Click.Add(fun _ -> mainFncs.OnAuthenticateButtonClicked)
        (mainWin.FindName("Upload") :?> Button).Click.Add(fun _ -> mainFncs.OnUploadButtonClicked)
        (mainWin.FindName("Search") :?> Button).Click.Add(fun _ -> mainFncs.OnSearchButtonClicked)
        mainFncs.InfoToAdd.Add(fun args -> mainFncs.InfoToRegister args)
        (mainWin.FindName("TicketComboBox") :?> TextBox).TextChanged.Add(fun _ -> uplFncs.CheckIfFindSolutionAction)
        (mainWin.FindName("FindSolutionButton") :?> Button).TextInput.Add(fun _ -> uplFncs.OnFindSolutionButtonClicked)
        (mainWin.FindName("ChooseFileButton") :?> Button).Click.Add(fun _ -> uplFncs.OnChooseFileButtonClicked )
        (mainWin.FindName("UploadButton") :?> Button).Click.Add(fun _ -> uplFncs.OnChooseFileButtonClicked )
