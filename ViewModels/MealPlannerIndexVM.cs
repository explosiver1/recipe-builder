using System.ComponentModel.DataAnnotations;
using RecipeBuilder.Models;

namespace RecipeBuilder.ViewModels
{
    public class MealPlannerIndexVM
    {
        public MPDay ScheduledMealsToday { get; set; } = new MPDay();
        public MPWeek ScheduledMealsThisWeek { get; set; } = new MPWeek();
    }
}