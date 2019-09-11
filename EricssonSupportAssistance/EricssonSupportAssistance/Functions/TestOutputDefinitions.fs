﻿namespace EricssonSupportAssistance.Functions 

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

    let stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("EricssonSupportAssistance.EmbeddedTestOutPut.FailedMethodInfoDocument.xml")

    let mutable FailedTMDoc = XDocument.Load(stream)

    type MethodInfo =
        {   Name : string
            Info : string
            StringToAnalyze : seq<seq<string>>
            Ticket : string
            Solution : string
            Category : string
            }

    let newSeqUponMatch (str : string) (sequence : seq<string>) =
        
        let length = sequence |> Seq.length |> fun i -> i - 1

        let (_,pos) = Seq.zip sequence [0..length] 
                      |> Seq.find(fun (strComp,_) -> str = strComp)

        let beforePos = sequence 
                        |> Seq.toArray
                        |> fun x -> x.[0..pos - 1]
                        |> fun x -> x |> Array.toSeq
        let afterPos = sequence 
                        |> Seq.toArray
                        |> fun x -> x.[pos + 1..length]
                        |> fun x -> x |> Array.toSeq

        
        sequence
        |> function
           | _ when pos = length - 1 ->
            
            sequence |> Seq.toArray |> fun x -> x.[0..length - 2] |> Array.toSeq
           
           | _ when pos = 0 -> 
            
            sequence |> Seq.toArray |> fun x -> x.[1..length - 1] |> Array.toSeq
           
           | _ ->
           
            Seq.append beforePos afterPos


    let stringPairMatches (seqTest : seq<string>) (seqComp : seq<string>) =
        
        let mutable seqCompMutable = seqComp 
        let mutable numSeq = [0..[|seqCompMutable|].Length]

        seqTest
        |> Seq.map(fun seqTest -> seqCompMutable
                                  |> Seq.exists(fun seqComp-> seqComp = seqTest)
                                  |> function
                                     | result when result = true -> fun _ -> seqCompMutable <- (newSeqUponMatch seqTest seqCompMutable)
                                                                    |> fun _ -> 1
                                     | _ -> 0)
        |> Seq.sum

    let getXmlValue (method : XElement) (name : string) =
        
        method.XPathSelectElements("*//*")
        |> Seq.find(fun info -> info.Name.ToString() = name)
        |> fun f -> f.FirstAttribute.Value
     
    let StringToSequenceofSequence (str : string) = 
         
        str
        |> fun str -> str.Split '\n'
        |> Seq.map(fun str -> str.Split ' '
                            |> Seq.map(fun str2 -> str2.Trim())
                            |> Seq.filter(fun str2 -> str2 = ""))

    let getRating (strChunk : string ) (strCompareSequencefied : seq<seq<string>>) =
        
        let strSequencefied = StringToSequenceofSequence strChunk

        strSequencefied
        |> Seq.collect(fun seq -> strCompareSequencefied
                                  |> Seq.map(fun seqComp -> stringPairMatches seq seqComp))
        |> Seq.sum
        
    let getSolution (category :  string) (strChunk : string) = 

        let methodRatingTuple =
            
            let methodsNodeAll = FailedTMDoc.XPathSelectElements(".//TestMethod")
            
            methodsNodeAll
            |> Seq.map(fun method -> {  Name = method.Name.ToString() ; 
                                        Info = getXmlValue method "Info" ;
                                        StringToAnalyze = StringToSequenceofSequence (getXmlValue method "StringToAnalyze") ;
                                        Ticket = getXmlValue method "Ticket" ;
                                        Solution = getXmlValue method "Solution";
                                        Category = method.XPathSelectElement("(..)[last()]").FirstAttribute.Value })
            |> Seq.filter(fun method -> method.Category = category)
            |> Seq.map(fun method -> (method, getRating strChunk method.StringToAnalyze))

        let maxRating = 
            methodRatingTuple
            |> Seq.map(fun (method, rating) -> rating)
            |> Seq.max

        methodRatingTuple
        |> Seq.find(fun (method,rating) -> rating = maxRating)
        |> fun (method, _) -> method.Solution
        
    let uploadInfoByDocument (ticket : string) = 
        
        MessageBox.Show("Please browse the HWLogCriteria.xml file directory in which changes are to be saved",
                        "Information",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information)
        |> ignore

        let dialog = new OpenFileDialog()

        let textFile = File.CreateText(dialog.FileName)

        let textFileString = textFile.ToString()

        let failedMethodStringChunk = Regex.Match(textFileString, "(\n\*\*\*\* )(?:(?!(\n\*\*\*\* )|( 	Fail))(.|\n))*?( 	Fail)").Value

        infoEv.Trigger(InfoEventArgs(String.Format("Getting failed method chunk:\n\n{0}", failedMethodStringChunk),
                                     Brushes.Black))

        let failedMethodName = Regex.Match(failedMethodStringChunk, "(\*\*\*\* )(\n|.)*?( \*\*\*\*)").Value

        infoEv.Trigger(InfoEventArgs(String.Format("Failed method name set to: {0}", failedMethodName),
                        Brushes.Black))

        let categories = FailedTMDoc.XPathSelectElements("*//Category")
                         |> Seq.map(fun cat -> cat.FirstAttribute.Value)

        let category = categories
                       |> fun sequence -> sequence
                                          |> Seq.exists(fun cat -> failedMethodName.Contains(cat))
                                          |> fun res -> (sequence, res)
                       |> function
                          | (sequence,result) when result = true -> sequence
                                                                    |> Seq.find(fun cat -> failedMethodName.Contains(cat))
                          
                          | (_,_) -> "Other"
        
        infoEv.Trigger(InfoEventArgs(String.Format("Category set to: {0}", category),
                        Brushes.Black))

        infoEv.Trigger(InfoEventArgs("Getting solution", Brushes.Black))
        let solution = getSolution category failedMethodStringChunk

        infoEv.Trigger(InfoEventArgs(String.Format("Obtained solution:\n\n{0}", solution),
                        Brushes.Black))

        MessageBox.Show("Please browse the HWLogCriteria.xml file directory in which changes are to be saved",
                        "Information",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information)
        
        let infoXElement = new XElement("Info", new XAttribute("Value",""))
        let ticketXElement = new XElement("Ticket", new XAttribute("Value",ticket))
        let solutionXElement = new XElement("Solution", new XAttribute("Value",solution))
        let stringToAnalyzeXElement = new XElement("StringToAnalyze", new XAttribute("Value",failedMethodStringChunk))

        let methodXElement = new XElement("Method",
                                          new XAttribute("Name", failedMethodName),
                                          infoXElement,
                                          ticketXElement,
                                          solutionXElement,
                                          stringToAnalyzeXElement)
                                          
        FailedTMDoc.XPathSelectElement(String.Format("*//Category[@Name = '{0}']", category)).Add(methodXElement)

        FailedTMDoc.Save(fileName)

        
       