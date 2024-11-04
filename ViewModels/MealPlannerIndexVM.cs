using System.ComponentModel.DataAnnotations;
using RecipeBuilder.Models;

namespace RecipeBuilder.ViewModels
{
    public class MealPlannerIndexVM
    {
        public MealPlanner ScheduledMealsToday { get; set; } = new MealPlanner();
        public MPWeek ScheduledMealsThisWeek { get; set; } = new MPWeek();
    }
}