using System;
using System.ComponentModel.DataAnnotations;
using RecipeBuilder.Models;

namespace RecipeBuilder.ViewModels{
    public class MealsEditVM()
    {
        public string? mealName { get; set; }
        public string? mealDescription { get; set; }
        public Recipe? recipeNew { get; set; }
        public List<string> UserRecipesNames { get; set; } = new List<string>();
        public List<string> mealRecipes { get; set; } = new List<string>();
    }
}
