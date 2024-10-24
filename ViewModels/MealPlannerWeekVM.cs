using System;
using System.ComponentModel.DataAnnotations;
using RecipeBuilder.Models;

namespace RecipeBuilder.ViewModels;

public class MealPlannerWeekVM()
{
    public MealPlanner mealPlanner { get; set; } = new MealPlanner();
}