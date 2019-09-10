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

module AuthenticateFuncs =

    type MainWindowFunctions with

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
                    this.InfoEv.Trigger(InfoEventArgs("Authentication failed", Brushes.IndianRed))

                        

                | _ when result.Result = Authentication.ResOptions.Pass ->
                    this.Sender.SearchPageControl.IsEnabled <- true
                    this.Sender.UploadPageControl.IsEnabled <- true
                    this.InfoEv.Trigger(InfoEventArgs("Authentication failed", Brushes.IndianRed))
                        
                | _ -> result |> ignore

        
        
       
    
    
        
        

