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
open System.Diagnostics
open System.Xml.XPath


 type UploadFunctions() =
    
    inherit ControlBase()

    let mutable uploadFile = ""
    let mutable solution = ""   
    let mutable tstOutput = new TestOutputDefinitions()

    member this.TstOutput 
        with get() = tstOutput
        and set(value) = 
            if value <> tstOutput then tstOutput <- value
    
    member internal this.Uploadfile
        with get() = uploadFile
        and set(value) = 
            if value <> uploadFile then uploadFile <- value

    member internal this.Solution
        with get() = solution
        and set(value) = 
            if value <> solution then solution <- value


    member this.CheckIfFindSolutionAction  =
               
        None
        |> function
                       
            | _ when this.Sender.TicketComboBox.Text <> "" ->
                            
                this.Sender.TicketComboBox.ItemsSource
                |> Seq.exists(fun t -> t = this.Sender.TicketComboBox.Text)
                |> function
                    | res when res = true ->

                        this.Sender.UploadButton.IsEnabled <- false
                        this.Sender.OpenSolutionButton.IsEnabled <- true 
                        let methodObtained =  this.TstOutput.FailedTMDoc.XPathSelectElements("*//Ticket")
                                              |> Seq.find(fun element -> element.FirstAttribute.Value = this.Sender.TicketComboBox.Text)
                                              |> fun element -> element.XPathSelectElement("(..)[last()]")
                                              
                        this.TstOutput.SolutionFileName <- "Ticket no " + methodObtained.XPathSelectElement("Ticket").FirstAttribute.Value
                        this.Solution <- methodObtained.XPathSelectElement("Solution").FirstAttribute.Value

                    | _ when this.Solution <> "" ->
                        
                        this.Sender.OpenSolutionButton.IsEnabled <- true
                        if this.Uploadfile <> ""
                        then this.Sender.UploadButton.IsEnabled <- true

                    | _ -> 
                        
                        this.Sender.OpenSolutionButton.IsEnabled <- false

            | _ -> this.Sender.UploadButton.IsEnabled <- false
               

                        
    member this.OnFindSolutionButtonClicked =
        
        None
        |> function
            
           | _ when this.Solution <> "" ->
            
                let answer = MessageBox.Show(String.Format("Hmmm...A solution file already exists, are you certain you wish to find" +
                                                            " a solution even though a solution may already exist?"),
                                            "Warning",
                                            MessageBoxButton.YesNoCancel,
                                            MessageBoxImage.Question)

                answer
                |> function
                   
                   | _ when answer = MessageBoxResult.Yes -> 
                        
                        let currSol = this.TstOutput.tryFindSolution (this.Sender.TicketComboBox.Text) (this.Uploadfile) ""
                        if  currSol <> ""
                        then this.Solution <- currSol
                        this.Sender.OpenSolutionButton.IsEnabled <- true 
                   
                   | _ -> None |> ignore

           | _ -> 
                
                let answer = MessageBox.Show(String.Format("Hold yo horses..." +
                                                           "Do you really want to find a solution, or was this purely a mistake? ;)",
                                                            this.Solution),
                                            "Warning",
                                            MessageBoxButton.YesNoCancel,
                                            MessageBoxImage.Question)
                answer
                |> function
                   
                   | _ when answer = MessageBoxResult.Yes -> 
                        
                        this.Solution <- this.TstOutput.tryFindSolution (this.Sender.TicketComboBox.Text) (this.Uploadfile) ""
                        this.Sender.OpenSolutionButton.IsEnabled <- true 
                   
                   | _ -> None |> ignore

    member this.OnOpenSolutionButtonClicked =
          
        this.infoEv.Trigger(InfoEventArgs(this.Solution, Brushes.Black))

    member this.OnChooseFileButtonClicked =
            

        None
        |>function

            | _ when this.Uploadfile = ""  -> 
                
                this.Uploadfile <- this.TstOutput.GetFile FileType.UploadFile
                this.Sender.FindSolutionButton.IsEnabled <- true
                
                
            
            | _  ->
                 
                let answer = MessageBox.Show("Hmmm...An upload file already exists, are you certain you want to change the current one?",
                                            "Warning",
                                            MessageBoxButton.YesNoCancel,
                                            MessageBoxImage.Question)
                                                     
                match answer with
                 
                | _ when answer = MessageBoxResult.Yes ->

                    this.Uploadfile <- this.TstOutput.GetFile FileType.UploadFile

                | _ -> None |> ignore

        this.CheckIfFindSolutionAction

    member this.OnUploadSolutionButtonClicked =
            

        None
        |>function

            | _ when this.Solution = ""  -> 
                
                this.Solution <- this.TstOutput.GetFile FileType.Solution
                this.Sender.OpenSolutionButton.IsEnabled <- true
                
                
            
            | _  ->
                 
                let answer = MessageBox.Show("Hmmm...An upload solution file already exists, are you certain you want to change the current one?",
                                            "Warning",
                                            MessageBoxButton.YesNoCancel,
                                            MessageBoxImage.Question)
                                                     
                match answer with
                 
                | _ when answer = MessageBoxResult.Yes ->

                    this.Solution <- this.TstOutput.GetFile FileType.Solution

                | _ -> None |> ignore

        this.CheckIfFindSolutionAction

    member this.OnUploadButtonClicked =
        
        this.TstOutput.tryFindSolution (this.Sender.TicketComboBox.Text) (this.Uploadfile) (this.Solution)
        |> fun _ -> None |> ignore
        this.Sender.OpenSolutionButton.IsEnabled <- true
                
    member this.OnUploadFileHover =
        
        this.Sender.ChooseFileButton.ToolTip <- String.Format("Current upload file: {0}\n", this.TstOutput.UploadFileName)
        

    member this.OnUploadSolutioneHover =
        
        this.Sender.UploadSolutionButton.ToolTip <- String.Format("Current solution file: {0}\n" +
                                                                  "Click \"Open Solution\" " +
                                                                  "to view solution file text", this.TstOutput.SolutionFileName)


        
     
        
        

