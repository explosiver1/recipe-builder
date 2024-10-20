using System;
using System.ComponentModel.DataAnnotations;
using RecipeBuilder.Models;

namespace RecipeBuilder.ViewModels
{
    public class CookbooksEditVM
    {
        public string CookbookName { get; set; } = string.Empty;
        public Recipe Recipe { get; set; } = new Recipe();

        public CookbooksEditVM()
        {
            // Initialize anything needed here
        }
    }
}