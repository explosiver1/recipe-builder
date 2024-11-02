using System;
using System.ComponentModel.DataAnnotations;
using RecipeBuilder.Models;

namespace RecipeBuilder.ViewModels
{
    public class MealsLookVM
    {
        // Uncomment if you need to track the user viewing the recipe
        // public required string userName { get; set; }
        public MealSet meal { get; set; }
    }
}