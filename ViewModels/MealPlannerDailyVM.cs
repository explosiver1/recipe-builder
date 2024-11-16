using System.ComponentModel.DataAnnotations;
using RecipeBuilder.Models;

namespace RecipeBuilder.ViewModels
{
    public class MealPlannerDailyVM
    {
        public MPDay mealPlansForDay { get; set; } = new MPDay();

        // The following lists are for selection menus
        public List<string> UserRecipesNames { get; set; } = new List<string>();
        public List<string> UserMealsNames { get; set; } = new List<string>();
        // The following are to track data for a recipe that needs removed from a meal
        public int mealNum { get; set; }
        public string recipeToRemove { get; set; } = "";
        public DateOnly date { get; set; }
        public string? msg { get; set; }
    }
}