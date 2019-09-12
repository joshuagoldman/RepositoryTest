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


 module definitions =
     let mutable uploadFile = ""

     let mutable solution = ""   

    
type MainWindowFunctions with
        
    member internal this.Uploadfile
        with get() = definitions.uploadFile
        and set(value) = 
            if value <> definitions.uploadFile then definitions.uploadFile <- value

    member internal this.Solution
        with get() = definitions.solution
        and set(value) = 
            if value <> definitions.solution then definitions.solution <- value

    member this.AddUploadPageEvents =
            
        (this.MainWin.FindName("TicketComboBox") :?> TextBox).TextChanged.Add(fun _ -> this.CheckIfFindSolutionAction)
        (this.MainWin.FindName("FindSolutionButton") :?> Button).TextInput.Add(fun _ -> this.OnFindSolutionButtonClicked)
        (this.MainWin.FindName("ChooseFileButton") :?> Button).Click.Add(fun _ -> this.OnChooseFileButtonClicked )
        (this.MainWin.FindName("UploadButton") :?> Button).Click.Add(fun _ -> this.OnChooseFileButtonClicked )

    member this.CheckIfFindSolutionAction =
               

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

                this.Uploadfile <- TestOutputDefinitions.uploadFile
            
            | _  ->
                 
                let answer = MessageBox.Show("Hmmm...An upload file already exists, are you certain you want to change the current one?",
                                            "Warning",
                                            MessageBoxButton.YesNoCancel,
                                            MessageBoxImage.Question)
                                                     
                match answer with
                 
                | _ when answer = MessageBoxResult.Yes ->

                    this.Uploadfile <- TestOutputDefinitions.uploadFile

                | _ -> None |> ignore

    member this.OnUploadButtonClicked =
            
        this.Solution <- TestOutputDefinitions.tryFindSolution this.Sender.TicketComboBox.Text this.Solution
                



        
     
        
        

