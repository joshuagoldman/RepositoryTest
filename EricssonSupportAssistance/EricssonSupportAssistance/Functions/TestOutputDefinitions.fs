namespace EricssonSupportAssistance.Functions 

open FsXaml
open System.Windows
open EricssonSupportAssistance
open System.Reflection
open System.Windows.Forms
open EricssonSupportAssistance.XamlFiles
open EricssonSupportAssistance.MailHandling.MailActions
open System
open System.Windows.Media
open System.Windows.Controls
open System.IO
open System.Windows.Data
open Microsoft.Win32
open System.Xml.Linq
open System.Xml.XPath
open System.Text.RegularExpressions

module TestOutputDefinitions =
    
    let infoEv = new Event<InfoEventArgs>()

    type MethodInfo =
        {   Name : string
            Info : string
            StringsToAnalyze : seq<seq<string>>
            Ticket : string
            Solution : string
            }

    type TestMethod =
        | Sftp  of MethodInfo
        | ReceptionSensitivity  of MethodInfo
        | Other of MethodInfo

    let sequencefyStr (str : string) =
        
        str
        |> fun s -> s.Split '\n'
        |> Seq.map(fun seq -> seq.Split ' '
                              |> Seq.filter(fun subSeq -> not(subSeq
                                                          |> String.forall(fun chr -> chr = ' '))))

    let matchExistsOrNotAction (result : bool) (sequence : seq<string>) =
        
        match result with
        
        | true ->  

    let stringPairMatches (seqTest : seq<string>) (seqComp : seq<string>) =
        
        let mutable seqCompMutable = seqComp 
        let mutable numSeq = [0..[|seqCompMutable|].Length]

        seqTest
        |> Seq.map(fun seqTest -> Seq.zip seqCompMutable numSeq
                                    |> Seq.exists(fun (seqComp,n) -> seqComp = seqTest))
                                    |> 

    let tryFindSolution (strChunk : string ) (strChunkCompare : string) =
        
        let strSequencefied = sequencefyStr strChunk
        let strCompareSequencefied = sequencefyStr strChunkCompare

        strSequencefied
        |> Seq.collect(fun seq -> strCompareSequencefied
                                  |> Seq.map(fun seqComp -> ))

    
    let getXmlValue (method : XElement) (name : string) =
        
        method.XPathSelectElements("*//*")
        |> Seq.find(fun info -> info.Name.ToString() = name)
        |> fun f -> f.FirstAttribute.Value
    
    let StringToSequenceofSequence (method : XElement) (name : string) = 
        
        getXmlValue method name
        |> fun str -> str.Split '\n'
        |> Seq.map(fun str -> str.Split ' '
                              |> Seq.map(fun str2 -> str2.Trim())
                              |> Seq.filter(fun str2 -> str2 = ""))

    let ChooseByCathegory (element : XElement) =

        element
        |> function 
            | _ when element.Name.ToString() = "Sftp" -> element.XPathSelectElements("*//*")
                                                         |> Seq.map(fun method -> Sftp({ Name = method.Name.ToString() ; 
                                                                                         Info = getXmlValue method "Info" ;
                                                                                         StringsToAnalyze = StringToSequenceofSequence method "StringsToAnalyze" ;
                                                                                         Ticket = getXmlValue method "Ticket" ;
                                                                                         Solution = getXmlValue method "Solution"}))

            | _ when element.Name.ToString() = "ReceptionSensitivity" -> element.XPathSelectElements("*//*")
                                                                         |> Seq.map(fun method -> ReceptionSensitivity({ Name = method.Name.ToString() ; 
                                                                                                                         Info = getXmlValue method "Info" ;
                                                                                                                         StringsToAnalyze = StringToSequenceofSequence method "StringsToAnalyze" ;
                                                                                                                         Ticket = getXmlValue method "Ticket" ;
                                                                                                                         Solution = getXmlValue method "Solution"}))

            | _ -> element.XPathSelectElements("*//*")
                   |> Seq.map(fun method -> Other({ Name = method.Name.ToString() ; 
                                                    Info = getXmlValue method "Info" ;
                                                    StringsToAnalyze = StringToSequenceofSequence method "StringsToAnalyze" ;
                                                    Ticket = getXmlValue method "Ticket" ;
                                                    Solution = getXmlValue method "Solution"}))


    let createStringsForAnalysis = 
        
        let stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("EricssonSupportAssistance.EmbeddedTestOutPut.FailedMethodInfoDocument.xml")

        let FailedTMDoc = XDocument.Load(stream)

        let tesMethodsAll =
            
            let MethodNodesAll = FailedTMDoc.XPathSelectElements(".//TestMethod")

            MethodNodesAll
            |> Seq.collect(fun node -> ChooseByCathegory node)
            |>

        
        
        