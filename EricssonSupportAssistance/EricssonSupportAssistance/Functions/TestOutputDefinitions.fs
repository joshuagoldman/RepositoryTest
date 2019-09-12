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
        
    let getSolution (strChunk : string) = 
        
        let stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("EricssonSupportAssistance.EmbeddedTestOutPut.FailedMethodInfoDocument.xml")
        infoEv.Trigger(InfoEventArgs("Obtaining embedded criteria file 'FailedMethodInfoDocument.xml' for evaluation", Brushes.Black)) 

        let FailedTMDoc = XDocument.Load(stream)

        let methodRatingTuple =
            
            infoEv.Trigger(InfoEventArgs("Calculating rating methods", Brushes.Black)) 
            let methodsNodeAll = FailedTMDoc.XPathSelectElements(".//TestMethod")
            
            methodsNodeAll
            |> Seq.map(fun method -> {  Name = method.Name.ToString() ; 
                                        Info = getXmlValue method "Info" ;
                                        StringToAnalyze = StringToSequenceofSequence (getXmlValue method "StringToAnalyze") ;
                                        Ticket = getXmlValue method "Ticket" ;
                                        Solution = getXmlValue method "Solution";
                                        Category = method.XPathSelectElement("(..)[last()]").FirstAttribute.Value })
            |> Seq.map(fun method -> (method, getRating strChunk method.StringToAnalyze))

        let maxRating = 
            methodRatingTuple
            |> Seq.map(fun (_, rating) -> rating)
            |> fun x -> x 
                        |> fun _ -> infoEv.Trigger(InfoEventArgs("Obtaining embedded criteria file 'FailedMethodInfoDocument.xml' for evaluation", Brushes.Black))
                        |> fun _ -> x |> Seq.max

        methodRatingTuple
        |> Seq.find(fun (_,rating) -> rating = maxRating)
        |> fun (method, _) -> method.Solution
        
    let uploadFailedMethod = 
        
        let 