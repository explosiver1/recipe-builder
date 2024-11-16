using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecipeBuilder.Models;

public class MPMeal()
{
    //public string mealDescription = "";
    //public string mealNotes = "";
    //public List<Recipe> recipes = new List<Recipe>();
    public List<string> recipeNames = new List<string>();
    public DateOnly date;
    public int mealNum;

    //public void AddRecipe (Recipe recipe)
    //{
    //    recipes.Add(recipe);
    //}

    //    public void AddMeal (MealSet meal)
    //{
    //    mealDescription.Concat(meal.Description);
    //    foreach (Recipe recipe in meal.Recipes)
    //    {
    //        recipes.Add(recipe);
    //    }
    //}
}