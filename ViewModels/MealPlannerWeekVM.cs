using System.ComponentModel.DataAnnotations;
using RecipeBuilder.Models;

namespace RecipeBuilder.ViewModels
{
public class MealPlannerWeekVM
{
    public MealPlanner mealPlanner { get; set; }
    public MPWeek selectedWeek { get; set; } = new MPWeek();

    public MealPlannerWeekVM()
    {
        mealPlanner = new MealPlanner();
    }
}
}