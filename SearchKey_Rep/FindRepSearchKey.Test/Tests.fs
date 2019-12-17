namespace FindRepSearchKey.Tests

open System
open Xunit
open FindRepSearchKey
open FindHwPidListItems


module Tests =

    type testType =
        {   Name : string
            RState : string
            Equation : string
        }


    [<Fact>]
    let ``NumberOfMatchesIsOne`` () =
    

        let mutable SeqOfSeq = seq[
                                seq[{Name = "1" ; RState = "r" ; Equation = "e"} ;
                                    {Name = "2" ; RState = "r" ; Equation = "e"}
                                    {Name = "3" ; RState = "r" ; Equation = "e"}
                                    {Name = "4" ; RState = "r" ; Equation = "e"}
                                    {Name = "5" ; RState = "r" ; Equation = "e"}
                                    ] ;

                                seq[{Name = "1" ; RState = "r" ; Equation = "e"} ;
                                    {Name = "6" ; RState = "r" ; Equation = "e"}
                                    {Name = "7" ; RState = "r" ; Equation = "e"}
                                    {Name = "8" ; RState = "r" ; Equation = "e"}
                                    {Name = "9" ; RState = "r" ; Equation = "e"}
                                    ] ;

                                seq[{Name = "10" ; RState = "r" ; Equation = "e"} ;
                                    {Name = "11" ; RState = "r" ; Equation = "e"}
                                    {Name = "12" ; RState = "r" ; Equation = "e"}
                                    {Name = "13" ; RState = "r" ; Equation = "e"}
                                    {Name = "14" ; RState = "r" ; Equation = "e"}
                                    ]
                                ]
        
        let matches = FindRepSearchKey.FindMatchesInOther(SeqOfSeq, "Name")

        Assert.True(matches.Count = 1)

    [<Fact>]
    let ``NumberOfMatchesIsFive`` () =
    

        let mutable SeqOfSeq = seq[
                                seq[{Name = "1" ; RState = "r" ; Equation = "e"} ;
                                    {Name = "2" ; RState = "r" ; Equation = "e"}
                                    {Name = "11" ; RState = "r" ; Equation = "e"}
                                    {Name = "6" ; RState = "r" ; Equation = "e"}
                                    {Name = "5" ; RState = "r" ; Equation = "e"}
                                    ] ;

                                seq[{Name = "1" ; RState = "r" ; Equation = "e"} ;
                                    {Name = "6" ; RState = "r" ; Equation = "e"}
                                    {Name = "7" ; RState = "r" ; Equation = "e"}
                                    {Name = "14" ; RState = "r" ; Equation = "e"}
                                    {Name = "2" ; RState = "r" ; Equation = "e"}
                                    ] ;

                                seq[{Name = "10" ; RState = "r" ; Equation = "e"} ;
                                    {Name = "11" ; RState = "r" ; Equation = "e"}
                                    {Name = "12" ; RState = "r" ; Equation = "e"}
                                    {Name = "2" ; RState = "r" ; Equation = "e"}
                                    {Name = "14" ; RState = "r" ; Equation = "e"}
                                    ]
                                ]
        
        let matches = FindRepSearchKey.FindMatchesInOther(SeqOfSeq, "Name")

        Assert.True(matches.Count = 5)


    [<Fact>]
    let ``Logs_317_837_Radio_Test`` () =
    

        let testString = "DCDC Component Failure 1   (317; > 0) and (837; = 0)
        DCDC Component Failure 2   (317; = 0) and (837; > 0)
        DCDC Component Failure 3   (317; > 0) and (837; > 0)
        DCDC Component Failure indication T1   (317; > 0) and (837; = 0)
        DCDC Component Failure indication T2   (317; = 0) and (837; > 0)
        DCDC Component Failure indication T3   (317; > 0) and (837; > 0)
        DCDC Component Failure indication in LAT RRUS 11B1. 2/-20,A   (317; > 0)
        DCDC Component Failure indication in LAT RRUS 11B1. 2/-20,B   (837; > 0)
        DCDC Component Failure indication, 317;. 2/-27,A   (317; > 0) and (1 > 837;)
        DCDC Component Failure indication, 837;. 2/-27,A   (1 > 317;) and (837; > 0)
        DCDC Component Failure indication, 317;837;. 2/-27,A   (317; > 0) and (837; > 0)
        DCDC Component Failure indication, 317;. 2/-27,A   (317; > 0) and (1 > 837;)
        DCDC Component Failure indication, 837;. 2/-27,A   (1 > 317;) and (837; > 0)
        DCDC Component Failure indication, 317;837;. 2/-27,A   (317; > 0) and (837; > 0)
        DCDC Component Failure, 317;. 2/-29,B   (317; > 0) and (1 > 837;)
        DCDC Component Failure, 837;. 2/-29,B   (1 > 317;) and (837; > 0)
        DCDC Component Failure, 317;,837;. 2/-29,B   (317; > 0) and (837; > 0)"

        let testStringNowSeq =
            testString
            |> fun x -> x.Split '\n'
            |> Array.toSeq

        let numbersWhereLargerThan317Only =
            
            Seq.zip testStringNowSeq [0..testStringNowSeq |> Seq.length]
            |> Seq.filter(fun (str, _) -> str.Contains("317; > 0") && not(str.Contains("837; > 0")))
            |> Seq.map(fun (_, num) -> Convert.ToString(num + 1))
            |> fun x -> (",", x) |> System.String.Join

        let numbersWhereLargerThan837Only =
            
            Seq.zip testStringNowSeq [0..testStringNowSeq |> Seq.length]
            |> Seq.filter(fun (str, _) -> str.Contains("837; > 0") && not(str.Contains("317; > 0")))
            |> Seq.map(fun (_, num) -> Convert.ToString(num + 1))
            |> fun x -> (",", x) |> System.String.Join

        let numbersWhereLargerThan837And317 =
            
            Seq.zip testStringNowSeq [0..testStringNowSeq |> Seq.length]
            |> Seq.filter(fun (str, _) -> str.Contains("837; > 0") && str.Contains("317; > 0") ||
                                          str.Contains("837; > 0") && not(str.Contains("317;")) ||
                                          str.Contains("317; > 0") && not(str.Contains("837;")))
            |> Seq.map(fun (_, num) -> Convert.ToString(num + 1))
            |> fun x -> (",", x) |> System.String.Join

        Assert.True(true)

    [<Fact>]
    let ``TestHwPidListCreation`` () =
        let testCase = {
              ProdNumber = "KRC 161 833/2" ; 
              Name = "Radio 8836" ;
              Power = "" ;
              FrequenceWidth = "" }

        let final = 
            testCase
            |> fun x -> getPower x "B7"
            |> getFreqWidth  

        Assert.True (   final.Power = "20" &&
                     final.FrequenceWidth = "5")