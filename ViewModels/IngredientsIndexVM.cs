using System;
using System.ComponentModel.DataAnnotations;
using RecipeBuilder.Models;

namespace RecipeBuilder.ViewModels;

public class IngredientsIndexVM()
{
    //public required List<Ingredient> ingredients {get; set;}
    // public required List<string> ingredientNames {get; set;}
    // public IngredientsIndexVM(List<string> ingredients)
    // {
    //     ingredientNames = ingredients.Sort();
    // }
    //public required Ingredient ingredient {get;set;}
    //public required List<List<string>> ingredientNames {get;set;}
    public required Dictionary<string, List<string>> ingredientNamesDict {get;set;}
}