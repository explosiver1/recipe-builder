using System.ComponentModel.DataAnnotations;
using RecipeBuilder.Models;

namespace RecipeBuilder.ViewModels
{
    public class MealPlannerDailyVm
    {
        public MPDay selectedDay {get; set;} = new MPDay();
        
        public MealPlanner mealPlanner { get; set; } = new MealPlanner();
    }
}