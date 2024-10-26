using System;
using System.ComponentModel.DataAnnotations;
using RecipeBuilder.Models;

namespace RecipeBuilder.ViewModels
{
    public class ShoppingListIndexVM
    {
        public List<Ingredient> items { get; set; }

        // Constructor initializes items to an empty list
        public ShoppingListIndexVM()
        {
            items = new List<Ingredient>();
        }
    }
}