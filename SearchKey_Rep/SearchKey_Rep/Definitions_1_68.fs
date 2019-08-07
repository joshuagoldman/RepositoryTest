
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
          InfoTextExtended : string
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

    type specialVarOption = 
        | Yes
        | No
