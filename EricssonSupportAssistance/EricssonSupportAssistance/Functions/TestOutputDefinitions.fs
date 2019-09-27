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
open System.Linq
open System.IO
open System.Windows.Xps.Packaging


type MethodInfo =
    {   Name : string
        Info : string
        StringToAnalyze : seq<seq<string>>
        Ticket : string
        Solution : string
        Category : string
        }


type UploadSolution =
    | Yes
    | No

type FileType =
    | UploadFile
    | Solution

type TestOutputDefinitions() =
    
    inherit ControlBase()

    let exAssembly = Assembly.GetExecutingAssembly()
    let stream = exAssembly.GetManifestResourceStream("EricssonSupportAssistance.EmbeddedTestOutput.FailedMethodInfoDocument.xml")
    let mutable textfileString = ""
    let mutable uploadFileName = ""
    let mutable solutionFileName = ""

    member this.TextFileString
        with get() = textfileString
        and set(value) =
            if value <> textfileString then textfileString <- value

    member this.UploadFileName
        with get() = uploadFileName
        and set(value) =
            if value <> uploadFileName then uploadFileName <- value

    member this.SolutionFileName
        with get() = solutionFileName
        and set(value) =
            if value <> solutionFileName then solutionFileName <- value

    member val FailedTMDoc = XDocument.Load(stream) with get, set

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

        let res =
            seqTest
            |> Seq.map(fun seqTest -> seqCompMutable
                                      |> Seq.exists(fun seqComp-> seqComp = seqTest)
                                      |> function
                                         | result when result = true -> fun _ -> seqCompMutable <- (this.newSeqUponMatch seqTest seqCompMutable)
                                                                        |> fun _ -> 1
                                         | _ -> 0)
            |> Seq.sum
        (res, seqCompMutable)
    member private this.getXmlValue (method : XElement) (name : string) =
        
        method.XPathSelectElement(".//" + name)
        |> fun f -> f.FirstAttribute.Value

    member this.toXElementSequence (valPairSeq : seq<{|Name : string ; Value : string|}>) =

        valPairSeq
        |> Seq.map(fun valPair -> new XElement(XName.Get valPair.Name,
                                                XAttribute(XName.Get "Value", valPair.Value)))
     
    member this.StringToSequenceofSequence (str : string) = 
         
        let res =
            str
            |> fun str -> str.Split([| "]>  " |], StringSplitOptions.None)
            |> Seq.distinct
            |> Seq.map(fun str -> str.Split ' '
                                  |>Seq.filter(fun str -> not(Regex.IsMatch(str, "\[[0-9]")))
                                  |> Seq.map(fun str2 -> str2.Trim())
                                  |> Seq.filter(fun str2 -> str2 <> ""))

        res

    member this.getRating (strChunk : string ) (strCompareSequencefied : seq<seq<string>>) =
        
        let strSequencefied = this.StringToSequenceofSequence strChunk
        let mutable strCompareArrMutable = strCompareSequencefied |> Seq.toArray
        let mutable numSeq = [|0..[|strCompareArrMutable|].Length|]

        Seq.zip strCompareArrMutable numSeq
        |> Seq.collect(fun (seqComp,num) -> strSequencefied
                                            |> Seq.map(fun seq -> this.stringPairMatches seq seqComp
                                                                  |> fun (matches, newSeq) -> strCompareArrMutable
                                                                                              |> fun arr -> arr.[num] <- newSeq
                                                                                              |> fun _ -> matches))
        |> Seq.sum
        
    member this.getSolution (strChunk : string) (category : string) = 

        let methodRatingTuple =
            
            this.infoEv.Trigger(InfoEventArgs("Calculating rating methods", Brushes.Black)) 
            let methodsNodeAll = this.FailedTMDoc.XPathSelectElements(".//Method")
                                 |> Seq.filter(fun x -> x.XPathSelectElement("(..)[last()]").FirstAttribute.Value = category)
            
            methodsNodeAll
            |> Seq.map(fun method -> {  Name = method.FirstAttribute.Value ; 
                                        Info = this.getXmlValue method "Info" ;
                                        StringToAnalyze = this.StringToSequenceofSequence (this.getXmlValue method "StringToAnalyze") ;
                                        Ticket = this.getXmlValue method "Ticket" ;
                                        Solution = this.getXmlValue method "Solution";
                                        Category = category})
            |> Seq.map(fun method -> (method, this.getRating strChunk method.StringToAnalyze))
        
        this.infoEv.Trigger(InfoEventArgs(String.Format("Rating result looking on category {0}:\n\n", category),
                                          Brushes.Black ))

        let maxRating = 
            methodRatingTuple
            |> Seq.map(fun (meth, rating) -> this.infoEv.Trigger(InfoEventArgs(String.Format("Name: {0}, Rating: {1}, Ticket: {2}\n", meth.Name, rating, meth.Ticket),
                                                                  Brushes.Black ))
                                             rating)
            |> fun x -> x 

                        |> fun _ -> x |> Seq.max

        methodRatingTuple
        |> Seq.find(fun (_,rating) -> rating = maxRating)
        |> fun (method, _) -> method.Solution

    member this.GetFile (uploadChoice : FileType) = 

        let dialog = new OpenFileDialog()
        dialog.ShowDialog()
        |> ignore

        if dialog.FileName <> ""
        then None
             |> function
                
                | _ when uploadChoice = FileType.UploadFile ->
                    
                        this.infoEv.Trigger(InfoEventArgs(String.Format("Current upload file is: {0}", dialog.FileName),
                                                          Brushes.Black ))
                        |> fun  _ -> this.UploadFileName <- dialog.FileName
                        |> fun _ -> File.ReadAllText(dialog.FileName)
                        

                | _ -> 
                
                   this.infoEv.Trigger(InfoEventArgs(String.Format("Current solution is: {0}", dialog.FileName),
                                                     Brushes.Black ))

                   |> fun  _ -> this.SolutionFileName <- dialog.FileName
                   |> fun _ -> File.ReadAllBytes(dialog.FileName)
                   |> fun x -> Convert.ToBase64String(x)
        
        else ""

    member this.tryFindSolution (ticket : string) (textFileString : string) (solutionPrepared : string) = 

        let failedMethodStringChunk = 
            
            Regex.Match(textFileString,
                        @"(\*\*\*\* )(?:(?!(\*\*\*\* )|(Fail ))(.|\n))*?(Fail )").Value

        this.infoEv.Trigger(InfoEventArgs(String.Format("Getting failed method chunk:\n\n{0}\n\n", failedMethodStringChunk),
                                     Brushes.Black))

        let failedMethodName = Regex.Match(failedMethodStringChunk, "(\*\*\*\* )(\n|.)*?( \*\*\*\*)").Value
                               |> fun str -> str.Replace("*","")
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

        let mutable uploadSolOrNot = UploadSolution.No 

        let solution = 
            
            solutionPrepared
            |> function
               | _ when solutionPrepared = "" ->
                    
                    let answer = MessageBox.Show("Do you wish to store solution for future comparison?",
                                                "Warning",
                                                MessageBoxButton.YesNoCancel,
                                                MessageBoxImage.Question)
                    answer
                    |> function
                       
                       | _ when answer = MessageBoxResult.Yes &&
                                         (this.Sender.TicketComboBox.Text = "" ||
                                          this.Sender.TicketComboBox.ItemsSource
                                          |> Array.exists(fun str -> str = this.Sender.TicketComboBox.Text)) ->
                            
                            MessageBox.Show("Sorry mate, a (unique) ticket number is required in order to store a solution" +
                                            ". Redo the procedure, but add a (unique) ticket number as well!",
                                            "Error",
                                            MessageBoxButton.OK,
                                            MessageBoxImage.Error)
                            |> fun _ -> ""
                       
                       | _ -> 
                            
                            if answer = MessageBoxResult.Yes 
                            then uploadSolOrNot <- UploadSolution.Yes
                            this.infoEv.Trigger(InfoEventArgs("Getting solution", Brushes.Black))
                            this.getSolution failedMethodStringChunk category

               | _ -> 
                    
                    uploadSolOrNot <- UploadSolution.Yes
                    this.infoEv.Trigger(InfoEventArgs("Getting prepared solution", Brushes.Black))
                    |> fun _ -> solutionPrepared

        this.infoEv.Trigger(InfoEventArgs("Solution obtained! View solution by clicking \"Open Solution\"",
                             Brushes.Black))
        
        let getMethodXElement =
            
            let elementSequence = seq[  {|Name = "Info" ; Value = ""|} ;
                                        {|Name = "Ticket" ; Value = ticket|} ;
                                        {|Name = "Solution" ; Value = solution|} ;
                                        {|Name = "StringToAnalyze" ; Value = failedMethodStringChunk|}
                                        ]

            new XElement(XName.Get "Method",
                         XAttribute(XName.Get "Name", failedMethodName),
                         this.toXElementSequence elementSequence)

        if uploadSolOrNot = UploadSolution.Yes
        then getMethodXElement
             |> fun methodXElement -> this.FailedTMDoc.XPathSelectElement(String.Format("*//Category[@Name = '{0}']", category)).Add(methodXElement)
             |> fun _ -> let embeddedCritPath = exAssembly.GetManifestResourceNames()
                         this.FailedTMDoc.Save(embeddedCritPath.[1]
                                               |> fun x -> x.Replace(".", "\\")
                                               |> fun x -> x.Replace("\\xml", ".xml")
                                               |> fun x -> "..\\..\\..\\" + x)
                         this.Sender.TicketComboBox.ItemsSource <- this.FailedTMDoc.XPathSelectElements("*//Ticket")
                                                                   |> Seq.map(fun el -> el.FirstAttribute.Value)
                                                                   |> fun x -> x |> Seq.toArray
        
        this.SolutionFileName <- "Ticket no " + ticket
        solution
