using System;
using System.ComponentModel.DataAnnotations;
using RecipeBuilder.Models;

namespace RecipeBuilder.ViewModels;

public class CookbooksIndexVM()
{
    public required List<Cookbook> cookbooks {get; set;}
}