namespace RecipeBuilder.Models
{
    public class ShoppingList
    {
        public int ShoppingListId { get; set; }
        public List<Ingredient> Items { get; set; }

        public void AddItem(Ingredient ingredient)
        {
            Items.Add(ingredient);
        }

        public void RemoveItem(Ingredient ingredient)
        {
            Items.Remove(ingredient);
        }

        public void CheckItemOff(Ingredient ingredient)
        {
        }
    }
}