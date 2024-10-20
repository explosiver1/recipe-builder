using System;
using System.ComponentModel.DataAnnotations;
using RecipeBuilder.Models;

namespace RecipeBuilder.ViewModels
{
    public class RecipeSelectVM
    {
        public required string CookbookName { get; set; } = string.Empty;
        public required List<Recipe> Recipes { get; set; } = new List<Recipe>(); // List of recipes
        public required List<Recipe> SelectedRecipes { get; set; } = new List<Recipe>(); // List of selected recipes
    }
}