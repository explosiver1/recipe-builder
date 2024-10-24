using System;
using System.ComponentModel.DataAnnotations;
using RecipeBuilder.Models;

namespace RecipeBuilder.ViewModels;

public class MealsIndexVM()
{
    public MealSet meal { get; set; } = new MealSet();
}