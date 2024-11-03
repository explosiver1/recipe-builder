using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RecipeBuilder.Models;
using RecipeBuilder.ViewModels;
using Newtonsoft.Json;

namespace RecipeBuilder.Controllers
{
    public class RecipeController : Controller
    {
        // Index method: Displays full list of user's recipes
        [HttpGet]
        public IActionResult Index(string? id, string msg = "")
        {
            if (id == "-9999" && msg != "")
            {
                RecipeIndexVM erroredViewModel = new RecipeIndexVM();
                erroredViewModel.msg = msg;
                return View(erroredViewModel);
            }

            List<Recipe> recipeList;
            RecipeIndexVM viewModel = new RecipeIndexVM();
            AuthToken at;
            bool test;

            try
            {
                at = JsonConvert.DeserializeObject<AuthToken>(HttpContext.Session.GetString("authToken")!)!;
                if (!at.Validate())
                {
                    throw new Exception("Authentication Expired. Please login again.");
                }
                recipeList = new List<Recipe>();
                if (id == null)
                {
                    List<string> recipeNameList = DBQueryModel.GetRecipeNodeNames(at.username).Result;
                    foreach (string recipeName in recipeNameList)
                    {
                        Recipe r = new Recipe();
                        r.Name = recipeName;
                        recipeList.Add(r);
                    }
                }
                else
                {
                    List<string> recipeNameList = DBQueryModel.GetRecipeNodeNamesByIngredient(at.username, id).Result;
                    foreach (string recipeName in recipeNameList)
                    {
                        Recipe r = new Recipe();
                        r.Name = recipeName;
                        recipeList.Add(r);
                    }
                }

                viewModel.recipes = recipeList;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error fetching recipes. Exception " + e);
                return View("-9999", "Error getting recipes. Exception " + e);
            }

            return View(viewModel);
        }

        // Add method (GET): Display the form to create a new recipe
        [HttpGet]
        public IActionResult Add(string msg = "")
        {
            var viewModel = new RecipeAddVM
            {
                recipe = new Recipe() // Initialize an empty recipe
            };

            if (msg != "")
            {
                viewModel.msg = msg;
            }
            return View(viewModel);
        }

        // Add method (POST): Handles form submission to create a new recipe
        [HttpPost]
        public IActionResult Add(RecipeAddVM recipeVM)
        {
            Console.WriteLine("POST Add action hit"); // Logging to console
                                                      //if (!ModelState.IsValid)
                                                      //{
                                                      //    Console.WriteLine("Model validation failed:");
                                                      //    foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                                                      //    {
                                                      //        Console.WriteLine(error.ErrorMessage);
                                                      //    }
                                                      //    return View(recipeVM); // Return the form with validation errors
                                                      //}

            AuthToken at;
            bool test;

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
            /*
            var servingSizeParts = recipeVM.ServingSizeInput.Split(',');
            if (servingSizeParts.Length == 2)
            {
                string unit = servingSizeParts[0].Trim();
                if (int.TryParse(servingSizeParts[1].Trim(), out int amount))
                {
                    recipeVM.recipe.servingSize = string.Empty; //new Dictionary<string, int> { { unit, amount } };
                }
            } */

            recipeVM.recipe.servingSize = recipeVM.ServingSizeInput;

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
            //SeedData.GetRecipeList().Add(recipeVM.recipe);
            try
            {
                Console.WriteLine("Entering try block on Account/Add");
                at = JsonConvert.DeserializeObject<AuthToken>(HttpContext.Session.GetString("authToken")!)!;
                if (!at.Validate())
                {
                    throw new Exception("Authentication Expired. Please login again.");
                }
                Console.WriteLine("About to enter CreateRecipeNode");
                test = DBQueryModel.CreateRecipeNode(at.username,
                    recipeVM.recipe.Name,
                    recipeVM.recipe.Description,
                    recipeVM.recipe.Rating.ToString(),
                    recipeVM.recipe.Difficulty.ToString(),
                    recipeVM.recipe.numServings.ToString(),
                    recipeVM.recipe.servingSize).Result;
            }
            catch (Exception e)
            {
                Console.WriteLine("Recipe could not be added. Error: " + e);
                return Add("Error, recipe could not be added. Exception: " + e);
            }

            if (!test)
            {
                return Add("Error, recipe could not be added. No Exception Thrown");
            }

            // Redirect to Look
            //Debug.WriteLine($"Redirecting to Look with recipeName: {recipeVM.recipe.Name}");
            //Redirecting to Index instead because the other redirect kept throwing an error loop
            return RedirectToAction("Index");
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
                ServingSizeInput = string.Empty, //recipe.servingSize != null ? string.Join(", ", recipe.servingSize.Select(kv => $"{kv.Key}, {kv.Value}")) : "",
                EquipmentInput = string.Join(", ", recipe.Equipment),
                InstructionsInput = string.Join("\n", recipe.Instructions)
            };

            return View(viewModel);
        }

        // Look method (GET): Displays a specific recipe in a specific cookbook
        [HttpGet]
        public IActionResult Look(string recipeName, string msg = "")
        {
            if (msg != "")
            {
                RecipeLookVM rlvm = new RecipeLookVM { recipe = new Recipe() };
                rlvm.msg = msg;
                return View(rlvm);
            }
            //Recipe? recipeModel = SeedData.GetRecipe(recipeName);
            //Debug.WriteLine(recipeModel != null ? $"Recipe found: {recipeModel.Name}" : "Recipe not found");
            AuthToken at;
            Recipe recipeModel;

            try
            {
                at = JsonConvert.DeserializeObject<AuthToken>(HttpContext.Session.GetString("authToken")!)!;
                if (!at.Validate())
                {
                    throw new Exception("Authentication Expired. Please login again.");
                }
                recipeModel = DBQueryModel.GetRecipe(recipeName, at).Result;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error, recipe could not be retrieved. Exception: " + e);
                return Look("ERROR", "Error, recipe could not be retrieved. Exception: " + e);
            }

            /*
            if (recipeModel == null)
            {
                Debug.WriteLine("Recipe not found");
                return NotFound();
            } */

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
