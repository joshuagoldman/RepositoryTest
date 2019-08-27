namespace Vradim.Models

open Vradim.Models.EnvironmentData
open Vradim.Models.Location
open Vradim.Models.Progression

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
        | Mike of Traits * Location * Status
        | Sam of Traits * Location * Status
        | Lara of Traits * Location * Status
        | Synthia of Traits * Location * Status

