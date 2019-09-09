namespace EricssonSupportAssistance

open System.Windows.Media

type MainWindowControls() =
    member val InfoLogs = new ControlAtributes() with get, set
    member val AuthenticatePage = new ControlAtributes() with get, set
    member val UploadPage = new ControlAtributes() with get, set
    member val SearchPage = new ControlAtributes() with get, set
    
    
type UploadPageControls() =
    member val UploadPageControl = new ControlAtributes() with get, set
    member val UserNameTextBox = new ControlAtributes(ItemsSource = [|"joshuagoldman94@gmail.com"|]) with get, set
    member val PasswordTextBox = new ControlAtributes(ItemsSource = [|"flygplan5"|]) with get, set
    member val InfoLogs = new ControlAtributes() with get, set  
    
type AuthenticatePageControls() =
    member val AuthenticationControl = new ControlAtributes() with get, set
    member val AuthenticateButton = new ControlAtributes() with get, set
    member val UserNameTextBox = new ControlAtributes(ItemsSource = [|"joshuagoldman94@gmail.com"|]) with get, set
    member val PasswordTextBox = new ControlAtributes(ItemsSource = [|"flygplan5"|]) with get, set

type SearchPageControls() =
    member val SearchPageControl = new ControlAtributes() with get, set
    member val LoginButton = new ControlAtributes() with get, set
    member val UserNameTextBox = new ControlAtributes(ItemsSource = [|"joshuagoldman94@gmail.com"|]) with get, set
    member val PasswordTextBox = new ControlAtributes(ItemsSource = [|"flygplan5"|]) with get, set
    member val InfoLogs = new ControlAtributes() with get, set

type InfoEventArgs(message : string, foreground : SolidColorBrush) =
    member val Message = message
    member val Foreground = foreground

type ObjectToPassEventArgs(objectsToPass : seq<obj>, message : string) =
    member val ObjectsToPass = objectsToPass
    member val Message = message