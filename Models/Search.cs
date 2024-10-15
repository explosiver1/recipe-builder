using System.Linq;

namespace RecipeBuilder.Models
{
    public class Search
    {
        //Attributes
        public string SearchQuery { get; set; } = string.Empty;
        public List<Ingredient> Results { get; set; } = new List<Ingredient> ();

        //Contructor
        public Search(List<Ingredient> ingredientList)
        {
            Results = ingredientList ?? new List<Ingredient>();
        } //end Constructor

        //Searches the results list for ingredients that match the search query
        public void ExecuteSearch(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                Console.WriteLine("Search query cannot be empty.");
                return;
            }//end if
            
            SearchQuery = query;
            Results = Results.Where(ingredient => 
                ingredient.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                ingredient.Description.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();

            if (Results.Count == 0)
            {
                Console.WriteLine("No ingredients found.");
            }//end if

            else
            {
                Console.WriteLine($"{Results.Count} ingredient(s) found matching '{query}'");
            }//end else

        }//end ExecuteSearch

        //Filters the results list for ingredients that match the unit
        public List<Ingredient> FilterResults(string unit)
        {
            if (string.IsNullOrWhiteSpace(unit))
            {
                Console.WriteLine("Unit cannot be empty.");
                return new List<Ingredient>();
            }//end if
            
            //filtering by unit of measurement
            var filteredResults = Results.Where(ingredient => ingredient.Unit == unit).ToList();
            Console.WriteLine($"{filteredResults.Count} ingredient(s) found matching '{unit}'");
            return filteredResults;
        }//end FilterResults

        //
        public List<Ingredient> SortResults()
        {
            //Sort the results alphabetically by ingredient name
            Results = Results.OrderBy(ingredient => ingredient.Name).ToList();
            Console.WriteLine("Results sorted alphabetically by ingredient name");
            return Results;
        }//end SortResults

    }//end Search
}//end namespace