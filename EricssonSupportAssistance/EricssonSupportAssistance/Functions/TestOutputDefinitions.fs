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

module TestOutputDefinitions =
    
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
                                                                                Ticket = getXmlValue "Ticket" ;
                                                                                Solution = getXmlValue "Solution"}))

            | _ when element.Name.ToString() = "ReceptionSensitivity" -> element.XPathSelectElements("*//*")
                                                                         |> Seq.map(fun method -> ReceptionSensitivity({ Name = method.Name.ToString() ; 
                                                                                                                Info = getXmlValue method "Info" ;
                                                                                                                StringsToAnalyze = StringToSequenceofSequence method "StringsToAnalyze" ;
                                                                                                                Ticket = getXmlValue "Ticket" ;
                                                                                                                Solution = getXmlValue "Solution"}))

            | _ -> ReceptionSensitivity({ Name = method.Name.ToString() ; 
                                          Info = getXmlValue method "Info" ;
                                          StringsToAnalyze = StringToSequenceofSequence method "StringsToAnalyze" ;
                                          Ticket = getXmlValue "Ticket" ;
                                          Solution = getXmlValue "Solution"})


    let createStringsForAnalysis = 
        
        let stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("EricssonSupportAssistance.EmbeddedTestOutPut.FailedMethodInfoDocument.xml")

        let FailedTMDoc = XDocument.Load(stream)

        let tesMethodsAll =
            
            let MethodNodesAll = FailedTMDoc.XPathSelectElements(".//TestMethod")

            MethodNodesAll
            |> Seq.map(fun node -> node)

        
        
        