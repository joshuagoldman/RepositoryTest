namespace SearchKeyRep

open System.IO
open System.Reflection
open System
open System.Windows.Forms
open System.Linq
open System.Text.RegularExpressions

module SK_2_41 =
    
    let file = File.ReadAllText("C:\Users\jogo\Documents\Search_Keys_Preparation_Docs\SK_2_41.txt")

    let anRecSeq =
        file
        |> fun str -> str.Split( [|"\r\n"|], StringSplitOptions.None)
        |> Seq.map(fun str -> str.Split ';')
        |> Seq.filter(fun arr -> arr.[0] <> "")
        |> Seq.map(fun prod -> {|Name = prod.[0].Trim() ; 
                                 Rstate = prod.[2].Trim()|})

    let prodString = 
        anRecSeq
        |> Seq.map(fun anReq -> "ProductNumberNEXT" + anReq.Name + "NEXTRState_ToNEXT" + anReq.Rstate + "\n")
        |> String.concat ","
        |> fun str -> str.Replace(",", "")

    prodString
    |> ignore