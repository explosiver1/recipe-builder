using System;
using System.ComponentModel.DataAnnotations;
using RecipeBuilder.Models;

namespace RecipeBuilder.ViewModels
{
    public class ShoppingListEditVM
    {
        public Ingredient ingredient { get; set; }
    }
}