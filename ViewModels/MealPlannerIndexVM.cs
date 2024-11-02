using System.ComponentModel.DataAnnotations;
using RecipeBuilder.Models;

namespace RecipeBuilder.ViewModels
{
    public class MealPlannerIndexVM
    {
        public MealPlanner mealPlanner { get; set; } = new MealPlanner();
    }
}