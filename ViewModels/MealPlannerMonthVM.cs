using System.ComponentModel.DataAnnotations;
using RecipeBuilder.Models;

namespace RecipeBuilder.ViewModels
{
    public class MealPlannerMonthVM
    {
        public MealPlanner mealPlanner { get; set; } = new MealPlanner();
    }
}