namespace SearchKeyRep

open System.IO
open System.Reflection
open System
open System.Windows.Forms
open System.Linq

module SK_1_50 =
    
    let ruFile = File.ReadAllText("C:\Users\jogo\Documents\SerialNumberList\RU_Rev_F_Test.txt")

    let duFile = File.ReadAllText("C:\Users\jogo\Documents\SerialNumberList\DU_Rev_F_Test.txt")

    let duDistSeqAnonRec = 
        duFile
        |> fun x -> x.Split( [|"\r\n"|], StringSplitOptions.None) 
        |> Seq.distinct
        |> Seq.map(fun str -> str
                              |> fun x -> x.Split ';'
                              |> Seq.toArray
                              |> function
                                 | x when x.Length > 1 -> {|ProductNumber = x.[0] ; Name = x.[1]|}
                                 | _ -> {|ProductNumber = "" ; Name = ""|})
    

    let duDistSeq =
        duDistSeqAnonRec
        |> Seq.filter(fun anRec -> anRec.Name.Contains("DU"))
        |> Seq.map(fun anRec -> anRec.ProductNumber)

    let basebandDistSeq =
        duDistSeqAnonRec
        |> Seq.filter(fun anRec -> anRec.Name.Contains("Baseband"))
        |> Seq.map(fun anRec -> anRec.ProductNumber)

    let ruDistSeq = 
        ruFile
        |> fun x -> x.Split( [|"\r\n"|], StringSplitOptions.None) 
        |> Seq.distinct
    
    let anonymRecord = seq[
                            {|Info = "---------------------------------" +
                                     "\n---------------------------------\n" +
                                     "RU product numbers:" +
                                     "\n---------------------------------" +
                                     "\n---------------------------------\n" ;
                               StrSeq = ruDistSeq|}  ;
                              
                            {|Info = "---------------------------------" +
                                     "\n---------------------------------\n" +
                                     "DU product numbers:" +
                                     "\n---------------------------------" +
                                     "\n---------------------------------\n";
                              StrSeq = duDistSeq|}

                            {|Info = "---------------------------------" +
                                     "\n---------------------------------\n" +
                                     "Baseband product numbers:" +
                                     "\n---------------------------------" +
                                     "\n---------------------------------\n";
                              StrSeq = basebandDistSeq|}
                            
                            ]

    let finalString =

        anonymRecord
        |> Seq.map(fun anRec -> anRec.StrSeq
                                |> Seq.map(fun str -> str + "\n")
                                |> String.concat ","
                                |> fun x -> anRec.Info + x.Replace(",", ""))
        |> String.concat ","
        |> fun x -> x.Replace(",", "")

    finalString
    |> ignore
    


