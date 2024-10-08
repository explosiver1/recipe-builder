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
            IngredientId = ingredientId;
            Name = name;
            Description = description;
            Unit = unit;
        }//end Ingredient

        // Methods
        // Update description
        public void UpdateDescription(string newDescription)
        {
            Description = newDescription;
            Console.WriteLine($"{Name} description updated to: {newDescription}");
        }//end UpdateDescription

        // Update unit
        public void UpdateUnit(string newUnit)
        {
            Unit = newUnit;
            Console.WriteLine($"{Name} unit updated to: {newUnit}");
        }//end UpdateUnit

        // Display ingredient information
        public void DisplayIngredientInfo()
        {
            Console.WriteLine($"Ingredient: {Name}");
            Console.WriteLine($"Description: {Description}");
            Console.WriteLine($"Unit: {Unit}");
        }//end DisplayIngredientInfo
    }//end Ingredient

}//end namespace RecipeBuilder.Models