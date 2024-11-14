using System;
using System.ComponentModel.DataAnnotations;
using RecipeBuilder.Models;

namespace RecipeBuilder.ViewModels
{
    public class ShoppingListIndexVM
    {
        //public List<IngredientDetail> items { get; set; }
        public Dictionary<string, List<IngredientDetail>> ABCShoppingList{ get; set; }
        public IngredientDetail newIngredient { get; set; }

        // Constructor initializes items to an empty list
        public ShoppingListIndexVM()
        {
            //items = new List<IngredientDetail>();
            ABCShoppingList = new Dictionary<string, List<IngredientDetail>>();
            newIngredient = new IngredientDetail();
        }
    }
}
