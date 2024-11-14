using System.ComponentModel.DataAnnotations;
using RecipeBuilder.Models;

namespace RecipeBuilder.ViewModels
{
    public class MealPlannerDailyVM
    {        
        public MealPlanner mealPlanner { get; set; } = new MealPlanner();
        public List<string> UserRecipesNames { get; set; } = new List<string>();
        public List<string> UserMealsNames { get; set; } = new List<string>();
        public string? msg { get; set; }
    }
}