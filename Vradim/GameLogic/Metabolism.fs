namespace Vradim.GameLogic

open Vradim.Models.Persons

module GameLogic =

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
