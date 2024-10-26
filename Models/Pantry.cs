namespace RecipeBuilder.Models
{
    public class Pantry
    {
        public List<IngredientDetail> Items { get; set; }

        // Blank constructor initializing to default values
        public Pantry()
        {
            Items = new List<IngredientDetail>();
        } // end Blank Constructor

        // Update ingredient quantity with validation
        public void UpdateItemQuantity(IngredientDetail ingredientDetail, double newQuantity)
        {
            if (ingredientDetail == null || !Items.Contains(ingredientDetail))
            {
                Console.WriteLine("Ingredient not found in the pantry.");
                return;
            } // end if

            ingredientDetail.SetQuantity(newQuantity);
        } // end UpdateItemQuantity

        // Add an ingredient detail with validation
        public void AddItem(IngredientDetail ingredientDetail)
        {
            if (ingredientDetail == null)
            {
                Console.WriteLine("Ingredient cannot be empty.");
                return;
            } // end if

            Items.Add(ingredientDetail);
            Console.WriteLine($"Ingredient '{ingredientDetail.Ingredient.Name}' added to pantry.");
        } // end AddItem

        // Remove an ingredient detail with validation
        public void RemoveItem(IngredientDetail ingredientDetail)
        {
            if (ingredientDetail == null || !Items.Contains(ingredientDetail))
            {
                Console.WriteLine("Ingredient not found in the pantry.");
                return;
            } // end if

            Items.Remove(ingredientDetail);
            Console.WriteLine($"Ingredient '{ingredientDetail.Ingredient.Name}' removed from pantry.");
        } // end RemoveItem

        // Get all pantry items
        public List<IngredientDetail> GetPantryItems()
        {
            return Items;
        } // end GetPantryItems
    } // end Pantry
} // end namespace