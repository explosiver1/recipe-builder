using System;
using System.ComponentModel.DataAnnotations;
using RecipeBuilder.Models;

namespace RecipeBuilder.ViewModels;

public class CookbooksAddVM()
{
    public required Cookbook cookbook {get; set;}
}