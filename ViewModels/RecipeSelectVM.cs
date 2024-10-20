using System;
using System.ComponentModel.DataAnnotations;
using RecipeBuilder.Models;

namespace RecipeBuilder.ViewModels
{
    public class RecipeSelectVM
    {
        public required string cookbookName { get; set; } = string.Empty;
        public required List<Recipe> recipes { get; set; } = new List<Recipe>(); // List of recipes
        public required List<Recipe> selectedRecipes { get; set; } = new List<Recipe>(); // List of selected recipes
    }
}