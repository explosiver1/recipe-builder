using System;

namespace RecipeBuilder.Models;

public static class SeedData
{
    /* SAMPLE DATA */ 

    /* Ingredient Data */
    private static List<Ingredient> myIngredients = [
        new Ingredient{Name = "Sugar", Description="White granulated sugar", Unit="Cups"},
        new Ingredient{Name = "Egg", Description="Large White Chicken Eggs", Unit="Dozen"},
        new Ingredient{Name = "Brown Sugar", Description="Light brown sugar", Unit="Cups"},
        new Ingredient{Name = "Powdered Sugar", Description="Powdered sugar", Unit="Cups"},
        new Ingredient{Name = "Flour", Description="Bleached all-purpose flour", Unit="Cups"},
        new Ingredient{Name = "Vanilla", Description="Pure vanilla extract", Unit="tsp"},
        new Ingredient{Name = "Baking Soda", Description="Sodium Bicarbonate", Unit="tsp"},
        new Ingredient{Name = "Baking Powder", Description="Baking powder", Unit="tsp"}
        ];
    /* Recipe Data */
    private static List<Recipe> myRecipes = [
        new Recipe{
            Name="", Description="", 
            Difficulty=0, Rating=0, CookTime=10, 
            servingSize = {{"Cup", 1}}, numServings=12, 
            Tags = [],
            equipment = [],
            Ingredients = [],
            Instructions = []
            },
        new Recipe{},
        new Recipe{},
        new Recipe{}
    ];

    /* Cookbook Data */
    private static List<Cookbook> myCookbooks = [
        new Cookbook{
            Title = "", Description ="",
            Recipes =[]
        },
        new Cookbook{
            Title = "", Description ="",
            Recipes =[]
        }
    ];
    /* User Data */
    // private static ;

    /* GET METHODS */ 

}