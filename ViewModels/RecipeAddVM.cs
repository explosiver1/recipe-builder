using System;
using System.ComponentModel.DataAnnotations;
using RecipeBuilder.Models;

namespace RecipeBuilder.ViewModels
{
    public class RecipeAddVM
    {
        // Uncomment if you need to track the user who is adding the recipe
        // public required string userName { get; set; }

        public required string CookbookName { get; set; } = string.Empty;
        public required Recipe Recipe { get; set; } = new Recipe();
    }
}