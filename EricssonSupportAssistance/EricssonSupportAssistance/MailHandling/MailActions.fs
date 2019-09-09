namespace EricssonSupportAssistance.MailHandling

open ActiveUp.Net.Mail
open System
open System.ComponentModel

module MailActions =
    
    
    type IClient =
        abstract member Client : Imap4Client

    type MailInfoEventArgs(errorMessage : string, message : string) =
        member val ErrorMessage = errorMessage
        member val Message = message

    type public MailRepository( mailserver : string,
                                port : int,
                                ssl : bool
                                ) =

        let client = new Imap4Client()
        let mailInfo = new Event<MailInfoEventArgs>()

        member this.Login(userName : string, password : string) =
            mailInfo.Trigger(MailInfoEventArgs("", "Checking ssl..."))
            try
                ssl
                |> function 
                    | _ when ssl -> (this :> IClient).Client.ConnectSsl(mailserver, port)
                    | _ -> (this :> IClient).Client.Connect(mailserver, port)
                |> fun _ -> (this :> IClient).Client.Login(userName, password)
            with
                 | ex -> mailInfo.Trigger(MailInfoEventArgs(ex.Message, "Cannot make Ssl connection")) 
                         |> fun _ -> "Login Failed"


        interface IClient with  
            member this.Client with get() = client
        
        [<CLIEvent>]
        member this.MailInfo = mailInfo.Publish

        member this.GetAlldMails (mailBox : string) = 
            
            this.GetMails(mailBox, "ALL")

        member this.GetUnreadMails (mailBox : string) = 
            
            this.GetMails(mailBox, "UNSEEN")

        member private this.GetMails (mailBox : string, searchPhrase : string) = 
            
            let mails = (this :> IClient).Client.SelectMailbox(mailBox)
            mails.SearchParse(searchPhrase)

