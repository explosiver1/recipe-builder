using System;
using System.ComponentModel.DataAnnotations;
using RecipeBuilder.Models;

namespace RecipeBuilder.ViewModels;

public class MealsIndexVM()
{
    public List<MealSet> meals { get; set; } = new List<MealSet>();
    public string? msg { get; set; }
}