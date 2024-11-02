namespace RecipeBuilder.Models
{
    public class MealPlanner
    {
        public DateOnly Date { get; set; }
        public List<MealSet> ScheduledMeals { get; set; }

        // Blank constructor initializing to default values
        public MealPlanner()
        {
            ScheduledMeals = new List<MealSet>();
            Date = DateOnly.FromDateTime(DateTime.Now);
        } // end Blank Constructor

        // Schedule a meal with validation
        public void ScheduleMeal(MealSet mealSet)
        {
            if (mealSet == null)
            {
                Console.WriteLine("Meal set cannot be empty.");
                return;
            } // end if

            ScheduledMeals.Add(mealSet);
            Console.WriteLine($"Meal set '{mealSet.Name}' scheduled.");
        } // end ScheduleMeal

        // Remove a scheduled meal with validation
        public void RemoveMeal(string mealSetName)
        {
            var mealSet = ScheduledMeals.FirstOrDefault(m => m.Name == mealSetName);
            if (mealSet == null)
            {
                Console.WriteLine("Meal set not found in the schedule.");
                return;
            }

            ScheduledMeals.Remove(mealSet);
            Console.WriteLine($"Meal set '{mealSet.Name}' removed from the schedule.");
        } // end RemoveMeal

        // Display the meal schedule
        public void GetMealSchedule()
        {
            if (ScheduledMeals.Count == 0)
            {
                Console.WriteLine("No meals scheduled.");
                return;
            } // end if

            Console.WriteLine("Scheduled Meals:");
            foreach (var mealSet in ScheduledMeals)
            {
                Console.WriteLine($"- {mealSet.Name}: {mealSet.Description}");
            }
        } // end GetMealSchedule

        // Placeholder for Neo4j connection
        public void ConnectToUser(User user)
        {
            bool useDatabase = false;  // Switch to true when Neo4j is ready

            if (useDatabase)
            {
                // Placeholder for Neo4j relationship creation between user and meal planner
                Console.WriteLine($"Creating a connection between user '{user.Username}' and meal planner in Neo4j...");
                // TODO: Add Neo4j code here (e.g., MERGE or CREATE relationship)
            } // end if
            else
            {
                // In-memory logic for now
                Console.WriteLine($"Simulating a connection between user '{user.Username}' and meal planner.");
            } // end else
        } // end ConnectToUser
    } // end MealPlanner
} // end namespace