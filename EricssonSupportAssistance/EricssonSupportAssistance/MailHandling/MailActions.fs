namespace EricssonSupportAssistance.MailHandling

open ActiveUp.Net.Mail
open System
open System.ComponentModel

module MailActions =
    
    
    type IClient =
        abstract member Client : Imap4Client

    type MailExceptionEventArgs(errorMessage : string, message : string) =
        member val ErrorMessage = errorMessage
        member val Message = message

    type public MailRepository( mailserver : string,
                                port : int,
                                ssl : bool,
                                login : string,
                                password : string
                                ) as this =

        let client = new Imap4Client()
        let mailExceptionEv = new Event<MailExceptionEventArgs>()

        do
            ssl
            |> function 
                | _ when ssl -> try
                                    (this :> IClient).Client.ConnectSsl(mailserver, port)
                                with
                                    | :? ActiveUp.Net.Mail.Imap4Exception as x ->
                                                            mailExceptionEv.Trigger(MailExceptionEventArgs(x.Message,"Cannot make Ssl connection")) 
                                                            |> fun _ -> ""
                                
                                    | ex -> mailExceptionEv.Trigger(MailExceptionEventArgs(ex.Message, "Cannot make Ssl connection")) 
                                            |> fun _ -> ""

                | _ -> (this :> IClient).Client.Connect(mailserver, port)

            |> fun _ -> (this :> IClient).Client.Login(login, password)
            |> ignore

        interface IClient with  
            member this.Client with get() = client
        
        [<CLIEvent>]
        member this.MailExceptionOccured = mailExceptionEv.Publish

        member this.GetAlldMails (mailBox : string) = 
            
            this.GetMails(mailBox, "ALL")

        member this.GetUnreadMails (mailBox : string) = 
            
            this.GetMails(mailBox, "UNSEEN")

        member private this.GetMails (mailBox : string, searchPhrase : string) = 
            
            let mails = (this :> IClient).Client.SelectMailbox(mailBox)
            mails.SearchParse(searchPhrase)

