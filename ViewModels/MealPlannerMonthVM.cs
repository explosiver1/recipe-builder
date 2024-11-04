using System.ComponentModel.DataAnnotations;
using RecipeBuilder.Models;

namespace RecipeBuilder.ViewModels
{
    public class MealPlannerMonthVM
    {
        public MPMonth monthPlans { get; set; } = new MPMonth();
        public string monthName {get; set;}
        // public List<MealPlanner> MealPlanners { get; set; } = new List<MealPlanner>(); // List of meal plans for the month
        // public DayOfWeek firstDayOfMonth;
        // public DayOfWeek lastDayOfMonth;

        // Optional constructor
        public MealPlannerMonthVM()
        {
            // MealPlanners = new List<MealPlanner>();
            monthPlans = new MPMonth();
            monthName = "";
        }
    }
}