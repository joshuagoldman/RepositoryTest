namespace EricssonSupportAssistance

open EricssonSupportAssistance.Views
module App  =

    open System  
    open FsXaml  

    type App = XAML<"App.xaml">  

    [<EntryPoint;STAThread>]  
    let main argv =
        newMainWindow.Initialize
        App().Run(newMainWindow.MainWin)
                                   

        
      