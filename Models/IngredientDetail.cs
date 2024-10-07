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
            Quantity = newQuantity;
            Console.WriteLine($"{Ingredient.Name} quantity updated to: {newQuantity} {Unit}");
        }//end SetQuantity

        // Update unit
        public void SetUnit(string newUnit)
        {
            Unit = newUnit;
            Console.WriteLine($"{Ingredient.Name} unit updated to: {newUnit}");
        }//end SetUnit

        // Display ingredient info
        public void DisplayIngredientDetail()
        {
            Console.WriteLine($"{Quantity} {Unit} of {Ingredient.Name}");
        }//end DisplayIngredientDetail

    }//end IngredientDetail
}//end namespace RecipeBuilder.Models