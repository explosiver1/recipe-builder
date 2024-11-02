using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecipeBuilder.Models
{
    public class Recipe
    {
        [Key]
        public int RecipeId { get; set; }

        [Required(ErrorMessage = "Recipe name is required.")]
        [StringLength(100, ErrorMessage = "Recipe name cannot exceed 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; } = string.Empty;

        // Serving size stored as a dictionary with key of unit & value of int amount
        public Dictionary<string, int> servingSize { get; set; } = new Dictionary<string, int>();
        
        public int numServings { get; set; }

        public List<string> Equipment { get; set; } = new List<string>();

        // Make Ingredients and Instructions optional if they don’t need to be required
        public List<IngredientDetail> Ingredients { get; set; } = new List<IngredientDetail>();

        public List<string> Instructions { get; set; } = new List<string>();

        public List<string> Tags { get; set; } = new List<string>();

        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; } = 1;

        [Range(1, 5, ErrorMessage = "Difficulty must be between 1 and 5.")]
        public int Difficulty { get; set; } = 1;

        [Range(0, 600, ErrorMessage = "Prep time must be between 0 and 600 minutes.")]
        public int PrepTime { get; set; }

        [Range(0, 600, ErrorMessage = "Cook time must be between 0 and 600 minutes.")]
        public int CookTime { get; set; }

        // Constructor to initialize lists and dictionaries if needed
        public Recipe()
        {
            Name = string.Empty;
            Description = string.Empty;
            Equipment = new List<string>();
            Ingredients = new List<IngredientDetail>();
            Instructions = new List<string>();
            Tags = new List<string>();
            servingSize = new Dictionary<string, int>();
        }

        public bool CreateRecipe(AuthToken at, string recipe, string title, string description)
        {

            if (!at.Validate())
            {
                Console.WriteLine("Authentication failed. Invalid or expired token.");
                return false;
            }
            else
            {
                // Passing the dictionary might be easier but all variable makes it more readable...
                //.Result added to change Task<bool> to bool; unsure if this is right fix or if this method as a whole also needs to be async
                bool recipeCreated = false; //DBQueryModel.CreateRecipeNode(at.username, recipe, title, description).Result;
                return recipeCreated;
            }
        }

        // Methods
        public void AddIngredient(IngredientDetail ingredientDetail)
        {
            if (ingredientDetail != null)
            {
                Ingredients.Add(ingredientDetail);
            }
        }

        public void RemoveIngredient(IngredientDetail ingredientDetail)
        {
            if (Ingredients.Contains(ingredientDetail))
            {
                Ingredients.Remove(ingredientDetail);
            }
        }

        public void EditRecipe(string name, int cookTime, int difficulty)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                Name = name;
            }

            if (cookTime > 0)
            {
                CookTime = cookTime;
            }

            if (difficulty >= 1 && difficulty <= 5)
            {
                Difficulty = difficulty;
            }
        }

        // Neo4j Placeholder Method: Connect to Neo4j database
        public void ConnectToNeo4j()
        {
            bool useNeo4j = false; // Toggle to true when Neo4j is ready

            if (useNeo4j)
            {
                // Placeholder for Neo4j integration
                Console.WriteLine($"Creating Neo4j entry for recipe '{Name}' with ID {RecipeId}...");
                // TODO: Add Neo4j integration logic (e.g., CREATE/MERGE statements)
            }
            else
            {
                Console.WriteLine($"Simulating Neo4j entry for recipe '{Name}' (ID {RecipeId}).");
            }
        }

        // Neo4j Placeholder Method: Update recipe in Neo4j
        public void UpdateInNeo4j()
        {
            bool useNeo4j = false; // Toggle to true when Neo4j is ready

            if (useNeo4j)
            {
                // Placeholder for Neo4j update logic
                Console.WriteLine($"Updating Neo4j entry for recipe '{Name}' (ID {RecipeId})...");
                // TODO: Add Neo4j integration logic (e.g., SET statements)
            }
            else
            {
                Console.WriteLine($"Simulating Neo4j update for recipe '{Name}' (ID {RecipeId}).");
            }
        }

        // Neo4j Placeholder Method: Delete recipe from Neo4j
        public void DeleteFromNeo4j()
        {
            bool useNeo4j = false; // Toggle to true when Neo4j is ready

            if (useNeo4j)
            {
                // Placeholder for Neo4j delete logic
                Console.WriteLine($"Deleting Neo4j entry for recipe '{Name}' (ID {RecipeId})...");
                // TODO: Add Neo4j delete logic (e.g., DELETE statement)
            }
            else
            {
                Console.WriteLine($"Simulating Neo4j deletion for recipe '{Name}' (ID {RecipeId}).");
            }
        }
    }
}
