namespace EricssonSupportAssistance

open System.Windows.Media
    
    type InfoEventArgs(message : string, foreground : SolidColorBrush) =
        member this.Message = message
        member this.Foreground = foreground
    
    type Controls() =
        
        let mutable authenticatePageButton = new ControlAtributes("AuthenticatePageButton")
        let mutable uploadPageButton = new ControlAtributes("UploadPageButton")
        let mutable documentViewerPageButton = new ControlAtributes("DocumentViewerPageButton")
        let mutable infoLogs = new ControlAtributes("InfoLogs")
        let mutable authenticationPageControl = new ControlAtributes("AuthenticationPageControl")
        let mutable authenticateButton = new ControlAtributes("AuthenticateButton")
        let mutable userNameTextBox = new ControlAtributes("UserNameTextBox")
        let mutable passwordTextBox = new ControlAtributes("PasswordTextBox") 
        let mutable uploadPageControl = new ControlAtributes("UploadPageControl")
        let mutable uploadSolutionButton = new ControlAtributes("UploadSolutionButton")
        let mutable findSolutionButton = new ControlAtributes("FindSolutionButton")
        let mutable ticketComboBox = new ControlAtributes("TicketComboBox")
        let mutable openSolutionButton = new ControlAtributes("OpenSolutionButton")
        let mutable uploadButton = new ControlAtributes("UploadButton")
        let mutable chooseFileButton = new ControlAtributes("ChooseFileButton")
        let mutable documentViewer = new ControlAtributes("DocumentViewer")
        let mutable documentViewerPageControl = new ControlAtributes("DocumentViewerPageControl")
        let mutable clearLogsButton = new ControlAtributes("ClearLogsButton")

        // MainWindow controls
        member this.AuthenticatePageButton
            with get() = authenticatePageButton 
            and set(value) = 
                if value <> authenticatePageButton then authenticatePageButton <- value

        member this.UploadPageButton
            with get() = uploadPageButton 
            and set(value) = 
                if value <> uploadPageButton then uploadPageButton <- value
                
    
        member this.DocumentViewerPageButton
            with get() = documentViewerPageButton 
            and set(value) = 
                if value <> documentViewerPageButton then documentViewerPageButton <- value
                  

        member this.InfoLogs
            with get() = infoLogs 
            and set(value) = 
                if value <> infoLogs then infoLogs <- value

        member this.ClearLogsButton
            with get() = clearLogsButton 
            and set(value) = 
                if value <> clearLogsButton then clearLogsButton <- value
                  

        // Authentization controls
        member this.AuthenticationPageControl
            with get() = authenticationPageControl 
            and set(value) = 
                if value <> authenticationPageControl then authenticationPageControl <- value
                  

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
                  
        member this.ChooseFileButton
            with get() = chooseFileButton 
            and set(value) = 
                if value <> chooseFileButton then chooseFileButton <- value
        
        // Document Viewer controls

        member this.DocumentViewerPageControl
            with get() = documentViewerPageControl 
            and set(value) = 
                if value <> documentViewerPageControl then documentViewerPageControl <- value

        member this.DocumentViewer
            with get() = documentViewer 
            and set(value) = 
                if value <> documentViewer then documentViewer <- value
                  
