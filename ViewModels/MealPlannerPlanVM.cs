using System.ComponentModel.DataAnnotations;
using RecipeBuilder.Models;

namespace RecipeBuilder.ViewModels
{
    public class MealPlannerPlanVm
    {
        [Required]
        public MealPlanner mealPlanner { get; set; } = new MealPlanner();
    }
}