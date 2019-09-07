namespace EricssonSupportAssistance.Functions 

open FsXaml
open System.Windows
open EricssonSupportAssistance
open EricssonSupportAssistance.MailHandling.MailActions
open System
open System.Windows.Controls
open System.Windows.Markup

type MainWindowFunctions(sender : MainWindowControls) =
    
    let mutable mailRepo = new MailRepository("imap.gmail.com",
                                              993,
                                              true,
                                              sender.UserNameTextBox.Text,
                                              sender.PasswordTextBox.Text)
    let mutable senderMutable = sender

    member this.Sender 
        with get() = senderMutable
        and set(value) = 
            if value <> senderMutable then senderMutable <- value

    member this.MailRepo 
        with get() = mailRepo
        and set(value) = 
            if value <> mailRepo then mailRepo <- value

    member this.OnLoginClicked (e : RoutedEventArgs) =
        
        let mails = mailRepo.GetAlldMails("inbox")
        this.Sender.InfoLogs.Text <- "Received all Mails"

    member this.OnMailExceptionOccurred (e : MailExceptionEventArgs) =
        
        this.Sender.InfoLogs.Text <- String.Format("Mail exception message:\n{0}\nOwn message:\n{1}\n",
                                                    e.ErrorMessage, e.Message)
       
    
    
        
        

