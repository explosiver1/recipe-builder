using System;
using System.ComponentModel.DataAnnotations;
using RecipeBuilder.Models;

namespace RecipeBuilder.ViewModels
{
    public class CookbooksEditVM
    {
        public string? cookbookName { get; set; }
        public string? cookbookDescription { get; set; }
        public Recipe? recipeNew { get; set; }
        public List<string> UserRecipesNames { get; set; } = new List<string>();
        public List<string> cookbookRecipes { get; set; } = new List<string>();
    }
}
