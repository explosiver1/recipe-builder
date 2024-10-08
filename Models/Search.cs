namespace RecipeBuilder.Models
{
    public class Search
    {
        public string SearchQuery { get; set; }
        public List<Ingredient> Results { get; set; }

        public void ExecuteSearch(string query)
        {
            SearchQuery = query;
        }

        public List<Ingredient> FilterResults()
        {
            return Results;
        }

        public List<Ingredient> SortResults()
        {
            return Results;
        }
    }
}