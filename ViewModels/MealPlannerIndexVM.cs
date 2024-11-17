using System.ComponentModel.DataAnnotations;
using RecipeBuilder.Models;

namespace RecipeBuilder.ViewModels
{
    public class MealPlannerIndexVM
    {
        public MPDay ScheduledMealsToday { get; set; } = new MPDay();
        public MPWeek ScheduledMealsThisWeek { get; set; } = new MPWeek();

        // Data for selecting a meal to view
        public string mealTitle { get; set; } = "";
        public MPMeal mealData { get; set; } = new MPMeal();
    }
}