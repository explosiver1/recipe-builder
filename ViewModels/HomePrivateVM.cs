using System;
using System.ComponentModel.DataAnnotations;
using RecipeBuilder.Models;

namespace RecipeBuilder.ViewModels;

public class HomePrivateVM()
{
    public List<Cookbook> cookbooks {get; set;} = new List<Cookbook>();
    public List<Recipe> recipes {get; set;} = new List<Recipe>();
}