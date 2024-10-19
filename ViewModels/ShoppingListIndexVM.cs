using System;
using System.ComponentModel.DataAnnotations;
using RecipeBuilder.Models;

namespace RecipeBuilder.ViewModels;

public class ShoppingListIndexVM()
{
    //public required string userName {get;set;}
    public required ShoppingList shoppingList;
}