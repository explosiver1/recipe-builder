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
        // For populating drop down
        public List<string> UserRecipesNames { get; set; } = new List<string>();
        //For populating current recipe list of cookbook
        public List<string> cookbookRecipes { get; set; } = new List<string>();
        // To keep track of recipe names that need added to db
        public List<string> recipesToAdd { get; set; } = new List<string>();
    }
}
