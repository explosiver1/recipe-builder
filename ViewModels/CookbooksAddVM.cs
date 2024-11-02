using System;
using System.ComponentModel.DataAnnotations;
using RecipeBuilder.Models;

namespace RecipeBuilder.ViewModels
{
    public class CookbooksAddVM
    {
        public string CookbookTitle { get; set; }
        public string CookbookDescription { get; set; }
        public List<Recipe>? Recipes { get; set; }

        public string? msg { get; set; }
    }
}
