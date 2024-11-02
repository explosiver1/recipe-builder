using System.ComponentModel.DataAnnotations;
using RecipeBuilder.Models;

namespace RecipeBuilder.ViewModels
{
    public class MealPlannerWeekVM
    {
        public MPWeek selectedWeek {get; set;} = new MPWeek();
        public MealPlanner mealPlanner { get; set; } = new MealPlanner();
    }
}