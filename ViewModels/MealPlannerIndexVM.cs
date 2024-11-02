using System.ComponentModel.DataAnnotations;
using RecipeBuilder.Models;

namespace RecipeBuilder.ViewModels
{
    public class MealPlannerIndexVM
    {
        public MealPlanner mealPlanner { get; set; } = new MealPlanner();
        public List<MealSet> ScheduledMealsToday { get; set; } = new List<MealSet>();
        public List<MPDay> ScheduledMealsThisWeek { get; set; } = new List<MPDay>();
    }
}