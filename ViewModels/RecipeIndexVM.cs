using System;
using System.ComponentModel.DataAnnotations;
using RecipeBuilder.Models;

namespace RecipeBuilder.ViewModels;

public class RecipeIndexVM()
{
    public required Cookbook cookbook {get; set;}
}