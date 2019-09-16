namespace EricssonSupportAssistance.EventHandlingFuncs  

open FsXaml
open System.Windows
open EricssonSupportAssistance
open System.Reflection
open System.Windows.Forms
open EricssonSupportAssistance.XamlFiles
open EricssonSupportAssistance.Functions
open EricssonSupportAssistance.MailHandling.MailActions
open System
open System.Windows.Media
open System.Windows.Controls
open System.IO
open Microsoft.Win32


 type UploadFunctions() =
    
    let mutable sender = new Controls()

    let mutable uploadFile = ""

    let mutable solution = ""   

    let mutable infoEv = new Event<InfoEventArgs>()

    let mutable dataContextUpdateEv = new Event<ObjectToPassEventArgs>()

    do
        TestOutputDefinitions.InfoToAdd.Add(fun evArgs -> infoEv.Trigger(evArgs))
     
    member this.Sender 
        with get() = sender
        and set(value) = 
            if value <> sender then sender <- value
    
    member internal this.Uploadfile
        with get() = uploadFile
        and set(value) = 
            if value <> uploadFile then uploadFile <- value

    member internal this.Solution
        with get() = solution
        and set(value) = 
            if value <> solution then solution <- value


    [<CLIEvent>]
    member this.InfoToAdd = infoEv.Publish

    [<CLIEvent>]
    member this.UpdateDataContext = dataContextUpdateEv.Publish


    member this.CheckIfFindSolutionAction  =
               
        None
        |> function
                       
            | _ when this.Sender.TicketComboBox.Text <> "" && this.Uploadfile <> "" ->
                            
                this.Sender.TicketComboBox.ItemsSource
                |> Seq.exists(fun t -> t = this.Sender.TicketComboBox.Text)
                |> function
                    | res when res = true ->

                        this.Sender.FindSolutionButton.IsEnabled <- true 

                        None
                        |> function
                                
                            | _ when this.Solution <> "" ->
                                    
                                this.Sender.UploadButton.IsEnabled <- true

                            | _ -> None |> ignore

                    | _ -> None |> ignore

            | _ -> None |> ignore
               

                        
    member this.OnFindSolutionButtonClicked =
            
        this.Solution <- TestOutputDefinitions.tryFindSolution this.Sender.TicketComboBox.Text ""

    member this.OnChooseFileButtonClicked =
            

        None
        |>function

            | _ when this.Uploadfile = ""  -> 
                
                this.Uploadfile <- TestOutputDefinitions.file
                
                
            
            | _  ->
                 
                let answer = MessageBox.Show("Hmmm...An upload file already exists, are you certain you want to change the current one?",
                                            "Warning",
                                            MessageBoxButton.YesNoCancel,
                                            MessageBoxImage.Question)
                                                     
                match answer with
                 
                | _ when answer = MessageBoxResult.Yes ->

                    this.Uploadfile <- TestOutputDefinitions.file

                | _ -> None |> ignore

    member this.OnUploadButtonClicked =
            
        this.Solution <- TestOutputDefinitions.tryFindSolution this.Sender.TicketComboBox.Text this.Solution
                



        
     
        
        

