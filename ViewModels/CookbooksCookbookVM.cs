using System;
using System.ComponentModel.DataAnnotations;
using RecipeBuilder.Models;

namespace RecipeBuilder.ViewModels;

public class CookbooksCookbookVM
{
    public Cookbook Cookbook { get; set; }

    public CookbooksCookbookVM()
    {
        Cookbook = new Cookbook(); // Initialize to avoid null reference
    }
}