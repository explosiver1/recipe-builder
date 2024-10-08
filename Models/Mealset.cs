namespace RecipeBuilder.Models
{
    public class MealSet
    {
        public int MealSetId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Recipe> Recipes { get; set; }

        public void AddRecipe(Recipe recipe)
        {
            Recipes.Add(recipe);
        }

        public void RemoveRecipe(Recipe recipe)
        {
            Recipes.Remove(recipe);
        }

        public void UpdateMeal(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}