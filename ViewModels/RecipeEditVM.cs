using System;
using System.ComponentModel.DataAnnotations;
using RecipeBuilder.Models;

namespace RecipeBuilder.ViewModels;

public class RecipeEditVM()
{
    public required Recipe recipe {get;set;}
}