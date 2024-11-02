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
        public IActionResult Add(string cookbookName)
        {
            var viewModel = new RecipeAddVM
            {
                cookbookName = cookbookName,
                recipe = new Recipe() // Initialize an empty recipe
            };

            return View(viewModel);
        }

        // Add method (POST): Handles form submission to create a new recipe
        [HttpPost]
        public IActionResult Add(RecipeAddVM recipeVM)
        {
            if (!ModelState.IsValid)
            {
                return View(recipeVM); // Return the form with validation errors
            }

            // Uncomment the following line if you want to save to seed data
            // if (RecipeSeedData.AddRecipe(recipeVM.CookbookName, recipeVM.Recipe))

            // For now, simulate saving to a database
            if (true) // Replace with actual condition when using database
            {
                // Redirect to the "Look" action to display the newly added recipe
                return RedirectToAction("Look", new { cookbookName = recipeVM.cookbookName, recipeName = recipeVM.recipe.Name });
            }
            else
            {
                return View(recipeVM); // Return the form with errors if save fails
            }
        }

        // Edit method (GET): Display the form to edit an existing recipe
        [HttpGet]
        public IActionResult Edit(string cookbookName, string recipeName)
        {
            // Uncomment the following line if you want to use seed data
            // var recipe = RecipeSeedData.GetRecipe(cookbookName, recipeName);

            // For now, create a placeholder or fetch from a database
            var recipe = new Recipe { Name = recipeName, Description = "Sample Description" };

            if (recipe == null)
            {
                return NotFound();
            }

            var viewModel = new RecipeEditVM
            {
                recipe = recipe
            };

            return View(viewModel);
        }

        // Look method (GET): Displays a specific recipe in a specific cookbook
        [HttpGet]
        public IActionResult Look(string recipeName)
        {
            Recipe? recipeModel = SeedData.GetRecipe(recipeName);
    
            if (recipeModel == null)
            {
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