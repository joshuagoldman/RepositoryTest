namespace Vradim.Locations

module Locations =
    
    type DishInfo =
        {   Name : string
            Kcal : int
            Taste : float
            Protein : int
            Fats : int
            Carbs : int}

    type Dish =
        {   Cake : DishInfo
            CornflakesWMilk : DishInfo
            MeatLoaf : DishInfo
            PastaCarbonara : DishInfo
            Salad : DishInfo
            Sushi : DishInfo}

    type MealType =
        | BreakFast
        | Lunch
        | Dinner

    type Locations =
        | Home
        | School
        | Park
        | Friend
