using System;
using System.ComponentModel.DataAnnotations;
using RecipeBuilder.Models;

namespace RecipeBuilder.ViewModels
{
    public class MealPlannerLookVM
    {
        public string mealTitle { get; set; } = "";
        public MPMeal mealData { get; set; } = new MPMeal();
    }
}