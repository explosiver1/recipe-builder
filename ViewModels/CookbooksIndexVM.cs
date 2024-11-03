using System;
using System.ComponentModel.DataAnnotations;
using RecipeBuilder.Models;


namespace RecipeBuilder.ViewModels
{
    public class CookbooksIndexVM
    {
        public List<Cookbook> cookbooks { get; set; } = new List<Cookbook>();
        public string? msg { get; set; }

        // Nullable property for UserId
        public string? UserId { get; set; }
    }
}
