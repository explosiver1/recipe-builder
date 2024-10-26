using System;
using System.Collections.Generic;
using RecipeBuilder.Models;

namespace RecipeBuilder.ViewModels
{
    public class PantryIndexVM
    {
        public List<Ingredient> items { get; set; }

        public PantryIndexVM()
        {
            items = new List<Ingredient>();
        }
        
    }
}