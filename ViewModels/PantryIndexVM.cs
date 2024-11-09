using System;
using System.Collections.Generic;
using RecipeBuilder.Models;

namespace RecipeBuilder.ViewModels
{
    public class PantryIndexVM
    {
        public List<IngredientDetail> items { get; set; }

        public bool success { get; set; }

        public PantryIndexVM()
        {
            items = new List<IngredientDetail>();
        }

    }
}
