namespace EricssonSupportAssistance.MVVM.ViewModels 

open FsXaml
open EricssonSupportAssistance.Functions.MainWindowFuncs
open System.Windows
open System
open System.Windows.Controls
open System.Windows.Markup
type MainWindow = XAML<"MainWindow.xaml">

module newMainWindow =
    
    let mutable MainWin = new MainWindow()


    let Initialize = 
        
        let ShellViewControls = new MainWindowControls() 
        MainWin.DataContext <- ShellViewControls

        let loginButton = MainWin.FindName("LoginButton") :?> Controls.Button

        loginButton.Click.Add(fun evArgs -> OnLoginClicked MainWin evArgs)


        

