namespace EricssonSupportAssistance

open System.Windows.Media

    
   
    type Controls() =
        
        // MainWindow controls
        member this.InfoLogs = new ControlAtributes() 
        member this.AuthenticatePage = new ControlAtributes()
        member this.UploadPage = new ControlAtributes() 
        member this.SearchPage = new ControlAtributes() 

        // Authentization controls
        member this.AuthenticationControl = new ControlAtributes() 
        member this.AuthenticateButton = new ControlAtributes() 
        member this.UserNameTextBox = new ControlAtributes(ItemsSource = [|"joshuagoldman94@gmail.com"|]) 
        member this.PasswordTextBox = new ControlAtributes(ItemsSource = [|"flygplan5"|]) 

        // Upload controls
        member this.UploadPageControl = new ControlAtributes() 

        // Search controls
        member this.SearchPageControl = new ControlAtributes() 


type InfoEventArgs(message : string, foreground : SolidColorBrush) =
    member this.Message = message
    member this.Foreground = foreground

type ObjectToPassEventArgs(objectsToPass : seq<obj>, message : string) =
    member this.ObjectsToPass = objectsToPass
    member this.Message = message