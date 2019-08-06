namespace SearchKeyRep

open SearchKey_GUI.Methods
open SearchKey_GUI
open System.Threading
open SearchKeyRep.DocumentReading.SetUpCase_SK_1_68_2
open SearchKeyRep.DocumentReading.SetUpCase_SK_1_68_1
open SearchKeyRep.Methods.SK_1_68_Methods
open System.Windows.Controls

module RepeatSearchKey =
    
    let keyChunkInfoRule1 = 
        [|
          "\(A6P\).*";
          "ICM CCR PLL.*"
          |]

    let varsInfo = [|
                      {Key = tablePattern ; ChunkStart = "Table 1-A: rules 1a to 1e valid for:" ; ChunkEnd = "Table 1-B: rules 1f to 1h valid for:"};
                      {Key = tablePattern ; ChunkStart = "Table 1-B: rules 1f to 1h valid for:" ; ChunkEnd = "Table 1-C: rules 1i to 1k valid for:"};
                      {Key = tablePattern ; ChunkStart = "Table 1-C: rules 1i to 1k valid for:" ; ChunkEnd = "2.4.3.2 Rule 2: Valid when no"}
                      |]

    Rule1Arr varsInfo keyChunkInfoRule1

    let OpenNGet (init : InitializeClasses) = 

        let box = init.AllWind.Main.FindName("SearchKey") :?> ComboBox

        // Template search key.
        box.Text <-  "ERS BB units with ICM CCR PLL issue, 1/-68; 1a, Rev B";

        
        init.ChoicAct.WindowText2ControlInfoClass()
        init.ChoicAct.CheckTreeExistence()

    let GenerateNewKey (init : InitializeClasses) = 
        init.ChoicAct.NotifyMandatoryFields <- ChoiceActions.TurnEmptyToRed.No;
        init.ChoicAct.WindowText2ControlInfoClass();
        init.ChoicAct.Write32App();

    let SearchKeyElements4Use = 
        
        let keyChunkProd = 
            { Key = "KDU" ;
              ChunkStart = "2.1 Product number" ;
              ChunkEnd = "2.2 Serial number";}

        let keyChunkInfo = 
            [|
              { Key = "\(A6M\)|\(A6N\)" ;
              ChunkStart = "2.4.2 Step 2: Collect other PLL traces" ;
              ChunkEnd = "Criteria 2: Trace from system log"};

              { Key = "(\<A6M\>)|(\<A6N\>)" ;
              ChunkStart = "2.4.2 Step 2: Collect other PLL traces" ;
              ChunkEnd = "Extract from syslog with some the wanted trace: \(from complaintID EMEA:8008402399:2012519726:9\)"};
              |]

        let sss = Rule2Arr keyChunkInfo keyChunkProd DocString

        sss

    let SaveAction (init : InitializeClasses) = 
        init.ChoicAct.SaveFile <- ChoiceActions.SaveXmlFile.Yes;
        init.ChoicAct.SaveFileActions(); 


    let PropertyProviding (searchKeyElements4Use : SearchKeyElements) (init : InitializeClasses)  = 
        
        (init.AllWind.Main.FindName("SearchKey") :?> ComboBox).Text <- searchKeyElements4Use.SearchKey
        (init.AllWind.Main.FindName("InputDateWithIndex") :?> TextBox).Text <- searchKeyElements4Use.Date
        (init.AllWind.ExWindow.FindName("SearchFilesFilter") :?> TextBox).Text <- searchKeyElements4Use.Filter
        (init.AllWind.ExWindow.FindName("Variable") :?> TextBox).Text <- searchKeyElements4Use.Variable
        (init.AllWind.InfoTextWin.FindName("Infotext") :?> TextBox).Text <- searchKeyElements4Use.Infotext
        (init.AllWind.Main.FindName("CriteriaReferenceWithRevision") :?> ComboBox).Text <- searchKeyElements4Use.CriteriaReferenceWithRevision
        (init.AllWind.ExWindow.FindName("Expression") :?> TextBox).Text <- searchKeyElements4Use.Expression
        
    let executeSeq (searchKeyElements4Use : SearchKeyElements) (init : InitializeClasses) =  
        
        OpenNGet init  
        PropertyProviding searchKeyElements4Use init
        GenerateNewKey init
        SaveAction init

    let PerformFullAction (searchKeyElements4Use : SearchKeyElements[]) (init : InitializeClasses)  =
        searchKeyElements4Use
        |> Array.iter(fun x -> executeSeq x init)
    
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