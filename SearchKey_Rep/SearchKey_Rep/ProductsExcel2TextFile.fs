module ProductsExcel2TextFile

open Microsoft.Office.Core
open System.IO
open System.Diagnostics
open Microsoft.Win32
open ExcelDataReader
open ExcelDataReader.Core




let getxlColumns (filePath : string) =
    let  stream = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite)
    let  dataReader = ExcelReaderFactory.CreateOpenXmlReader(stream)
    let dataSet = dataReader.AsDataSet()
    let table = dataSet.Tables.[0]

    let mutable result = Seq.empty :> seq<seq<string>>


    [0..table.Columns.Count - 1]
    |> Seq.iter (fun i -> 
                    let mutable temp = Seq.empty :> seq<string>
                    [0..table.Rows.Count - 1]
                    |> Seq.iter (fun j -> 
                                let kaka = table.Rows.[j].[i].ToString()

                                temp <- Seq.append temp [ kaka ])
                    result <- Seq.append result [temp] )

    
    let smallerRes =
        [0..15]
        |> Seq.map (fun pos -> result |> Seq.item(pos))

    smallerRes

getxlColumns "C:/Users/jogo/Documents/jogo/Ericsson/testing.xlsx"
|> ignore



