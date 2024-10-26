using System.ComponentModel.DataAnnotations;
using RecipeBuilder.Models;

namespace RecipeBuilder.ViewModels
{
    public class MealPlannerWeekVm
    {
        public MealPlanner mealPlanner { get; set; } = new MealPlanner();
    }
}