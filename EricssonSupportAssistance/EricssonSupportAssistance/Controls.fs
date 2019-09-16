namespace EricssonSupportAssistance

open System.Windows.Media

    
   
    type Controls() =
        
        // MainWindow controls
        member val InfoLogs = new ControlAtributes() with get, set 
        member val AuthenticatePage = new ControlAtributes() with get, set
        member val UploadPage = new ControlAtributes() with get, set 
        member val SearchPage = new ControlAtributes() with get, set

        // Authentization controls
        member val AuthenticationControl = new ControlAtributes() with get, set
        member val AuthenticateButton = new ControlAtributes() with get, set
        member val UserNameTextBox = new ControlAtributes(ItemsSource = [|"joshuagoldman94@gmail.com"|]) with get, set 
        member val PasswordTextBox = new ControlAtributes(ItemsSource = [|"flygplan5"|]) with get, set

        // Upload controls
        member val UploadPageControl = new ControlAtributes() with get, set
        member val UploadSolutionButton = new ControlAtributes() with get, set
        member val FindSolutionButton = new ControlAtributes() with get, set
        member val TicketComboBox = new ControlAtributes() with get, set
        member val OpenSolutionButton = new ControlAtributes() with get, set
        member val UploadButton = new ControlAtributes() with get, set
        
        // Search controls
        member val SearchPageControl = new ControlAtributes() with get, set


type InfoEventArgs(message : string, foreground : SolidColorBrush) =
    member this.Message = message
    member this.Foreground = foreground

type ObjectToPassEventArgs(objectToPass : obj) =
    member this.ObjectToPass = objectToPass