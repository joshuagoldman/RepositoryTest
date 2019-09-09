namespace EricssonSupportAssistance

open EricssonSupportAssistance.MainWindow
module App  =

    open System  
    open FsXaml  

    type App = XAML<"App/App.xaml">  

    [<EntryPoint;STAThread>]  
    let main argv =
        let win = new MainWindowFunctions()
        App().Run(win.MainWin)
                                   

        
      