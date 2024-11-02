using System.ComponentModel.DataAnnotations;
using RecipeBuilder.Models;

namespace RecipeBuilder.ViewModels
{
    public class MealPlannerMonthVM
    {
        public List<MealPlanner> MealPlanners { get; set; } = new List<MealPlanner>(); // List of meal plans for the month

        // Optional constructor
        public MealPlannerMonthVM()
        {
            MealPlanners = new List<MealPlanner>();
        }
    }
}