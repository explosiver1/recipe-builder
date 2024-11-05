namespace RecipeBuilder.Models
{
    public class Ingredient
    {
        // Attributes
        public string Name { get; set; }
        public string Description { get; set; }

        // Blank constructor initializing to default values
        public Ingredient()
        {
            Name = string.Empty;
            Description = string.Empty;
        }// end Blank Constructor

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
        }// end UpdateDescription

        // Update unit
        public void UpdateUnit(string newUnit)
        {
            if (string.IsNullOrWhiteSpace(newUnit))
            {
                Console.WriteLine("Unit cannot be empty.");
                return;
            }//end if

            Console.WriteLine($"{Name} unit updated to: {newUnit}");
        }// end UpdateUnit

        // Placeholder for Neo4j connection
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
            }// end else
        }// end ConnectToUser

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


        }// end DisplayIngredientInfo
    }// end Ingredient
}// end namespace RecipeBuilder.Models
