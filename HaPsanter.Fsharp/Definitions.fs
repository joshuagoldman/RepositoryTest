namespace HaPsanter.Fsharp

module Definitions =
    
    type Position =
        {   X : float
            Y : float
            }

    type LocationSettings =
        {   Music : string
            Position : Position
            Picture : string
            }

    type Location =
        | Street of LocationSettings
        | House of LocationSettings
        | Syngoge of LocationSettings
        | Park of LocationSettings
    
    type Person =
        {   Location : Location
            Name : string
            }

