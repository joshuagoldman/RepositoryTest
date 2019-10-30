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
open System.Reflection
open System.Xml.XPath

type Initilization() as this =

    inherit ControlBase()    

    let mutable mainWin = new MainWindow()
    let mutable mainFncs = new MainWindowFunctions()
    let mutable uplFncs = new UploadFunctions()
    let mutable autFncs = new AuthenticateFunctions()
    let mutable docViewFncs = new DocumentViewerFuncs()
    let mutable dataContextUpdateEv = new Event<ObjectToPassEventArgs>()
    let mutable sequenceOfControls = [||]
    let mutable sequenceOfCtrlBase = [|this :> ControlBase|]

    do
        mainWin.DataContext <- this.Sender
        this.AddAllEvents()

        this.Sender.InfoLogs.Visibility <- Visibility.Visible
        this.Sender.UploadSolutionButton.IsEnabled <- true
        this.Sender.ChooseFileButton.IsEnabled <- true
        this.Sender.FindSolutionButton.IsEnabled <- false
        this.Sender.OpenSolutionButton.IsEnabled <- false
        this.Sender.UploadButton.IsEnabled <- false
        this.Sender.TicketComboBox.ItemsSource <- uplFncs.TstOutput.FailedTMDoc.XPathSelectElements("*//Ticket")
                                                  |> Seq.map(fun el -> el.FirstAttribute.Value)
                                                  |> fun x -> x |> Seq.toArray

    member this.MainWin 
        with get() = mainWin
        and set(value) = 
            if value <> mainWin then mainWin <- value

    member this.UplFncs 
        with get() = uplFncs
        and set(value) =
            if value <> uplFncs then uplFncs <- value

    member this.AutFncs 
        with get() = autFncs
        and set(value) =
            if value <> autFncs then autFncs <- value

    member this.DocViewFncs 
        with get() = docViewFncs
        and set(value) =
            if value <> docViewFncs then docViewFncs <- value

    member this.MainFncs 
        with get() = mainFncs
        and set(value) =
            if value <> mainFncs then mainFncs <- value

    member this.SequenceOfControls 
        with get() = sequenceOfControls
        and set(value) =
            if value <> sequenceOfControls then sequenceOfControls <- value
    
    member this.SequenceOfCtrlBase 
        with get() = sequenceOfCtrlBase
        and set(value) =
            if value <> sequenceOfCtrlBase then sequenceOfCtrlBase <- value

    member this.UpdateDatacontext (o : ObjectToPassEventArgs) =
        
        let controlAttrPropertyToChange = this.Sender.GetType().GetProperties(BindingFlags.Public ||| BindingFlags.Instance)
                                          |> Array.filter(fun prop ->  prop.PropertyType = typeof<ControlAtributes>)
                                          |> Array.find(fun prop -> prop.Name = o.nameWOwner.[0])
                                          |> fun x -> x.GetValue(this.Sender)

        controlAttrPropertyToChange.GetType().GetProperties(BindingFlags.Public ||| BindingFlags.Instance)
        |> Array.find(fun prop -> prop.Name = o.nameWOwner.[1])
        |> fun prop -> prop.SetValue(controlAttrPropertyToChange, o.Value)

        this.SequenceOfCtrlBase
        |> Seq.iter(fun ctrlBase -> ctrlBase.Sender <- this.Sender)

    member this.InfoToRegister (e : InfoEventArgs) =

        mainFncs.Sender.InfoLogs.Text <- mainFncs.Sender.InfoLogs.Text + "\n> " + e.Message
        mainFncs.Sender.InfoLogs.TbForeground <- e.Foreground

    [<CLIEvent>]
    member this.UpdateDataContext = dataContextUpdateEv.Publish

    member this.listenToAllCtrlAttr = 

            let fillCtrls (infoArr : PropertyInfo[]) (o : obj) =
                
                infoArr
                |> Array.filter(fun prop -> prop.PropertyType = typeof<Controls>)
                |> Array.map(fun prop -> prop.GetValue(o) :?> Controls)
                |> fun x -> this.SequenceOfControls <- Array.append this.SequenceOfControls x

            let fillCtrlsBase (infoArr : PropertyInfo[]) (o : obj) =
                
                infoArr
                |> Array.filter(fun prop -> prop.PropertyType.IsSubclassOf(typeof<ControlBase>))
                |> Array.map(fun prop -> prop.GetValue(o) :?> ControlBase)
                |> fun x -> this.SequenceOfCtrlBase <- Array.append this.SequenceOfCtrlBase x          


            let allCtrlBaseInstancesFirstLevel =
                
                this.GetType().GetProperties(BindingFlags.Public ||| BindingFlags.Instance)
                |> fun props -> fillCtrlsBase props this
                                |> fun _ -> props
                |> Array.filter(fun prop ->  prop.PropertyType.IsSubclassOf(typeof<ControlBase>))
                |> Array.map(fun prop -> prop.GetValue(this))


            allCtrlBaseInstancesFirstLevel
            |> Array.iter(fun prop -> prop.GetType().GetProperties(BindingFlags.Public ||| BindingFlags.Instance)
                                      |> fun props -> fillCtrlsBase props prop
                                                      |> fun _ -> props
                                      |> Array.filter(fun subProp -> subProp.PropertyType = typeof<Controls> ||
                                                                     subProp.PropertyType.IsSubclassOf(typeof<ControlBase>))
                                      |> fun subProps -> (fillCtrls subProps prop)
                                                         |> fun _ -> subProps
                                                                     |> Array.map(fun subProp -> subProp.GetValue(prop))
                                                         |> fun subPropsObj -> subPropsObj
                                                                               |> Array.iter(fun subPropObj -> subPropObj.GetType().GetProperties(BindingFlags.Public ||| BindingFlags.Instance)
                                                                                                               |> fun subSubProps -> fillCtrls subSubProps subPropObj))
            
            
            this.SequenceOfCtrlBase
            |> Seq.iter(fun ctrlBase -> ctrlBase.InfoToAdd.Add(fun evArgs -> this.InfoToRegister evArgs))

            let ctrlAttrsAll =
                
                this.SequenceOfControls
                |> Array.collect(fun ctrl -> ctrl.GetType().GetProperties(BindingFlags.Public ||| BindingFlags.Instance)
                                             |> Array.filter(fun subProp -> subProp.PropertyType = typeof<ControlAtributes>)
                                             |> Array.map(fun subProp -> subProp.GetValue(ctrl) :?> ControlAtributes))

            ctrlAttrsAll
            |> Array.iter(fun ctrlAttr -> ctrlAttr.UpdateDataContext.Add(fun evArgs -> this.UpdateDatacontext evArgs))

    member this.AddAllEvents() =

        this.listenToAllCtrlAttr

        // Authentication button located in the main window
        let authButton = (mainWin.FindName("AuthenticatePageButton") :?> Button)
        authButton.Click.Add(fun _ -> mainFncs.OnAuthenticateButtonClicked)

        // Upload button located in the main window
        let uplButton = (mainWin.FindName("UploadPageButton") :?> Button)
        uplButton.Click.Add(fun _ -> mainFncs.OnUploadButtonClicked)

        // Document Viewer button located in the main window
        let docViewCtrl = (mainWin.FindName("DocumentViewerPageButton") :?> Button)
        docViewCtrl.Click.Add(fun _ -> mainFncs.OnDocumentViewerButtonClicked)

        // The two of three pages existing on the main window
        let authenCtrl = (mainWin.FindName("AuthenticationPageControl") :?> UserControl)
        let uplCtrl = (mainWin.FindName("UploadPageControl") :?> UserControl)

        // Authentication button located in the Authentication page
        (authenCtrl.FindName("AuthenticateButton") :?> Button).Click.Add(fun _ -> autFncs.OnAuthenticateButtonClicked )

        // Ticket combobox located in the upload page
        (uplCtrl.FindName("TicketComboBox") :?> ComboBox).AddHandler(TextBoxBase.TextChangedEvent, TextChangedEventHandler(fun _ _ -> uplFncs.CheckIfFindSolutionAction))

        // Upload file button located in the upload page
        (uplCtrl.FindName("ChooseFileButton") :?> Button).Click.Add(fun _ -> uplFncs.OnChooseFileButtonClicked)
        (uplCtrl.FindName("ChooseFileButton") :?> Button).ToolTipOpening.Add(fun _ -> uplFncs.OnUploadFileHover)


        // Find solution button located in the upload page
        (uplCtrl.FindName("FindSolutionButton") :?> Button).Click.Add(fun _ -> uplFncs.OnFindSolutionButtonClicked)

        // Upload button located in the upload page
        (uplCtrl.FindName("UploadSolutionButton") :?> Button).Click.Add(fun _ -> uplFncs.OnUploadSolutionButtonClicked)
        (uplCtrl.FindName("UploadSolutionButton") :?> Button).ToolTipOpening.Add(fun _ -> uplFncs.OnUploadSolutioneHover)

        // Upload button located in the upload page
        (uplCtrl.FindName("Upload") :?> Button).Click.Add(fun _ -> uplFncs.OnUploadButtonClicked)

        // Button for opening solution located in the upload page
        (uplCtrl.FindName("OpenSolutionButton") :?> Button).Click.Add(fun _ -> uplFncs.OnOpenSolutionButtonClicked)

        // Button for clearing all logs
        (mainWin.FindName("ClearLogsButton") :?> Button).Click.Add(fun _ -> mainFncs.OnClearLogsButtonClicked)
