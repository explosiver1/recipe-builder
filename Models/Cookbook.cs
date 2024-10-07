namespace RecipeBuilder.Models
{
    public class Cookbook
    {
        public int CookbookId { get; set; }
        public string Title { get; set; }
        public List<Recipe> Recipes { get; set; }

        public void AddRecipe(Recipe recipe)
        {
            Recipes.Add(recipe);
        }

        public void RemoveRecipe(Recipe recipe)
        {
            Recipes.Remove(recipe);
        }

        public Recipe GetRecipe(int recipeId)
        {
            return Recipes.FirstOrDefault(r => r.RecipeId == recipeId);
        }

        public void RenameCookbook(string newTitle)
        {
            Title = newTitle;
        }
    }
}