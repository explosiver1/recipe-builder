using System;
using System.ComponentModel.DataAnnotations;
using RecipeBuilder.Models;

namespace RecipeBuilder.ViewModels
{
    public class CookbooksEditVM
    {
        public string cookbookName { get; set; } = string.Empty;
        public Recipe recipe { get; set; } = new Recipe();

        public CookbooksEditVM()
        {
            // Initialize anything needed here
        }
    }
}