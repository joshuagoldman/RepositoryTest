namespace EricssonSupportHelperFSharp

open EricssonSupportHelperFSharp.Models
open Caliburn.Micro
open System.Xaml
open System.Windows


module ViewModels =



    type SearchTicketViewModel() = 
        inherit Screen()
        member val SearchPhraseComboBox = new ControlAttributes() with get, set
        member val TicketOptionComboBox = new ControlAttributes() with get, set
        member val InformationMessage = new ControlAttributes() with get, set

    type ShellViewModel() = 
        inherit Conductor<obj>()

        let loadPage = 
            base.ActivateItemAsync(new SearchTicketViewModel())

        member val UpdateButton = new ControlAttributes() with get, set
        member val LoginButton = new ControlAttributes() with get, set
        member val LoginTextBox = new ControlAttributes() with get, set
        member val TicketComboBox = new ControlAttributes() with get, set
        member val SearchTicketButton = new ControlAttributes() with get, set
        member val OpenSolutionButon = new ControlAttributes() with get, set
        member val SolutionMessage = new ControlAttributes() with get, set

    type Bootstarpper() = 
        inherit BootstrapperBase()
        
        do base.Initialize()

        override this.OnStartup(sender : obj, e : StartupEventArgs) =
            
            base.DisplayRootViewFor<ShellViewModel>()
            |> ignore
            
            

        

