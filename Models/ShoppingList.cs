namespace RecipeBuilder.Models
{
    public class ShoppingList
    {
        // Attributes
        public List<IngredientDetail>? Items { get; set; }

        // Blank constructor that initializes Items to null
        public ShoppingList()
        {
            Items = null;
        }// end Blank Constructor

        // Add an item with validation
        public void AddItem(IngredientDetail ingredient)
        {
            if (ingredient == null)
            {
                Console.WriteLine("Ingredient cannot be empty.");
                return;
            }//end if

            if (Items == null)
                Items = new List<IngredientDetail>(); // Initialize if null

            Items.Add(ingredient);
            Console.WriteLine($"{ingredient.Name} added to the shopping list.");
        } // end AddItem

        // Remove an item with validation
        public void RemoveItem(IngredientDetail ingredient)
        {
            if (ingredient == null || !Items.Contains(ingredient))
            {
                Console.WriteLine("Ingredient not found in the shopping list.");
                return;
            }//end if

            Items.Remove(ingredient);
            Console.WriteLine($"{ingredient.Name} removed from the shopping list.");
        } // end RemoveItem

        // Check off an item with validation
        public void CheckItemOff(IngredientDetail ingredient)
        {
            if (ingredient == null || !Items.Contains(ingredient))
            {
                Console.WriteLine("Ingredient not found in the shopping list.");
                return;
            }

            Console.WriteLine($"{ingredient.Name} checked off the shopping list.");
            Items.Remove(ingredient); // Optional: Remove after checking off
        } // end CheckItemOff

        // Placeholder for Neo4j connection
        public void ConnectToUser(User user)
        {
            bool useDatabase = false; // Switch to true when Neo4j is ready

            if (useDatabase)
            {
                Console.WriteLine($"Creating a connection between user '{user.Username}' and shopping list in Neo4j...");
                // TODO: Add Neo4j code here (e.g., MERGE or CREATE relationship)
            }//end if
            else
            {
                Console.WriteLine($"Simulating a connection between user '{user.Username}' and shopping list.");
            }//end else
        } // end ConnectToUser
    } // end ShoppingList
} // end namespace RecipeBuilder.Models
