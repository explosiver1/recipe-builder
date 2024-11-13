namespace RecipeBuilder.Models
{
    public class MealSet
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Recipe> Recipes { get; set; }
        public List<string> RecipeNames { get; set; }
        public DateOnly Date { get; set; }

        // Blank constructor initializing to default values
        public MealSet()
        {
            Name = string.Empty;
            Description = string.Empty;
            Recipes = new List<Recipe>();
        } // end Blank Constructor

        // Add a recipe with validation
        public void AddRecipe(Recipe recipe)
        {
            if (recipe == null)
            {
                Console.WriteLine("Recipe cannot be empty.");
                return;
            } // end if

            Recipes.Add(recipe);
            Console.WriteLine($"Recipe '{recipe.Name}' added to meal set '{Name}'.");
        } // end AddRecipe

        // Remove a recipe with validation
        public void RemoveRecipe(Recipe recipe)
        {
            if (recipe == null || !Recipes.Contains(recipe))
            {
                Console.WriteLine("Recipe not found in the meal set.");
                return;
            } // end if

            Recipes.Remove(recipe);
            Console.WriteLine($"Recipe '{recipe.Name}' removed from meal set '{Name}'.");
        } // end RemoveRecipe

        // Update meal set details with validation
        public void UpdateMeal(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(description))
            {
                Console.WriteLine("Name and description cannot be empty.");
                return;
            } // end if

            Name = name;
            Description = description;
            Console.WriteLine($"Meal set updated: '{name}' with description '{description}'.");
        } // end UpdateMeal

        // Placeholder for Neo4j connection
        public void ConnectToUser(User user)
        {
            bool useDatabase = false;  // Switch to true when Neo4j is ready

            if (useDatabase)
            {
                // Placeholder for Neo4j relationship creation between user and meal set
                Console.WriteLine($"Creating a connection between user '{user.Username}' and meal set '{Name}' in Neo4j...");
                // TODO: Add Neo4j code here (e.g., MERGE or CREATE relationship)
            } // end if
            else
            {
                // In-memory logic for now
                Console.WriteLine($"Simulating a connection between user '{user.Username}' and meal set '{Name}'.");
            } // end else
        } // end ConnectToUser
        public void PrintAllStats()
        {
            Console.WriteLine("Meal Properties: \n" +
            "Name: " + Name + "\n" +
            "Description: " + Description + "\n" +
            "RecipeNames: \n");
            foreach (string r in RecipeNames)
            {
                Console.WriteLine("     Recipe: " + r);
            }
        }
    } // end MealSet
} // end namespace