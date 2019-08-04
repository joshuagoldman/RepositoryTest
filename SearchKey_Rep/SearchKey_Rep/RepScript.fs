namespace SearchKeyRep

open SearchKey_GUI.Methods
open SearchKey_GUI
open System.Threading
open System.Windows
open SearchKeyRep
open System.Windows.Controls

module RepeatSearchKey =

    let OpenNGet (init : InitializeClasses) = 

        let box = init.AllWind.Main.FindName("SearchKey") :?> TextBox

        // Template search key.
        box.Text <-  "ERS BB units with ICM CCR PLL issue, 1/-68; 1a, Rev B";

        
        init.ChoicAct.WindowText2ControlInfoClass()
        init.ChoicAct.CheckTreeExistence()

    let GenerateNewKey (init : InitializeClasses) = 
        init.ChoicAct.NotifyMandatoryFields <- ChoiceActions.TurnEmptyToRed.No;
        init.ChoicAct.WindowText2ControlInfoClass();
        init.ChoicAct.Write32App();
       

    type SearchKeyElements = 
        { SearchKey : string 
          Variable : string
          Filter : string
          Date : string
          Infotext : string
          Product : string
          CriteriaReferenceWithRevision : string
          }

    let SearchKeyElements4Use = 
        


    let SaveAction (init : InitializeClasses) = 
        init.ChoicAct.SaveFile <- ChoiceActions.SaveXmlFile.Yes;
        init.ChoicAct.SaveFileActions(); 


    let PropertyProviding (searchKeyElements4Use : SearchKeyElements) (init : InitializeClasses)  = 
        
        (init.AllWind.Main.FindName("SearchKey") :?> TextBox).Text <- searchKeyElements4Use.SearchKey
        (init.AllWind.Main.FindName("InputDateWithIndex") :?> TextBox).Text <- searchKeyElements4Use.Date
        (init.AllWind.ExWindow.FindName("SearchFilesFilter") :?> TextBox).Text <- searchKeyElements4Use.Filter
        (init.AllWind.ExWindow.FindName("Variable") :?> TextBox).Text <- searchKeyElements4Use.Variable
        (init.AllWind.InfoTextWin.FindName("Infotext") :?> TextBox).Text <- searchKeyElements4Use.Infotext
        (init.AllWind.Main.FindName("CriteriaReferenceWithRevision") :?> TextBox).Text <- searchKeyElements4Use.CriteriaReferenceWithRevision
        
    let executeSeq (searchKeyElements4Use : SearchKeyElements) (init : InitializeClasses) =  
        
        OpenNGet init  
        PropertyProviding searchKeyElements4Use init
        GenerateNewKey init
        SaveAction init

    let PerformFullAction (searchKeyElements4Use : Set<SearchKeyElements>) (init : InitializeClasses)  =
        searchKeyElements4Use
        |> Set.iter(fun x -> executeSeq x init)
    
    let launcher() = 
        let window = new MainWindow()
        window.Show()
        let init = window.ClassInit;
        PerformFullAction SearchKeyElements4Use init 

    let thread = new Thread( launcher)

    let h = new ManualResetEventSlim()

    thread.SetApartmentState(ApartmentState.STA);
    thread.IsBackground <- true
    thread.Start()
    h.Wait()
    h.Dispose()