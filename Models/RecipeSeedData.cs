using System;

namespace RecipeBuilder.Models;

public static class RecipeSeedData
{

    public static IngredientDetail porkchop = new IngredientDetail{Ingredient=new Ingredient {Name = "Porkchop"}, Quantity = 2, Unit ="chops"};
    public static IngredientDetail lambchop = new IngredientDetail{Ingredient=new Ingredient {Name = "Lambchop"}, Quantity = 3, Unit ="chops"};
    public static IngredientDetail seasoning = new IngredientDetail{Ingredient=new Ingredient {Name = "Seasoning"}, Quantity = 5, Unit ="TBSP"};

    public static Recipe porkchopRecipe = new Recipe { 
        Name = "Pork Chops", Instructions = ["Season porkchop","Cook porkchop"], 
        Difficulty = 1, Rating = 3, CookTime = 45, 
        Ingredients = [porkchop,seasoning]          
            };

    public static Recipe lambchopRecipe = new Recipe { 
        Name = "Lamb Chops", Instructions = ["Season lambchop","Cook lambchop"], 
        Difficulty = 1, Rating = 3, CookTime = 50, 
        Ingredients = [lambchop,seasoning]          
            };

    public static Cookbook dessertsCookbook = new Cookbook { Title = "desserts", Recipes = []};
    public static Cookbook chopsCookbook =new Cookbook { Title = "Chops", Recipes = [porkchopRecipe, lambchopRecipe]};
    public static List<Cookbook> cookbooks = [dessertsCookbook, chopsCookbook];
    public static Cookbook GetCookbook(string cookbookName)
    {
        if (cookbookName == dessertsCookbook.Title)
        {
            return dessertsCookbook;
        }
        else if (cookbookName == chopsCookbook.Title)
        {
            return chopsCookbook;
        }
        else
        {
            return new Cookbook();
        }
    }

    public static Recipe GetRecipe(string cookbookName, string recipeName)
    {
        Cookbook cookbook;
        if (cookbookName == dessertsCookbook.Title)
        {
            cookbook = dessertsCookbook;
            foreach (Recipe recipe in cookbook.Recipes)
            {
                if (recipeName == recipe.Name)
                {
                    return recipe;
                }
            }
        }
        else if (cookbookName == chopsCookbook.Title)
        {
            cookbook = chopsCookbook;
            foreach (Recipe recipe in cookbook.Recipes)
            {
                if (recipeName == recipe.Name)
                {
                    return recipe;
                }
            }
        }
        return new Recipe();

        // if (recipeName == porkchopRecipe.Name)
        // {
        //     return porkchopRecipe;
        // }
        // else if (recipeName == lambchopRecipe.Name)
        // {
        //     return lambchopRecipe;
        // }
        // else
        // {
        //     return new Recipe();
        // }
    }

    public static Boolean AddRecipe(string cookbookName, Recipe newRecipe)
    {
        Cookbook cookbook;
        if (cookbookName == dessertsCookbook.Title)
        {
            cookbook = dessertsCookbook;
            cookbook.AddRecipe(newRecipe);
            return true;
        }
        else if (cookbookName == chopsCookbook.Title)
        {
            cookbook = chopsCookbook;
            cookbook.AddRecipe(newRecipe);
            return true;
        }
        return false;
    }
}
