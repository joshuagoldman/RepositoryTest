namespace EricssonSupportAssistance.ViewModels

open EricssonSupportAssistance.Models
 
 type SearchTicketViewModel() = 
     
     member val SearchPhraseComboBox = new ControllAttributes() with get, set
     member val AlternativesComboBox = new ControllAttributes() with get, set
     member val InfoMessage = new ControllAttributes() with get, set

type ShellViewModel() = 
    
    member val UpdateButton = new ControllAttributes() with get, set
    member val LoginButton = new ControllAttributes() with get, set
    member val UsernameTextBlock = new ControllAttributes() with get, set
    member val PasswordTextBlock = new ControllAttributes() with get, set
    member val TicketTextBlock = new ControllAttributes() with get, set
    member val SolutionTextBlock = new ControllAttributes() with get, set
    member val SearchTicketButton = new ControllAttributes() with get, set
    member val InfoMessage = new ControllAttributes() with get, set


