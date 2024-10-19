using System;
using System.ComponentModel.DataAnnotations;
using RecipeBuilder.Models;

namespace RecipeBuilder.ViewModels;

public class RecipeSelectVM()
{
    public required string cookbookName {get;set;}
    public required List<Recipe> recipes {get;set;}
    public required List<Recipe> selectedRecipes {get;set;}
}