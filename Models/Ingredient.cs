namespace RecipeBuilder.Models
{
    public class Ingredient
    {
        // Attributes
        public int IngredientId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; } // Unit of measurement

        // Constructor
        public Ingredient(int ingredientId, string name, string description, string unit)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Ingredient name cannot be empty.");
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Ingredient description cannot be empty.");
            if (string.IsNullOrWhiteSpace(unit))
                throw new ArgumentException("Ingredient unit cannot be empty.");

            IngredientId = ingredientId;
            Name = name;
            Description = description;
            Unit = unit;
        }//end Ingredient

        // Methods
        // Update description
        public void UpdateDescription(string newDescription)
        {
            if (string.IsNullOrWhiteSpace(newDescription))
            {
                Console.WriteLine("Description cannot be empty.");
                return;
            }//end if

            Description = newDescription;
            Console.WriteLine($"{Name} description updated to: {newDescription}");
        }//end UpdateDescription

        // Update unit
        public void UpdateUnit(string newUnit)
        {
            if (string.IsNullOrWhiteSpace(newUnit))
            {
                Console.WriteLine("Unit cannot be empty.");
                return;
            }//end if

            Unit = newUnit;
            Console.WriteLine($"{Name} unit updated to: {newUnit}");
        }//end UpdateUnit

        //Placeholder for creating a connection to this ingredient in Neo4j
        public void ConnectToUser(User user)
        {
            bool useDatabase = false;  // Switch this to true when Neo4j is ready

            if (useDatabase)
            {
                // Placeholder for creating a relationship in Neo4j between user and ingredient
                Console.WriteLine($"Creating a connection between {user.Username} and {Name} in Neo4j...");
                // TODO: Add Neo4j code here to create a relationship (e.g., MERGE or CREATE)
            }
            else
            {
                // In-memory logic for now
                Console.WriteLine($"Simulating a connection between {user.Username} and {Name}.");
            }
        }

        // Display ingredient information
        public void DisplayIngredientInfo()
        {
            if (string.IsNullOrWhiteSpace(Name))
                Console.WriteLine("Ingredient name is missing.");
            else
                Console.WriteLine($"Ingredient: {Name}");

            if (string.IsNullOrWhiteSpace(Description))
                Console.WriteLine("Description is missing.");
            else
                Console.WriteLine($"Description: {Description}");

            if (string.IsNullOrWhiteSpace(Unit))
                Console.WriteLine("Unit is missing.");
            else
                Console.WriteLine($"Unit: {Unit}");
        }//end DisplayIngredientInfo
    }//end Ingredient

}//end namespace RecipeBuilder.Models