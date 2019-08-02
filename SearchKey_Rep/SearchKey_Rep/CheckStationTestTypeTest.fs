namespace SearchKeyRep

open System.Threading
open System.Windows
open System.Windows.Controls
open Ericsson.TM.Node;
open Ericsson.SC.Node;
open Ericsson.AM.Common.Definitions;
open System.Linq;

module CheckStationTestTypeTest = 
    
    type TestTypeInfo =
        { 
            StationTypeStr : string
            StationType : StationTestType
            isPrtt : bool
            isScreeningStation : bool
        }

    type Result = 
            |Pass = 0
            |Fail = 1

    type OutCome = 
        { 
            Res : Result
            Message : string
        }

    type Currentsettings = 
        { 
            STT : string
            PRTT : bool
            Screening : bool
            ExpectedOutCome : OutCome
        }

    let TestTypeInfo = 

        [|
            {StationTypeStr = "All" ; StationType = StationTestType.All ; isPrtt = true ; isScreeningStation = true} ;
            {StationTypeStr = "RcExtPrtt" ; StationType = StationTestType.RcExtPrtt ; isPrtt = true ; isScreeningStation = false} ;  
            {StationTypeStr = "RcExtLat" ; StationType = StationTestType.RcExtLat ; isPrtt = false ; isScreeningStation = false} ;
            {StationTypeStr = "RcLat" ; StationType = StationTestType.RcLat ; isPrtt = false ; isScreeningStation = false} ;
            {StationTypeStr = "RcPrtt" ; StationType = StationTestType.RcPrtt ; isPrtt = true ; isScreeningStation = false} ;
            {StationTypeStr = "ScLat" ; StationType = StationTestType.ScLat ; isPrtt = false ; isScreeningStation = true} ;
            {StationTypeStr = "ScPrtt" ; StationType = StationTestType.ScPrtt ; isPrtt = true ; isScreeningStation = true} ;
        |]

    let Cases = 

        [|
            {STT = "All" ; PRTT = true ; Screening = true ; ExpectedOutCome = {Res = Result.Pass ; 
            Message = ""}} ;

            {STT = "RcExtPrtt" ; PRTT = true ; Screening = true ; ExpectedOutCome = {Res = Result.Fail ; 
            Message = "ScreeningStation should be set to 'false' in IVI settings, which is currently not the case."}} ;

            {STT = "RcExtLat" ; PRTT = true ; Screening = true ; ExpectedOutCome = {Res = Result.Fail ; 
            Message = "Test type should not be set to PRTT_TEST, which is currently the case.\nScreeningStation should be set to 'false' in IVI settings, which is currently not the case."}} ;

            {STT = "RcLat" ; PRTT = false ; Screening = false ; ExpectedOutCome = {Res = Result.Pass ; 
            Message = ""}} ;

            {STT = "RcPrtt" ; PRTT = true ; Screening = false ; ExpectedOutCome = {Res = Result.Pass ; 
            Message = ""}} ;

            {STT = "ScPrtt" ; PRTT = false ; Screening = false ; ExpectedOutCome = {Res = Result.Fail ; 
            Message = "Test type should be set to PRTT_TEST, which is currently not the case.\nScreeningStation should be set to 'true' in IVI settings, which is currently not the case."}} ;

            {STT = "Scsss" ; PRTT = false ; Screening = false ; ExpectedOutCome = {Res = Result.Fail ; 
            Message = "Wrong station test type in IVI settings"}} ;

            {STT = "ScLat" ; PRTT = false ; Screening = false ; ExpectedOutCome = {Res = Result.Fail ; 
            Message = "ScreeningStation should be set to 'true' in IVI settings, which is currently not the case."}} ;

            {STT = "RcExtLat" ; PRTT = false ; Screening = false ; ExpectedOutCome = {Res = Result.Pass ; 
            Message = ""}} ;
        |]

    type ConditionText = 
        { 
            Expected : bool
            Actual : bool
            message : string
        }

    let Method (case : Currentsettings) = 
        
        let isprtt = case.PRTT
        let isScreeningStation = case.Screening
        let stationTestTypeIVI = case.STT

        let stationTestTypeInfoChosen = 
            TestTypeInfo
            |> Array.tryFind(fun test_type -> test_type.StationTypeStr = stationTestTypeIVI &&
                                              test_type.isPrtt = isprtt &&
                                              test_type.isScreeningStation = isScreeningStation)
            |> function
                | ss when ss.IsNone -> None 
                | ss when ss.IsSome -> ss
                | _ -> None

        let WrongStationTypeStringCheck (stt : string) (testTypeInfoComp : TestTypeInfo []) =
            testTypeInfoComp
            |> Array.forall(fun info -> info.StationTypeStr <> stt) 

        let OtherOptions  =

            let ExpectedInfo = 
                TestTypeInfo
                |> Array.tryFind(fun info -> info.StationTypeStr = stationTestTypeIVI)
            
            let ConditionTextPRTT = 
                [|
                    {Expected = true ; Actual = false ; message =  "Test type should be set to PRTT_TEST, which is currently not the case.\n"};
                    {Expected = false ; Actual = true ; message =  "Test type should not be set to PRTT_TEST, which is currently the case.\n"};
                |]

            let ConditionTextScreening = 
                [|
                    {Expected = true ; Actual = false ; message =  "ScreeningStation should be set to 'true' in IVI settings, which is currently not the case."};
                    {Expected = false ; Actual = true ; message =  "ScreeningStation should be set to 'false' in IVI settings, which is currently not the case."};
                |]

            let IsPrttFinalText = 
                ConditionTextPRTT
                |> function 
                    | _ when ExpectedInfo = None -> Option.None
                    | text -> text |> Array.tryFind(fun cond -> cond.Actual = isprtt && cond.Expected = ExpectedInfo.Value.isPrtt)

            let IsScreeningFinalText = 
                ConditionTextScreening
                |> function 
                    | _ when ExpectedInfo = None -> Option.None
                    | text -> text |> Array.tryFind(fun cond -> cond.Actual = isScreeningStation && cond.Expected = ExpectedInfo.Value.isScreeningStation)
            
            let FinalStr (strArr : ConditionText option[]) =
            
                (strArr.ElementAt(0), strArr.ElementAt(1))
                |> function 
                    | (is_prtt,is_screening) when is_prtt = None && is_screening = None -> ""
                    | (is_prtt,is_screening) when is_prtt <> None && is_screening = None -> is_prtt.Value.message
                    | (is_prtt,is_screening) when is_prtt = None && is_screening <> None -> is_screening.Value.message
                    | (is_prtt,is_screening) -> is_prtt.Value.message + is_screening.Value.message
            
            {Res = Result.Pass ; Message = FinalStr  [|IsPrttFinalText ; IsScreeningFinalText|]}
        
        stationTestTypeInfoChosen 
        |> function
            |test_type when  test_type.IsNone &&  WrongStationTypeStringCheck stationTestTypeIVI TestTypeInfo = false -> OtherOptions 
            |test_type when test_type.IsNone = true -> {Res = Result.Pass ; Message = "Wrong station test type in IVI settings"}
            | _ -> {Res = Result.Pass ; Message = ""}
    
    let GetOutCome (outcome : (OutCome * Currentsettings)) =
        
        outcome
        |> function
            | (result,expected) when result.Message = expected.ExpectedOutCome.Message -> {Message = "Test case passed:\n\nactual: " + result.Message + "\nexpected: " + expected.ExpectedOutCome.Message + "\n\n\n";
                                                                                           Res = Result.Pass}
            | (result,expected) -> {Message = "Test case failed:\n\nactual: " + result.Message + "\nexpected: " + expected.ExpectedOutCome.Message + "\n\n\n";
                                    Res = Result.Fail}

    let PopUpMessage (outcome : OutCome[]) = 
        outcome
        |> Array.forall(fun res -> res.Res = Result.Pass)
        |> function
            | true -> outcome |> Array.map(fun res -> res.Message) |> String.concat "" |> fun str -> str + "\n\n\n\nPASSED"
            | _ -> outcome |> Array.map(fun res -> res.Message) |> String.concat "" |> fun str -> str + "\n\n\n\nFAILED"


    let MethodLoop (cases :  Currentsettings[]) = 
        
        cases
        |> Array.map(fun case -> Method case)
        |> fun res -> Array.zip res cases
        |> Array.map(fun outcome -> GetOutCome outcome)
        |> fun res_Arr -> MessageBox.Show(PopUpMessage res_Arr)

    MethodLoop Cases |> ignore