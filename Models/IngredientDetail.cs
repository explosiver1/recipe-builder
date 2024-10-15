namespace RecipeBuilder.Models
{
    public class IngredientDetail
    {
        // Attributes
        public Ingredient Ingredient { get; set; }
        public float Quantity { get; set; }
        public string Unit { get; set; }

        // Constructor
        public IngredientDetail(Ingredient ingredient, float quantity, string unit)
        {
            if (ingredient == null)
                throw new ArgumentException("Ingredient cannot be empty.");
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.");
            if (string.IsNullOrWhiteSpace(unit))
                throw new ArgumentException("Unit cannot be empty.");

            Ingredient = ingredient;
            Quantity = quantity;
            Unit = unit;
        }//end IngredientDetail

        // Methods
        // Get quantity
        public float GetQuantity()
        {
            return Quantity;
        }

        // Set quantity
        public void SetQuantity(float newQuantity)
        {
            if (newQuantity <= 0)
            {
                Console.WriteLine("Quantity must be greater than zero.");
                return;
            }//end if

            Quantity = newQuantity;
            Console.WriteLine($"{Ingredient.Name} quantity updated to: {newQuantity} {Unit}");
        }//end SetQuantity

        // Update unit
        public void SetUnit(string newUnit)
        {
            if (string.IsNullOrWhiteSpace(newUnit))
            {
                Console.WriteLine("Unit cannot be empty.");
                return;
            }//end if

            Unit = newUnit;
            Console.WriteLine($"{Ingredient.Name} unit updated to: {newUnit}");
        }//end SetUnit

        public void ConnectToRecipe(Recipe recipe)
        {
            bool useDatabase = false;  // Switch to true when Neo4j is ready

            if (useDatabase)
            {
                // Placeholder for creating a relationship in Neo4j between ingredient and recipe
                Console.WriteLine($"Creating a connection between {Ingredient.Name} and {recipe.Name} in Neo4j...");
                // TODO: Add Neo4j code here to create a relationship (e.g., MERGE or CREATE)
            }//end if
            else
            {
                // In-memory logic for now
                Console.WriteLine($"Simulating a connection between {Ingredient.Name} and {recipe.Name}.");
            }//end else
        }//end ConnectToRecipe

        // Display ingredient info
        public void DisplayIngredientDetail()
        {
            Console.WriteLine($"{Quantity} {Unit} of {Ingredient.Name}");
        }//end DisplayIngredientDetail

    }//end IngredientDetail
}//end namespace RecipeBuilder.Models