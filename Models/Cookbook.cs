namespace RecipeBuilder.Models
{
    public class Cookbook
    {
        public string Title { get; set; }
        public List<Recipe> Recipes { get; set; }

        // Blank constructor initializing to default values
        public Cookbook()
        {
            Title = string.Empty;
            Recipes = new List<Recipe>();
        }// end Blank Constructor

        // Add a recipe with validation
        public void AddRecipe(Recipe recipe)
        {
            if (recipe == null)
            {
                Console.WriteLine("Recipe cannot be empty.");
                return;
            }//end if

            Recipes.Add(recipe);
            Console.WriteLine($"Recipe '{recipe.Name}' added to cookbook '{Title}'.");
        }// end AddRecipe

        // Remove a recipe with validation
        public void RemoveRecipe(Recipe recipe)
        {
            if (recipe == null || !Recipes.Contains(recipe))
            {
                Console.WriteLine("Recipe not found in the cookbook.");
                return;
            }//end if

            Recipes.Remove(recipe);
            Console.WriteLine($"Recipe '{recipe.Name}' removed from cookbook '{Title}'.");
        }// end RemoveRecipe

        // Get recipe by name with null check
        public Recipe GetRecipe(string recipeName)
        {
            var recipe = Recipes.FirstOrDefault(r => r.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase));
            if (recipe == null)
                Console.WriteLine($"No recipe found with the name '{recipeName}'.");

            return recipe;
        }// end GetRecipe

        // Rename cookbook with validation
        public void RenameCookbook(string newTitle)
        {
            if (string.IsNullOrWhiteSpace(newTitle))
            {
                Console.WriteLine("New title cannot be empty.");
                return;
            }//end if

            Title = newTitle;
            Console.WriteLine($"Cookbook renamed to: {newTitle}");
        }// end RenameCookbook

        // Placeholder for Neo4j connection
        public void ConnectToUser(User user)
        {
            bool useDatabase = false;  // Switch to true when Neo4j is ready

            if (useDatabase)
            {
                // Placeholder for Neo4j relationship creation between user and cookbook
                Console.WriteLine($"Creating a connection between user '{user.Username}' and cookbook '{Title}' in Neo4j...");
                // TODO: Add Neo4j code here (e.g., MERGE or CREATE relationship)
            }//end if
            else
            {
                // In-memory logic for now
                Console.WriteLine($"Simulating a connection between user '{user.Username}' and cookbook '{Title}'.");
            }//end else
        }// end ConnectToUser
    }// end Cookbook
}// end namespace