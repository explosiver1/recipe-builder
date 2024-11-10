using System;
using System.Collections.Generic;
using RecipeBuilder.Models;

namespace RecipeBuilder.ViewModels
{
    public class PantryIndexVM
    {
        // public List<IngredientDetail> items { get; set; }
        public Dictionary<string, List<IngredientDetail>> ABCPantry { get; set; }
        public IngredientDetail newIngredient { get; set; }

        public bool success { get; set; }

        public PantryIndexVM()
        {
            // items = new List<IngredientDetail>();
            ABCPantry = new Dictionary<string, List<IngredientDetail>>();
            newIngredient = new IngredientDetail();
        }

    }
}
