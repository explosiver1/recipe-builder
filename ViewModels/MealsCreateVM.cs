using System;
using System.ComponentModel.DataAnnotations;
using RecipeBuilder.Models;

namespace RecipeBuilder.ViewModels
{
    public class MealsCreateVM
    {
        public MealSet meal { get; set; }
        public List<string> UserRecipesNames { get; set; } = new List<string>();
        public string? msg { get; set; }
    }
}