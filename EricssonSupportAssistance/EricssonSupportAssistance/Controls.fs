namespace EricssonSupportAssistance

open System.Windows.Media
    
    type InfoEventArgs(message : string, foreground : SolidColorBrush) =
        member this.Message = message
        member this.Foreground = foreground
    
    type Controls() =
        
        let mutable authenticatePage = new ControlAtributes("AuthenticatePage")
        let mutable uploadPage = new ControlAtributes("UploadPage")
        let mutable searchPage = new ControlAtributes("SearchPage")
        let mutable infoLogs = new ControlAtributes("InfoLogs")
        let mutable authenticationControl = new ControlAtributes("AuthenticationControl")
        let mutable authenticateButton = new ControlAtributes("AuthenticateButton")
        let mutable userNameTextBox = new ControlAtributes("UserNameTextBox")
        let mutable passwordTextBox = new ControlAtributes("PasswordTextBox") 
        let mutable uploadPageControl = new ControlAtributes("UploadPageControl")
        let mutable uploadSolutionButton = new ControlAtributes("UploadSolutionButton")
        let mutable findSolutionButton = new ControlAtributes("FindSolutionButton")
        let mutable ticketComboBox = new ControlAtributes("TicketComboBox")
        let mutable openSolutionButton = new ControlAtributes("OpenSolutionButton")
        let mutable uploadButton = new ControlAtributes("UploadButton")
        let mutable searchPageControl = new ControlAtributes("SearchPageControl")

        // MainWindow controls
        member this.AuthenticatePage
            with get() = authenticatePage 
            and set(value) = 
                if value <> authenticatePage then authenticatePage <- value

        member this.UploadPage
            with get() = uploadPage 
            and set(value) = 
                if value <> uploadPage then uploadPage <- value
                
    
        member this.SearchPage
            with get() = searchPage 
            and set(value) = 
                if value <> searchPage then searchPage <- value
                  

        member this.InfoLogs
            with get() = infoLogs 
            and set(value) = 
                if value <> infoLogs then infoLogs <- value
                  

        // Authentization controls
        member this.AuthenticationControl
            with get() = authenticationControl 
            and set(value) = 
                if value <> authenticationControl then authenticationControl <- value
                  

        member this.AuthenticateButton
            with get() = authenticateButton 
            and set(value) = 
                if value <> authenticateButton then authenticateButton <- value
                  

        member this.UserNameTextBox
            with get() = userNameTextBox 
            and set(value) = 
                if value <> userNameTextBox then userNameTextBox <- value
                  

        member this.PasswordTextBox
            with get() = passwordTextBox 
            and set(value) = 
                if value <> passwordTextBox then passwordTextBox <- value
                  

        // Upload controls
        member this.UploadPageControl
            with get() = uploadPageControl 
            and set(value) = 
                if value <> uploadPageControl then uploadPageControl <- value
                  

        member this.UploadSolutionButton
            with get() = uploadSolutionButton 
            and set(value) = 
                if value <> uploadSolutionButton then uploadSolutionButton <- value
                 
                
        member this.FindSolutionButton
            with get() = findSolutionButton 
            and set(value) = 
                if value <> findSolutionButton then findSolutionButton <- value
                  

        member this.TicketComboBox
            with get() = ticketComboBox 
            and set(value) = 
                if value <> ticketComboBox then ticketComboBox <- value
                  

        member this.OpenSolutionButton
            with get() = openSolutionButton 
            and set(value) = 
                if value <> openSolutionButton then openSolutionButton <- value
                  

        member this.UploadButton
            with get() = uploadButton 
            and set(value) = 
                if value <> uploadButton then uploadButton <- value
                  
        
        // Search controls
        member this.SearchPageControl
            with get() = searchPageControl 
            and set(value) = 
                if value <> searchPageControl then searchPageControl <- value
                  
