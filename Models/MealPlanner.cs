namespace RecipeBuilder.Models
{
    public class MealPlanner
    {
        public int MealPlannerId { get; set; }
        public List<MealSet> ScheduledMeals { get; set; }

        public void ScheduleMeal(MealSet mealSet)
        {
            ScheduledMeals.Add(mealSet);
        }

        public void RemoveMeal(MealSet mealSet)
        {
            ScheduledMeals.Remove(mealSet);
        }

        public void GetMealSchedule()
        {
        }
    }
}