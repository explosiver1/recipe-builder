using System;
using System.ComponentModel.DataAnnotations;
using RecipeBuilder.Models;

namespace RecipeBuilder.ViewModels
{
    public class RecipeLookVM
    {
        // Uncomment if you need to track the user viewing the recipe
        // public required string userName { get; set; }

        public required string CookbookName { get; set; } = string.Empty;
        public required Recipe Recipe { get; set; } = new Recipe(); // Initialize a new Recipe object
    }
}