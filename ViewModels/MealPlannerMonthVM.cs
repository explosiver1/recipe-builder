using System.ComponentModel.DataAnnotations;
using RecipeBuilder.Models;

namespace RecipeBuilder.ViewModels
{
    public class MealPlannerMonthVM
    {
        public MPMonth monthPlans { get; set; } = new MPMonth();
        public string monthName {get; set;}

        // Optional constructor
        public MealPlannerMonthVM()
        {
            // MealPlanners = new List<MealPlanner>();
            monthPlans = new MPMonth();
            monthName = "";
        }
    }
}