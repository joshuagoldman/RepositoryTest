namespace EricssonSupportAssistance.Functions 

open FsXaml
open System.Windows
open System.Windows.Controls
open System.Windows.Media
open EricssonSupportAssistance.Functions.Authentication
open EricssonSupportAssistance
open EricssonSupportAssistance.MailHandling.MailActions
open EricssonSupportAssistance.XamlFiles
open System


type AuthenticatePageFunctions( mainWindowSender : MainWindowControls) =

    let mutable sender = new AuthenticatePageControls()
    let mutable infoEv = new Event<InfoEventArgs>()
    let mutable objPassingEv = new Event<ObjectToPassEventArgs>()
    let mutable mainWindowSender = mainWindowSender
    
    member this.MainWindowSender 
        with get() = mainWindowSender
        and set(value) = 
            if value <> mainWindowSender then mainWindowSender <- value

    member this.Sender 
        with get() = sender
        and set(value) = 
            if value <> sender then sender <- value
        

    [<CLIEvent>]
    member this.InfoToAdd = infoEv.Publish

    [<CLIEvent>]
    member this.ObjectToPass = objPassingEv.Publish

    member this.OnAuthenticateButtonClicked =
        
        let info = {UserName = {AllowedStrings = [|"joshuagoldman94@gmail.com"|]; ActualString = this.Sender.UserNameTextBox.Text};
                    Password = {AllowedStrings = [|"imale"|]; ActualString = this.Sender.PasswordTextBox.Text}
                        }
        let result = getAuthentication info

        infoEv.Trigger(InfoEventArgs(result.Message, Brushes.Black))

        result
        |> function
            | _ when result.Result = Authentication.ResOptions.Fail -> 

                let mutable objToPassSearchPage = new SearchPageControls()
                objToPassSearchPage.SearchPageControl.IsEnabled <- false
                let mutable objToPassUploadPage = new UploadPageControls()
                objToPassUploadPage.UploadPageControl.IsEnabled <- false
                objPassingEv.Trigger(ObjectToPassEventArgs([|objToPassSearchPage ; objToPassUploadPage|],
                                                           "Authentication failed:\n" +
                                                           "sending object that'll make search" + 
                                                           "page and upload page unavailable"))

            | _ when result.Result = Authentication.ResOptions.Pass ->
                
                let mutable objToPassSearchPage = new SearchPageControls()
                objToPassSearchPage.SearchPageControl.IsEnabled <- true
                let mutable objToPassUploadPage = new UploadPageControls()
                objToPassUploadPage.UploadPageControl.IsEnabled <- true
                objPassingEv.Trigger(ObjectToPassEventArgs([|objToPassSearchPage ; objToPassUploadPage|],
                                                           "Authentication passed:\n" +
                                                           "sending object that'll make search" + 
                                                           "page and upload page available"))
            | _ -> result |> ignore

        
        
       
    
    
        
        

