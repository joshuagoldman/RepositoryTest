module RelAutomation4XlFile

open Microsoft.Office.Core
open System.IO
open System.Diagnostics
open Microsoft.Win32
open ExcelDataReader
open ExcelDataReader.Core
open System.IO.Compression
open Ionic.Zip


let str = "#2903 PRTT - R10D - ESI wasn’t collected]	]	]	PRTT]	N/A]	PRTT/LAT Team]	]	Danijel Kudric, Ericsson Zagreb Croatia]	N/A]	Yes]	43774]	43791]	Closed]	PRTT PO]	R10F_fix]
#2928 Re: LAT Test RBS6K: Log Analyze Issue for  KRC 161 282/2 and KRC 161 297/2]	HIGH]	]	LAT]	N/A]	PRTT/LAT Team]	]	Dinesh Kumawat, Jabil Pune India]	IN-JABPU]	]	43786]	43791]	Closed]	LAT PO]	R10F_fix]
#2932 LAT - 925/41 & /31 units are failing]	URGENT]	]	LAT]	ERS]	PRTT/LAT Team]	]	Vinod, Ericsson, Kuala Lumpur Malaysia]	MY-ERIKL]	]	43787]	2019-11-57]	Closed]	LAT PO]	R10F_fix]
#2943 PRTT - KRC 161 752/1 issue]	]	]	PRTT]	N/A]	PRTT/LAT Team]	]	Carlos Torres, Jabil Guadalajara, Mexico]	MX-JABGU]	]	43790]	43794]	Closed]	PRTT PO]	R10F_fix]
#2952 Re: PRTT test is failed on \"MTD process\"]	URGENT]	]	PRTT]	N/A]	PRTT/LAT Team]	]	Aleksandr Nesterenko, Ericsson Tallinn]	EE-ERITA]	]	43791]	43795]	Closed]	PRTT PO]	R10F_fix]
#2978 Re: PRTT test issue concerning the implementation of RCO. ]	]	]	PRTT]	ERS]	PRTT/LAT Team]	]	Ilir Lampros, Ericsson Tallinn]	EE-ERITA]	]	43801]	43804]	Closed]	PRTT PO]	R10F_fix]
#2982 Re: Release R10F RCO Issue ( PRTT ; severity: High)]	HIGH]	]	PRTT]	N/A]	PRTT/LAT Team]	]	Yudi, Syntronic Indonesien]	ID-SYNJA]	]	43802]	]	Open]	PRTT PO]	R10F_fix]
#2985 Re: Perform RCO 156 25-KRC 118 91/2-3 failed in RTT release mail, R10F, CN-ERIGZ]	]	]	PRTT]	N/A]	PRTT/LAT Team]	]	Mars Huang, Ericsson Guangzhou China]	CN-ERIGZ]	]	43803]	]	Open]	PRTT PO]	R10F_fix]
#3010 Re: Flex BUD RC - LAT test issue with BB 52XX *Urgent]	URGENT]	]	LAT]	ERS]	PRTT/LAT Team]	]	Norbert Horvath, Flex Budapest, Hungary]	HU-FLXBU]	]	43810]	43810]	Closed]	LAT PO]	R10F_fix]
#2991 Re: Flex BUD RC - PRTT problem with KRC 118 91/2 *Urgent]	URGENT]	]	PRTT]	N/A]	PRTT/LAT Team]	]	Norbert Horvath, Flex Budapest, Hungary]	HU-FLXBU]	]	43804]	43804]	Closed]	PRTT PO]	R10F_fix]
#3017 Re: KRC161296/1 R1V; SW update for SC RY/NL]	]	]	PRTT]	]	]	]	]	]	]	]	]	]	]	]"

let pickPos (pos : int) (sequence : seq<string>) = 
    sequence
    |> Seq.map (fun row -> row.Split ']'
                           |> fun x -> x.[pos].Trim() + "\n")
    |> String.concat ","
    |> fun x -> x.Replace(",", "")

let final = 
    str
    |> fun x -> x.Split '\n'
    |> Seq.filter (fun str -> str.Split ']'
                              |> fun arr -> arr.[12].Trim().ToLower() = "closed")
    |> fun x -> pickPos 0 x + pickPos 3 x



let fileNames = seq
                    [
                        "Prtt_R10F_1_Rel-BangladeshDhaka"
                        "Prtt_R10F_1_Rel-BrazilSaoJoseDosCampos"
                        "Prtt_R10F_1_Rel-ChinaGuangzhou"
                        "Prtt_R10F_1_Rel-ChinaNanjing"
                        "Prtt_R10F_1_Rel-ChinaWuxia"
                        "Prtt_R10F_1_Rel-EstoniaTallinn"
                        "Prtt_R10F_1_Rel-HollandRijen"
                        "Prtt_R10F_1_Rel-HungaryBudapest"
                        "Prtt_R10F_1_Rel-IndiaPune"
                        "Prtt_R10F_1_Rel-IndonesiaJakarta"
                        "Prtt_R10F_1_Rel-JabilMexicoGuadalajara"
                        "Prtt_R10F_1_Rel-JapanNarita"
                        "Prtt_R10F_1_Rel-MalaysiaKualaLumpur"
                        "Prtt_R10F_1_Rel-SanminaMexicoGuadalajara"
                        "Prtt_R10F_1_Rel-USADallas"
                    
                    ]

let filePath = "C:\\Users\\jogo\\Documents\\git_Test\\r10F_rELEASE"
let unzipFilePath = "\\\\rubin\Projects\Ericsson\RC Global Test Support\_LAT & PRTT Test SW Release\Release Templates\PRTT\R10F V2"

let zipFile (fileName : string) =
    using (new ZipFile())
        (fun zip -> 

            zip.AddDirectory (filePath +  "\\" + fileName)
            |> fun _ -> zip.Save (filePath +  "\\" + fileName + ".zip")
        )
    |> ignore 

let fileProcedure (fileName : string) =
    let thisFilePath = filePath +  "\\" + fileName + "\\TemplateInfo.xml"
    File.ReadAllText thisFilePath
    |> fun x -> x.Replace("<TestType>1/LPA_R10F</TestType>", "<TestType>1/LPA_R10F_1</TestType>")
    |> fun x -> x.Replace("<Name>Prtt_R10F_Rel</Name>", "<Name>Prtt_R10F_1_Rel</Name>")
    |> fun x ->
        File.WriteAllText (thisFilePath, x)

    zipFile fileName

let unzipFile (fileName : string) =
    let thisFilePath = unzipFilePath +  "\\" + fileName + ".zip"

    using (ZipFile.Read thisFilePath)
        (fun zip -> 

            zip.ExtractAll (filePath +  "\\" + fileName)
        )
    fileProcedure fileName 
    
let procedure = 
    fileNames
    |> Seq.iter (fun file_name -> fileProcedure file_name )

procedure
|> ignore




