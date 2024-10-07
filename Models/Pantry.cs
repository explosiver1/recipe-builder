namespace RecipeBuilder.Models
{
    public class Pantry
    {
        public int PantryId { get; set; }
        public List<Ingredient> Items { get; set; }

        public void AddItem(Ingredient ingredient)
        {
            Items.Add(ingredient);
        }

        public void RemoveItem(Ingredient ingredient)
        {
            Items.Remove(ingredient);
        }

        public void UpdateItemQuantity(Ingredient ingredient, float newQuantity)
        {
            
        }

        public List<Ingredient> GetPantryItems()
        {
            return Items;
        }
    }
}