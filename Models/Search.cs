using System;
using System.Linq;
using System.Collections.Generic;

namespace RecipeBuilder.Models
{
    public class Search
    {
        // Attributes
        public string SearchQuery { get; set; } = string.Empty;
        public List<Ingredient> Results { get; set; } = new List<Ingredient>();

        // Blank constructor initializing default values
        public Search()
        {
            SearchQuery = string.Empty;
            Results = new List<Ingredient>();
        }// end Blank Constructor

        // Executes a search for ingredients that match the query
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
            }
            else
            {
                Console.WriteLine($"{Results.Count} ingredient(s) found matching '{query}'");
            }
        }// end ExecuteSearch

        // Filters the results by a specific unit of measurement
        public List<Ingredient> FilterResults(string unit)
        {
            if (string.IsNullOrWhiteSpace(unit))
            {
                Console.WriteLine("Unit cannot be empty.");
                return new List<Ingredient>();
            }//end if

            var filteredResults = Results.Where(ingredient => ingredient.Unit == unit).ToList();
            Console.WriteLine($"{filteredResults.Count} ingredient(s) found matching '{unit}'");
            return filteredResults;
        }// end FilterResults

        // Sorts the results alphabetically by ingredient name
        public List<Ingredient> SortResults()
        {
            Results = Results.OrderBy(ingredient => ingredient.Name).ToList();
            Console.WriteLine("Results sorted alphabetically by ingredient name");
            return Results;
        }// end SortResults
    }// end Search
}// end namespace