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

    member this.UplFncs 
        with get() = uplFncs
        and set(value) =
            if value <> uplFncs then uplFncs <- value

    member this.AutFncs 
        with get() = autFncs
        and set(value) =
            if value <> autFncs then autFncs <- value

    member this.SrchFncs 
        with get() = srchFncs
        and set(value) =
            if value <> srchFncs then srchFncs <- value

    member this.MainFncs 
        with get() = mainFncs
        and set(value) =
            if value <> mainFncs then mainFncs <- value

    member this.Sender 
        with get() = sender
        and set(value) = 
            if value <> sender then sender <- value

    member this.UpdateDatacontext (o : ObjectToPassEventArgs) =
        
        let controlAttrPropertyToChange = this.Sender.GetType().GetProperties(BindingFlags.Public ||| BindingFlags.Instance)
                                          |> Array.filter(fun prop ->  prop.PropertyType = typeof<ControlAtributes>)
                                          |> Array.find(fun prop -> prop.Name = o.nameWOwner.[0])
                                          |> fun x -> x.GetValue(this.Sender)

        controlAttrPropertyToChange.GetType().GetProperties(BindingFlags.Public ||| BindingFlags.Instance)
        |> Array.find(fun prop -> prop.Name = o.nameWOwner.[1])
        |> fun prop -> prop.SetValue(controlAttrPropertyToChange, o.Value)

    member this.InfoToRegister (e : InfoEventArgs) =

        mainFncs.Sender.InfoLogs.Text <- mainFncs.Sender.InfoLogs.Text + "\n> " + e.Message
        mainFncs.Sender.InfoLogs.TbForeground <- e.Foreground

    [<CLIEvent>]
    member this.UpdateDataContext = dataContextUpdateEv.Publish

    member this.listenToAllCtrlAttr = 
            
            let mutable sequenceOfControls = [||]

            let fillCtrls (infoArr : PropertyInfo[]) (o : obj) =
                
                infoArr
                |> Array.filter(fun prop -> prop.PropertyType = typeof<Controls>)
                |> Array.map(fun prop -> prop.GetValue(o) :?> Controls)
                |> fun x -> sequenceOfControls <- Array.append sequenceOfControls x

                
            let firstLevelProperties =
                
                this.GetType().GetProperties(BindingFlags.Public ||| BindingFlags.Instance)
                |> Array.filter(fun prop ->  prop.PropertyType = typeof<MainWindowFunctions> ||
                                             prop.PropertyType = typeof<UploadFunctions> ||
                                             prop.PropertyType = typeof<SearchPageFuncs> ||
                                             prop.PropertyType = typeof<AuthenticateFunctions>)
                |> Array.map(fun prop -> prop.GetValue(this))

            firstLevelProperties
            |> Array.iter(fun prop -> prop.GetType().GetProperties(BindingFlags.Public ||| BindingFlags.Instance)
                                      |> Array.filter(fun subProp -> subProp.PropertyType = typeof<Controls> ||
                                                                     subProp.PropertyType = typeof<Functions.TestOutputDefinitions>)
                                      |> fun subProps -> (fillCtrls subProps prop)
                                                         |> fun _ -> subProps
                                                                     |> Array.map(fun subProp -> subProp.GetValue(prop))
                                                         |> fun subPropsObj -> subPropsObj
                                                                               |> Array.iter(fun subPropObj -> subPropObj.GetType().GetProperties(BindingFlags.Public ||| BindingFlags.Instance)
                                                                                                               |> fun subSubProps -> fillCtrls subSubProps subPropObj))
                                                        
            
            let ctrlAttrsAll =
                
                sequenceOfControls
                |> Array.collect(fun ctrl -> ctrl.GetType().GetProperties(BindingFlags.Public ||| BindingFlags.Instance)
                                             |> Array.filter(fun subProp -> subProp.PropertyType = typeof<ControlAtributes>)
                                             |> Array.map(fun subProp -> subProp.GetValue(ctrl) :?> ControlAtributes))

            ctrlAttrsAll
            |> Array.iter(fun ctrlAttr -> ctrlAttr.UpdateDataContext.Add(fun evArgs -> this.UpdateDatacontext evArgs))

    member this.AddAllEvents() =

        this.listenToAllCtrlAttr

        mainFncs.InfoToAdd.Add(fun args -> this.InfoToRegister args)
        autFncs.InfoToAdd.Add(fun args -> this.InfoToRegister args)
        uplFncs.InfoToAdd.Add(fun args -> this.InfoToRegister args)

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
