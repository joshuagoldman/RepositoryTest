namespace Vradim.Models

module EnvironmentData =
    
    type Daytime =
        | SunRise of string
        | Morning of string
        | Lunch of string
        | AfterNoon of string 
        | Evening of string
        | Night of string

    type SensoryData =
        {   Music : string
            Picture : Daytime
            }

    type Points =
        | Lowest of int
        | Lower of int
        | Higher of int
        | Highest of int

    type Alternative =
        | First of Points
        | Second of Points
        | Third of Points
        | Fourth of Points

    type Assignment =
        { Question : string
          Answer : Alternative
        }
