using System.ComponentModel.DataAnnotations;
using RecipeBuilder.Models;

namespace RecipeBuilder.ViewModels
{
    public class MealPlannerDailyVm
    {
        public MealPlanner mealPlanner { get; set; } = new MealPlanner();
    }
}