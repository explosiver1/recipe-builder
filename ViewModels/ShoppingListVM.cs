using System;
using System.ComponentModel.DataAnnotations;
using RecipeBuilder.Models;

namespace RecipeBuilder.ViewModels
{
    public class ShoppingListVM
    {
        public ShoppingList shoppingList { get; set; }

        // Constructor initializes shoppingList to a new instance
        public ShoppingListVM()
        {
            shoppingList = new ShoppingList();
        }
    }
}