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
        
        inherit ControlBase()

        member this.OnAuthenticateButtonClicked =
        
            let info = {UserName = {AllowedStrings = [|"joshuagoldman94@gmail.com"|]; ActualString = this.Sender.UserNameTextBox.Text};
                        Password = {AllowedStrings = [|"imale"|]; ActualString = this.Sender.PasswordTextBox.Text}
                            }
            let result = getAuthentication info

            result
            |> function
                | _ when result.Result = Authentication.ResOptions.Fail -> 
                    this.Sender.DocumentViewerPageControl.IsEnabled <- false
                    this.Sender.UploadPageControl.IsEnabled <- false
                    this.infoEv.Trigger(InfoEventArgs(result.Message, Brushes.IndianRed))

                        

                | _ when result.Result = Authentication.ResOptions.Pass ->
                    this.Sender.DocumentViewerPageControl.IsEnabled <- true
                    this.Sender.UploadPageControl.IsEnabled <- true
                    this.infoEv.Trigger(InfoEventArgs(result.Message, Brushes.IndianRed))
                        
                | _ -> result |> ignore

          
       
    
    
        
        

