using System;
using System.ComponentModel.DataAnnotations;
using RecipeBuilder.Models;

namespace RecipeBuilder.ViewModels
{
    public class CookbooksAddVM
    {
        public Cookbook newcookbook { get; set; }
        public List<string> UserRecipesNames { get; set; } = new List<string>();
        public string? msg { get; set; }
    }
}
