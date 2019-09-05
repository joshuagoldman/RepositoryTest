namespace Vradim.GameLogic

open Vradim.Models.Persons
open System.Diagnostics
open Caliburn.Micro

module Metabolism =
  
    type Animal =
        | Frog
        | Hare
        | Butterfly
        | Dolphin

    type AnimalToShowEvent =
        {   Animal : Animal
            Season : string
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

    let gwtBackGroundImage (str : string) = 
        
        match str with
        |"fall" -> "Vradim.Pictures.Seasons.Fall.jpg"
        |"winter" -> "Vradim.Pictures.Seasons.Winter.jpg"
        |"summer" -> "Vradim.Pictures.Seasons.Summer.jpg"
        |"spring" -> "Vradim.Pictures.Seasons.Spring.jpg"
        | _ -> "Vradim.Pictures.Seasons.Vradim_Picture.jpg"
    

    let checkSeasonString (str : string) (_event : IEventAggregator)=
        
        let alternatives = seq [|{Season = "fall" ; Animal = Animal.Frog};
                                 {Season = "winter" ; Animal = Animal.Hare};
                                 {Season = "spring" ; Animal = Animal.Butterfly};
                                 {Season = "summer" ; Animal = Animal.Dolphin}|]
        
        let matchingStringExists = alternatives
                                   |> Seq.exists(fun alt -> alt.Season <> str)
        let sendEventWAnimal =  alternatives
                                |> Seq.find(fun alt -> alt.Season = str)
                                |> fun animal -> _event.PublishOnUIThreadAsync(animal)
                                |> ignore
                                
        if matchingStringExists then sendEventWAnimal

    