
namespace Vradim.Persons

module Persons =
    
    type BodyTraits =
        {   FatBurning : float
            MuscleBuilding : float
            DiabetiesTendency : float
            }

    type BodyType =
     {  Ectomorph : float
        Endomorph : float
        Mesomorph : float
        }

    let getMetabolism (bodyType : BodyType) =

        (bodyType.Ectomorph + bodyType.Endomorph + bodyType.Mesomorph) / 3.0

    let getMuscleBuilding (bodyType : BodyType) =

        (bodyType.Ectomorph + bodyType.Endomorph + bodyType.Mesomorph) / 3.0

    let getDiabetesTendency (bodyType : BodyType) =

        (bodyType.Ectomorph + bodyType.Endomorph + bodyType.Mesomorph) / 3.0

    let getBodyTraits (bodyType : BodyType) =
        
        bodyType
        |> fun x -> {   FatBurning = getMetabolism x ; 
                        MuscleBuilding = getMuscleBuilding x ;
                        DiabetiesTendency = getDiabetesTendency x
                        }
     
    type Socialenvironment =
        {   Friends : int
            FamilyStability : float
        }

    type Colour =
        | Black
        | Blonde
        | Brown

    type Texture =
        | Straight
        | Curly
        | Afro

    type HairType =
        {   Color : Colour
            HairTexture : Texture
        }

    type AppearanceTraits =
        {   Weight : int
            Height : float
            Hair : HairType
        }

    type Traits = 
        { Appearance : AppearanceTraits
          Body : BodyTraits
          StressResistance : decimal
          Social : Socialenvironment
          Age : int
          }

    type Person = 
    | Mike of Traits
    | Sam of Traits
    | Lara of Traits
    | Synthia of Traits

