namespace RecipeBuilder.Models
{
    public class Recipe
    {
        public int RecipeId { get; set; }
        public string Name { get; set; }
        public List<IngredientDetail> Ingredients { get; set; }
        public List<string> Instructions { get; set; }
        public List<string> Tags { get; set; }
        public int Rating { get; set; }
        public int Difficulty { get; set; }
        public int CookTime { get; set; }

        // Methods
        public void AddIngredient(IngredientDetail ingredientDetail)
        {
            Ingredients.Add(ingredientDetail);
        }

        public void RemoveIngredient(IngredientDetail ingredientDetail)
        {
            Ingredients.Remove(ingredientDetail);
        }

        public void EditRecipe(string name, int cookTime, int difficulty)
        {
            Name = name;
            CookTime = cookTime;
            Difficulty = difficulty;
        }
    }
}