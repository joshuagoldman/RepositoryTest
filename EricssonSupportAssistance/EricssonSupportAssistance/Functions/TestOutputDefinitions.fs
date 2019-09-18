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


type MethodInfo =
    {   Name : string
        Info : string
        StringToAnalyze : seq<seq<string>>
        Ticket : string
        Solution : string
        Category : string
        }

type TestOutputDefinitions() =
    
    inherit ControlBase()

    let exAssembly = Assembly.GetExecutingAssembly()
    let stream = exAssembly.GetManifestResourceStream("EricssonSupportAssistance.EmbeddedTestOutput.FailedMethodInfoDocument.xml")


    member val private FailedTMDoc = XDocument.Load(stream) with get, set

    member val FilePath = "" with get, set

    member private this.newSeqUponMatch (str : string) (sequence : seq<string>) =
        
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


    member private this.stringPairMatches (seqTest : seq<string>) (seqComp : seq<string>) =
        
        let mutable seqCompMutable = seqComp 
        let mutable numSeq = [0..[|seqCompMutable|].Length]

        seqTest
        |> Seq.map(fun seqTest -> seqCompMutable
                                  |> Seq.exists(fun seqComp-> seqComp = seqTest)
                                  |> function
                                     | result when result = true -> fun _ -> seqCompMutable <- (this.newSeqUponMatch seqTest seqCompMutable)
                                                                    |> fun _ -> 1
                                     | _ -> 0)
        |> Seq.sum

    member private this.getXmlValue (method : XElement) (name : string) =
        
        method.XPathSelectElements("*//*")
        |> Seq.find(fun info -> info.Name.ToString() = name)
        |> fun f -> f.FirstAttribute.Value

    member this.toXElementSequence (valPairSeq : seq<{|Name : string ; Value : string|}>) =

        valPairSeq
        |> Seq.map(fun valPair -> new XElement(XName.Get valPair.Name,
                                                XAttribute(XName.Get "Value", valPair.Value)))
     
    member this.StringToSequenceofSequence (str : string) = 
         
        str
        |> fun str -> str.Split '\n'
        |> Seq.map(fun str -> str.Split ' '
                            |> Seq.map(fun str2 -> str2.Trim())
                            |> Seq.filter(fun str2 -> str2 = ""))

    member this.getRating (strChunk : string ) (strCompareSequencefied : seq<seq<string>>) =
        
        let strSequencefied = this.StringToSequenceofSequence strChunk

        strSequencefied
        |> Seq.collect(fun seq -> strCompareSequencefied
                                  |> Seq.map(fun seqComp -> this.stringPairMatches seq seqComp))
        |> Seq.sum
        
    member this.getSolution (strChunk : string) = 
        
        let stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("EricssonSupportAssistance.EmbeddedTestOutPut.FailedMethodInfoDocument.xml")
        this.infoEv.Trigger(InfoEventArgs("Obtaining embedded criteria file 'FailedMethodInfoDocument.xml' for evaluation", Brushes.Black)) 

        let FailedTMDoc = XDocument.Load(stream)

        let methodRatingTuple =
            
            this.infoEv.Trigger(InfoEventArgs("Calculating rating methods", Brushes.Black)) 
            let methodsNodeAll = FailedTMDoc.XPathSelectElements(".//TestMethod")
            
            methodsNodeAll
            |> Seq.map(fun method -> {  Name = method.Name.ToString() ; 
                                        Info = this.getXmlValue method "Info" ;
                                        StringToAnalyze = this.StringToSequenceofSequence (this.getXmlValue method "StringToAnalyze") ;
                                        Ticket = this.getXmlValue method "Ticket" ;
                                        Solution = this.getXmlValue method "Solution";
                                        Category = method.XPathSelectElement("(..)[last()]").FirstAttribute.Value })
            |> Seq.map(fun method -> (method, this.getRating strChunk method.StringToAnalyze))

        let maxRating = 
            methodRatingTuple
            |> Seq.map(fun (_, rating) -> rating)
            |> fun x -> x 
                        |> fun _ -> this.infoEv.Trigger(InfoEventArgs("Obtaining embedded criteria file 'FailedMethodInfoDocument.xml' for evaluation", Brushes.Black))
                        |> fun _ -> x |> Seq.max

        methodRatingTuple
        |> Seq.find(fun (_,rating) -> rating = maxRating)
        |> fun (method, _) -> method.Solution

    member this.GetFile = 

        let dialog = new OpenFileDialog()
        dialog.ShowDialog()
        |> ignore

        this.infoEv.Trigger(InfoEventArgs(String.Format("Current upload file is: {0}", dialog.FileName),
                             Brushes.Black ))

        File.ReadAllText(dialog.FileName)

    member this.tryFindSolution (ticket : string) (solutionPrepared : string) = 
        
        let textFileString = this.GetFile

        let failedMethodStringChunk = Regex.Match(textFileString, "(\n\*\*\*\* )(?:(?!(\n\*\*\*\* )|( 	Fail))(.|\n))*?( 	Fail)").Value

        this.infoEv.Trigger(InfoEventArgs(String.Format("Getting failed method chunk:\n\n{0}", failedMethodStringChunk),
                                     Brushes.Black))

        let failedMethodName = Regex.Match(failedMethodStringChunk, "(\*\*\*\* )(\n|.)*?( \*\*\*\*)").Value
                               |> fun str -> str.Replace("\*","")
                               |> fun str -> str.Trim()

        this.infoEv.Trigger(InfoEventArgs(String.Format("Failed method name set to: {0}", failedMethodName),
                             Brushes.Black))

        let categories = this.FailedTMDoc.XPathSelectElements("*//Category")
                         |> Seq.map(fun cat -> cat.FirstAttribute.Value)

        let category = categories
                       |> fun sequence -> sequence
                                          |> Seq.exists(fun cat -> failedMethodName.Contains(cat))
                                          |> fun res -> (sequence, res)
                       |> function
                          | (sequence,result) when result = true -> sequence
                                                                    |> Seq.find(fun cat -> failedMethodName.Contains(cat))
                          
                          | (_,_) -> "Other"
        
        this.infoEv.Trigger(InfoEventArgs(String.Format("Category set to: {0}", category),
                             Brushes.Black))

        let solution = 
            
            solutionPrepared
            |> function
               | _ when solutionPrepared = "" ->
                    
                    this.infoEv.Trigger(InfoEventArgs("Getting solution", Brushes.Black))
                    |> fun _ -> this.getSolution failedMethodStringChunk
               
               | _ -> 
                    
                    this.infoEv.Trigger(InfoEventArgs("Getting prepared solution", Brushes.Black))
                    |> fun _ -> solutionPrepared

        this.infoEv.Trigger(InfoEventArgs(String.Format("Obtained solution:\n\n{0}", solution),
                             Brushes.Black))
        
        let elementSequence = seq[  {|Name = "Info" ; Value = ""|} ;
                                    {|Name = "Ticket" ; Value = ticket|} ;
                                    {|Name = "Solution" ; Value = solution|} ;
                                    {|Name = "StringToAnalyze" ; Value = failedMethodStringChunk|}
                                    ]

        let methodXElement = new XElement(XName.Get "Method",
                                          XAttribute(XName.Get "Name", failedMethodName),
                                          this.toXElementSequence elementSequence)
                                          
        this.FailedTMDoc.XPathSelectElement(String.Format("*//Category[@Name = '{0}']", category)).Add(methodXElement)

        this.FailedTMDoc.Save(this.FilePath)

        solution
