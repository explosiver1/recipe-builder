using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RecipeBuilder.Models;
using RecipeBuilder.ViewModels;

namespace RecipeBuilder.Controllers
{
    public class RecipeController : Controller
    {
        // Index method: Displays full list of user's recipes 
        [HttpGet]
        public IActionResult Index(string? id)
        {
            List<Recipe> recipeList;
            RecipeIndexVM viewModel = new RecipeIndexVM();

            if (id == null)
            {
                recipeList = CtrlModel.GetRecipeList();    
            }
            else
            {
                recipeList = CtrlModel.GetRecipesForIngredient(id);
            }
            
            viewModel.recipes = recipeList;
            return View(viewModel);
        }

        // Add method (GET): Display the form to create a new recipe
        [HttpGet]
        public IActionResult Add()
        {
            var viewModel = new RecipeAddVM
            {
                recipe = new Recipe() // Initialize an empty recipe
            };

            return View(viewModel);
        }

        // Add method (POST): Handles form submission to create a new recipe
        [HttpPost]
        public IActionResult Add(RecipeAddVM recipeVM)
        {
            Console.WriteLine("POST Add action hit"); // Logging to console
            if (!ModelState.IsValid)
            {
                Console.WriteLine("Model validation failed:");
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                return View(recipeVM); // Return the form with validation errors
            }

            // Parse Tags
            recipeVM.recipe.Tags = recipeVM.TagsInput.Split(',')
                                        .Select(tag => tag.Trim())
                                        .Where(tag => !string.IsNullOrEmpty(tag))
                                        .ToList();

            // Parse Ingredients
            recipeVM.recipe.Ingredients = recipeVM.IngredientsInput.Split('\n')
                                        .Select(line => new IngredientDetail { Name = line.Trim() })
                                        .Where(ingredient => !string.IsNullOrEmpty(ingredient.Name))
                                        .ToList();

            // Parse Serving Size (assuming a format like "cup, 2")
            var servingSizeParts = recipeVM.ServingSizeInput.Split(',');
            if (servingSizeParts.Length == 2)
            {
                string unit = servingSizeParts[0].Trim();
                if (int.TryParse(servingSizeParts[1].Trim(), out int amount))
                {
                    recipeVM.recipe.servingSize = new Dictionary<string, int> { { unit, amount } };
                }
            }

            // Parse Equipment
            recipeVM.recipe.Equipment = recipeVM.EquipmentInput.Split(',')
                                        .Select(tool => tool.Trim())
                                        .Where(tool => !string.IsNullOrEmpty(tool))
                                        .ToList();

            // Parse Instructions
            recipeVM.recipe.Instructions = recipeVM.InstructionsInput.Split('\n')
                                        .Select(instruction => instruction.Trim())
                                        .Where(instruction => !string.IsNullOrEmpty(instruction))
                                        .ToList();

            // ** Add the recipe to SeedData here **
            SeedData.GetRecipeList().Add(recipeVM.recipe);

            // Redirect to Look
            Debug.WriteLine($"Redirecting to Look with recipeName: {recipeVM.recipe.Name}");
            return RedirectToAction("Look", new { recipeName = recipeVM.recipe.Name });
        }

        // Edit method (GET): Display the form to edit an existing recipe
        [HttpGet]
        public IActionResult Edit(string cookbookName, string recipeName)
        {
            // Get the recipe data (replace with actual fetch logic)
            var recipe = SeedData.GetRecipe(recipeName);

            if (recipe == null)
            {
                return NotFound();
            }

            var viewModel = new RecipeEditVM
            {
                recipe = recipe,
                TagsInput = string.Join(", ", recipe.Tags),
                IngredientsInput = string.Join("\n", recipe.Ingredients.Select(i => $"{i.Quantity} {i.Unit} {i.Ingredient.Name}")),
                ServingSizeInput = recipe.servingSize != null ? string.Join(", ", recipe.servingSize.Select(kv => $"{kv.Key}, {kv.Value}")) : "",
                EquipmentInput = string.Join(", ", recipe.Equipment),
                InstructionsInput = string.Join("\n", recipe.Instructions)
            };

            return View(viewModel);
        }

        // Look method (GET): Displays a specific recipe in a specific cookbook
        [HttpGet]
        public IActionResult Look(string recipeName)
        {
            Recipe? recipeModel = SeedData.GetRecipe(recipeName);
            Debug.WriteLine(recipeModel != null ? $"Recipe found: {recipeModel.Name}" : "Recipe not found");
    
            if (recipeModel == null)
            {
                Debug.WriteLine("Recipe not found");
                return NotFound();
            }

            var viewModel = new RecipeLookVM
            {
                recipe = recipeModel
            };

            return View(viewModel);
        }

        // Select method (GET): Displays a list of recipes for selection (if needed)
        [HttpGet]
        public IActionResult Select(string selectedRecipe)
        {
            return View(); // Can expand this later
        }
    }
}