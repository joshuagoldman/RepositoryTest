namespace EricssonSupportAssistance.EventHandlingFuncs  

open FsXaml
open System.Windows
open System.Windows.Controls
open System.Windows.Media
open EricssonSupportAssistance.Functions.Authentication
open EricssonSupportAssistance
open EricssonSupportAssistance.Functions
open EricssonSupportAssistance.MailHandling.MailActions
open EricssonSupportAssistance.XamlFiles
open System

type AuthenticateFunctions() =
        
        let mutable sender = new Controls()
        let mutable dataContextUpdateEv = new Event<ObjectToPassEventArgs>()
        let mutable infoEv = new Event<InfoEventArgs>()

        member this.Sender 
            with get() = sender
            and set(value) = 
                if value <> sender then sender <- value

        [<CLIEvent>]
        member this.InfoToAdd = infoEv.Publish

        [<CLIEvent>]
        member this.UpdateDataContext = dataContextUpdateEv.Publish

        member this.OnAuthenticateButtonClicked =
        
            let info = {UserName = {AllowedStrings = [|"joshuagoldman94@gmail.com"|]; ActualString = this.Sender.UserNameTextBox.Text};
                        Password = {AllowedStrings = [|"imale"|]; ActualString = this.Sender.PasswordTextBox.Text}
                            }
            let result = getAuthentication info

            result
            |> function
                | _ when result.Result = Authentication.ResOptions.Fail -> 
                    this.Sender.SearchPageControl.IsEnabled <- false
                    this.Sender.UploadPageControl.IsEnabled <- false
                    infoEv.Trigger(InfoEventArgs("Authentication failed", Brushes.IndianRed))

                        

                | _ when result.Result = Authentication.ResOptions.Pass ->
                    this.Sender.SearchPageControl.IsEnabled <- true
                    this.Sender.UploadPageControl.IsEnabled <- true
                    infoEv.Trigger(InfoEventArgs("Authentication suceeded!", Brushes.IndianRed))
                        
                | _ -> result |> ignore

            
            dataContextUpdateEv.Trigger(ObjectToPassEventArgs(this.Sender))
       
    
    
        
        

