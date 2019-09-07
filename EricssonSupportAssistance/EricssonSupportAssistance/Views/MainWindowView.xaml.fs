namespace EricssonSupportAssistance.Views 

open FsXaml
open EricssonSupportAssistance.Functions
open EricssonSupportAssistance
open System.Windows
open System
open System.Windows.Controls
open System.Windows.Markup
type MainWindow = XAML<"MainWindow.xaml">

module newMainWindow =
    
    let mutable MainWin = new MainWindow()


    let Initialize = 
        
        let ShellViewControls = new MainWindowControls() 
        let mutable eventFuncs = new MainWindowFunctions(ShellViewControls)
        MainWin.DataContext <- eventFuncs.Sender

        let loginButton = MainWin.FindName("LoginButton") :?> Controls.Button
        loginButton.Click.Add(fun evArgs -> eventFuncs.OnLoginClicked evArgs)
        eventFuncs.MailRepo.MailExceptionOccured.Add(fun evArgs -> eventFuncs.OnMailExceptionOccurred evArgs)




        

