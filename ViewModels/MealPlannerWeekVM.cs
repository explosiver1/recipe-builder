using System.ComponentModel.DataAnnotations;
using RecipeBuilder.Models;

namespace RecipeBuilder.ViewModels
{
public class MealPlannerWeekVM
{
    public MPWeek selectedWeek { get; set; }

    public MealPlannerWeekVM()
    {
        selectedWeek = new MPWeek();
    }
}
}