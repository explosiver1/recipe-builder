using System;
using System.ComponentModel.DataAnnotations;
using RecipeBuilder.Models;

namespace RecipeBuilder.ViewModels;

public class MealPlannerPlanVM()
{
    public MealPlanner mealPlanner { get; set; } = new MealPlanner();
}