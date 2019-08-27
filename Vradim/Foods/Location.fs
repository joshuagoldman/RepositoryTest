namespace Vradim.Locations

namespace Vradim.Models

open Vradim.Models.Foods

module Location =
    


    type Location =
        | Home of MealType
        | School of MealType
        | Park of MealType
        | Friend of MealType
