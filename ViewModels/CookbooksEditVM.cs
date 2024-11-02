using System;
using System.ComponentModel.DataAnnotations;
using RecipeBuilder.Models;

namespace RecipeBuilder.ViewModels
{
    public class CookbooksEditVM
    {
        public string? cookbookName { get; set; }
        public Recipe? recipe { get; set; }

    }
}
