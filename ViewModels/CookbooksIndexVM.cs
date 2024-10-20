using System;
using System.ComponentModel.DataAnnotations;
using RecipeBuilder.Models;


namespace RecipeBuilder.ViewModels
{
    public class CookbooksIndexVM
    {
        public List<Cookbook> cookbooks { get; set; } = new List<Cookbook>();

        // Nullable property for UserId
        public string? UserId { get; set; }
    }
}