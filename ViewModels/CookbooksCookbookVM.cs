using System;
using System.ComponentModel.DataAnnotations;
using RecipeBuilder.Models;

namespace RecipeBuilder.ViewModels;

public class CookbooksCookbookVM
{
    public Cookbook cookbook { get; set; }
    public string? newCookbookDescription { get; set; }
    public string? recipeToAdd { get; set; }
    public string? recipeToRemove { get; set; }

    public string msg { get; set; }

    public CookbooksCookbookVM()
    {
        cookbook = new Cookbook(); // Initialize to avoid null reference
    }
}
