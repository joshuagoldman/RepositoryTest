

namespace SearchKeyRep.Definitions

open System
open System.Windows
open System.Reflection
open System.IO
open System.Diagnostics
open SearchKeyRep.Transpose
open System.Text.RegularExpressions


module Definitions_1_68 =

    type SearchKeyElements = 
        { SearchKey : string 
          Variable : string
          Filter : string
          Date : string
          Infotext : string
          Product : string
          CriteriaReferenceWithRevision : string
          Expression : string
          }

    type KeyStringChunkInfo = 
        {   Key : string
            ChunkStart : string
            ChunkEnd : string 
            }
    

    type VarInfoTxtCompPair =
        { Var : string
          Info : string
          }

    type Table =
        { TableInfo : VarInfoTxtCompPair[]
          }
    
    type VarLogicType =
         { Var : string
           Index : int
           }

    type PLLInfos = 
        {   Conditions : string[][]
            InfoTextComponent : string[]
            }

    type TableInfo = 
        {   ArrMaxIndex : int
            PartitionSize : int
            FirstPartition : int[]
            LastPartition : int[]
                    
            }

    type ProdInfo = 
        {   ProdNumber : string
            Revision : string                    
            }

    let fileIndexDict = 

        dict[   
                0, "ee_esi.log" ;
                1, "syslog.txt"
            ]

    type Options = 
        | None
        | X1 
        | X2 

    let isVariable (cond : string) =
        
        match cond with
        |"Y" -> Options.X1
        | "N" -> Options.X2
        | _ -> Options.None

    let int2Alphabet (num : int) = 
        
        match num with
        | 0 -> "a" | 1 -> "b" | 2 -> "c" | 3 -> "d" | 4 -> "e"  | 5 -> "f" | 6 -> "g" | 7 -> "h"
        | 8 -> "i" | 9 -> "j" | 10 -> "k" | 11 -> "l" | 12 -> "m" | 13 -> "n" | 14 -> "o" | 15 -> "p"
        | 16 -> "q" | 17 -> "r" | 18 -> "s" | 19 -> "t" | 20 -> "u" | 21 -> "v" | 22 -> "w" | _ -> ""

    type specialVarOption = 
        | Yes
        | No
