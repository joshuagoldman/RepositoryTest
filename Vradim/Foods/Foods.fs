namespace Vradim.Models

module Foods =
    
    type Nutrition =
        {   Name : string
            Kcal : int
            Protein : int
            Fats : int
            Carbs : int}

    type DishInfo =
        {   Name : string
            Taste : float
            Ingrediens : Nutrition[]}

    type Dish =
            |Cake of DishInfo
            |CornflakesWMilk of DishInfo
            |MeatLoaf of DishInfo
            |PastaCarbonara of DishInfo
            |Salad of DishInfo
            |Sushi of DishInfo

    type MealType =
        | BreakFast of Dish
        | Lunch of Dish
        | Dinner of Dish
