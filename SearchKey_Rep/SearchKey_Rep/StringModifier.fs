namespace SearchKeyRep

open SearchKey_GUI.Methods
open SearchKey_GUI
open System.Threading
open System.Windows
open System.Windows.Controls
open System.IO
open System.Linq

module StringModifier =

    let TableStrings = 
        "KRC 161 490/1	2217 B1	R2C	Perform RCO 156 25-KRC 161 490/1-1 Uen,
        KRC 161 490/1	2217 B1	R2D	Perform RCO 156 25-KRC 161 490/1-1 Uen,
        KRC 161 490/2	2217 B1	R2C	Perform RCO 156 25-KRC 161 490/1-1 Uen,
        KRC 161 490/2	2217 B1	R2D	Perform RCO 156 25-KRC 161 490/1-1 Uen,
        KRC 161 500/1	2217 B1/3	R1E	Perform RCO 156 25-KRC 161 490/1-1 Uen,
        KRC 161 427/1	2217 B3	R1D	Perform RCO 156 25-KRC 161 490/1-1 Uen,
        KRC 161 566/1	2217 B5	R1E	Perform RCO 156 25-KRC 161 490/1-1 Uen,
        KRC 161 566/1	2217 B5	R1H	Perform RCO 156 25-KRC 161 490/1-1 Uen,
        KRC161 428/1	2217 B7	R2B	Perform RCO 156 25-KRC 161 490/1-1 Uen,
        KRC161 428/1	2217 B7	R2C	Perform RCO 156 25-KRC 161 490/1-1 Uen,
        KRC 161 685/1	2217 B7A	R1A	Perform RCO 156 25-KRC 161 490/1-1 Uen,
        KRC 161 548/1	2217 B8	R1H	Perform RCO 156 25-KRC 161 490/1-1 Uen,
        KRC 161 548/2	2217 B8	R1H	Perform RCO 156 25-KRC 161 490/1-1 Uen,
        KRC 161 568/2	2217 B11	R1D	Perform RCO 156 25-KRC 161 490/1-1 Uen,
        KRC 161 549/1	2217 B20	R1J	Perform RCO 156 25-KRC 161 490/1-1 Uen,
        KRC 161 592/1	2217 B26D	R1E	Perform RCO 156 25-KRC 161 490/1-1 Uen,
        KRC 161 550/1	2217 B28A	R1F	Perform RCO 156 25-KRC 161 490/1-1 Uen,
        KRC 161 550/2	2217 B28A	R1F	Perform RCO 156 25-KRC 161 490/1-1 Uen,
        KRC 161 499/1	2217 B28B	R1E	Perform RCO 156 25-KRC 161 490/1-1 Uen,
        KRC 161 703/1	2217 B66A	R1A	Perform RCO 156 25-KRC 161 490/1-1 Uen,
        KRC 161 647/1	2218 B42B	R1A	Perform RCO 156 25-KRC 161 616/1-3 Uen,
        KRC 161 616/1	2218 B42B	R1D	Perform RCO 156 25-KRC 161 616/1-3 Uen,
        KRC 161 606/1	2218 B42	R1D	Perform RCO 156 25-KRC 161 616/1-3 Uen,
        KRC 161 624/1	2212 B1	R1E	Perform RCO 156 25-KRC 161 624/2-1 Uen,
        KRC 161 624/1	2212 B1	R1E/A	Perform RCO 156 25-KRC 161 624/2-1 Uen,
        KRC 161 688/3	2212 B2 B25	R1A	Perform RCO 156 25-KRC 161 624/2-1 Uen,
        KRC 161 688/3	2212 B2 B25	R1B	Perform RCO 156 25-KRC 161 624/2-1 Uen,
        KRC 161 496/1	2212 B3	R2C/A	Perform RCO 156 25-KRC 161 624/2-1 Uen,
        KRC 161 496/1	2212 B3	R2C/B	Perform RCO 156 25-KRC 161 624/2-1 Uen,
        KRC 161 496/1	2212 B3	R2E	Perform RCO 156 25-KRC 161 624/2-1 Uen,
        KRC 161 652/3	2212 B5	R2D	Perform RCO 156 25-KRC 161 624/2-1 Uen,
        KRC 161 652/3	2212 B5	R2E	Perform RCO 156 25-KRC 161 624/2-1 Uen,
        KRC 161 630/1	2212 B66A	R2C	Perform RCO 156 25-KRC 161 624/2-1 Uen,
        KRC 161 630/1	2212 B66A	R2C/A	Perform RCO 156 25-KRC 161 624/2-1 Uen,
        KRC 161 630/1	2212 B66A	R2C/B	Perform RCO 156 25-KRC 161 624/2-1 Uen,
        KRC 161 630/1	2212 B66A	R2D	Perform RCO 156 25-KRC 161 624/2-1 Uen,
        KRC 161 630/3	2212 B66A	R2B	Perform RCO 156 25-KRC 161 624/2-1 Uen,
        KRC 161 630/3	2212 B66A	R2C/A	Perform RCO 156 25-KRC 161 624/2-1 Uen,
        KRC 161 630/3	2212 B66A	R2C/B	Perform RCO 156 25-KRC 161 624/2-1 Uen,
        KRC 161 630/3	2212 B66A	R2C	Perform RCO 156 25-KRC 161 624/2-1 Uen,
        KRC 161 628/1	2212 B7	R1D	Perform RCO 156 25-KRC 161 624/2-1 Uen,
        KRC 161 628/1	2212 B7	R1D/A	Perform RCO 156 25-KRC 161 624/2-1 Uen,
        KRC 161 628/2	2212 B7	R1D	Perform RCO 156 25-KRC 161 624/2-1 Uen,
        KRC 161 628/2	2212 B7	R1D/A	Perform RCO 156 25-KRC 161 624/2-1 Uen,
        KRC 161 628/2	2212 B7	R1F	Perform RCO 156 25-KRC 161 624/2-1 Uen,
        KRC 161 650/1	2212 B8	R2D	Perform RCO 156 25-KRC 161 624/2-1 Uen,
        KRC 161 650/1	2212 B8	R2E	Perform RCO 156 25-KRC 161 624/2-1 Uen,
        KRC 161 631/3	2212 B13	R1C	Perform RCO 156 25-KRC 161 624/2-1 Uen,
        ".Split ','


    type ProdInfo = 
        {   ProdNumber : string
            RState : string
        }

    type Crits = 
        {   Crit1 : ProdInfo []
            Crit2 : ProdInfo []
            Crit3 : ProdInfo []
        }

    let RStateObtain (str : string) = 
        let InitialRStateString = str.Replace("\r\n        ","").Substring(20, 16)
        let strWSpace = InitialRStateString.Substring(InitialRStateString.IndexOf('\t'), 
                                                      InitialRStateString.IndexOf('\t', InitialRStateString.IndexOf('\t') + 1) - InitialRStateString.IndexOf('\t'))
        strWSpace.Replace("\t", "")
        

    let GetProdFrString (prodString : string) = 
        {ProdNumber = prodString.Replace("\r\n        ","").Substring(0,13) ; RState = RStateObtain prodString}

    let GetCritProdInfo (tableString : string)  =
        {ProdNumber = (GetProdFrString tableString).ProdNumber ; RState = (GetProdFrString tableString).RState}

    let ReWriteEquals (prod : ProdInfo ) (prods : ProdInfo []) =
        prods
        |> Array.filter(fun prod_comp -> prod_comp = prod)
        |> fun prods -> {ProdNumber = prods.First().ProdNumber ;
                         RState = String.concat "," (prods |> Array.map(fun prod -> prod.RState))}

    let HasEquals (prod : ProdInfo ) (prods : ProdInfo []) =
        prods
        |> Array.filter(fun prod_comp -> prod_comp.RState <> prod.RState)
        |> Array.map(fun prod -> prod.ProdNumber)
        |> function
            |temp when temp.Length < 1 -> false
            |temp when temp.Length > 1 -> true
            | _ -> false
                       
    let PairEquals (prods : ProdInfo []) =
        let HasNoEquals = 
            prods
            |> Array.filter(fun prod -> HasEquals prod prods = false)
        let HasEquals = 
            prods
            |> Array.filter(fun prod -> HasEquals prod prods = true)
            |> Array.map(fun prod -> ReWriteEquals prod prods) 
        
        (HasEquals.Concat HasNoEquals).ToArray() 

    let CritFilter (tableStrings : string []) (critString : string)  = 
       tableStrings
       |> Array.filter(fun str -> str.Contains(critString))
       |> Array.map(fun str -> GetCritProdInfo str)
       |> fun strArr ->  PairEquals strArr

    let FinalArray (tableStrings : string []) =
        {Crit1 = CritFilter  tableStrings "Perform RCO 156 25-KRC 161 490/1-1 Uen" ;
         Crit2 = CritFilter tableStrings "Perform RCO 156 25-KRC 161 616/1-3 " ;
         Crit3 = CritFilter tableStrings "Perform RCO 156 25-KRC 161 624/2-1 Uen"}
    
    let StringCreation (prodArr : ProdInfo []) =
        prodArr
        |> Array.map(fun prod -> "ProductNumberNEXT" + prod.ProdNumber +  "NEXTRStateNEXT" + prod.RState + "\n")
        |> String.concat "\n"

    let FinalString (crit : Crits) =
        let first = StringCreation crit.Crit1
        let sec = StringCreation crit.Crit2
        let third = StringCreation crit.Crit3

        [|first ; sec ; third|]
    
    let Write2TextFile (str : string) (count : string) =  
        let Lines = (String.concat "," (List.replicate 200 "-")).Replace(",","") + "\n"
        let Heading = (String.concat "," (List.replicate 50 " ")).Replace(",","")  + "Criteria " + count + "\n"
        File.AppendAllText("C:\Users\jogo\Documents\jogo\CritText.txt", Lines + Heading + Lines + str)
    
    File.WriteAllLines("C:\Users\jogo\Documents\jogo\CritText.txt", Array.empty)
    let FinStr = FinalString (FinalArray TableStrings)
    Seq.zip FinStr  [|"A";"B";"C"|]
    |>  Seq.iter(fun (str, num) -> Write2TextFile str num)
    |> ignore