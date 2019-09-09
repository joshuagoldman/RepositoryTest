namespace SearchKeyRep

open SearchKey_GUI.Methods
open SearchKey_GUI
open System.Threading
open SearchKeyRep.DocumentReading.SetUpCase_SK_1_68_2
open SearchKeyRep.DocumentReading.SetUpCase_SK_1_68_1
open SearchKeyRep.Methods.SK_1_68_Methods
open SearchKeyRep.Definitions.Definitions_1_68
open System.Windows.Controls

module SK_1_68_Final_Setup = 
    
    let keyChunkInfoRule1 = 
        [|
          "(A6P).*";
          "ICM CCR PLL.*"
          |]

    let varsInfo = [|
                      {Key = tablePattern ; ChunkStart = "Table 1-A: rules 1a to 1e valid for:" ; ChunkEnd = "Table 1-B: rules 1f to 1h valid for:"};
                      {Key = tablePattern ; ChunkStart = "Table 1-B: rules 1f to 1h valid for:" ; ChunkEnd = "Table 1-C: rules 1i to 1k valid for:"};
                      {Key = tablePattern ; ChunkStart = "Table 1-C: rules 1i to 1k valid for:" ; ChunkEnd = "2.4.3.2 Rule 2: Valid when no"}
                      |]

    let rule1_1_68_AllSearchKeys = Rule1Arr varsInfo keyChunkInfoRule1

    let keyChunkProd = 
        { Key = "KDU" ;
          ChunkStart = "2.1 Product number" ;
          ChunkEnd = "2.2 Serial number";}

    let keyChunkInfo = 
        [|
          { Key = "\(A6M\)|\(A6N\)" ;
          ChunkStart = "2.4.2 Step 2: Collect other PLL traces" ;
          ChunkEnd = "Criteria 2: Trace from system log"};

          { Key = "(\\<A6M\\>)|(\\<A6N\\>)" ;
          ChunkStart = "2.4.2 Step 2: Collect other PLL traces" ;
          ChunkEnd = "Extract from syslog with some the wanted trace: \(from complaintID EMEA:8008402399:2012519726:9\)"};
          |]

    let rule2_1_68_AllSearchKeys = Rule2Arr keyChunkInfo keyChunkProd DocString
