using System;
using System.ComponentModel.DataAnnotations;
using RecipeBuilder.Models;

namespace RecipeBuilder.ViewModels
{
    public class RecipeIndexVM
    {
        public List<Recipe> recipes { get; set; } = new List<Recipe>(); // Initialize a new Cookbook object
        public string? msg { get; set; }
    }
}
