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
            //If user isn't logged in, don't allow access to this page - redirect to main site page
            AuthToken at;
            try
            {
                at = JsonConvert.DeserializeObject<AuthToken>(HttpContext.Session.GetString("authToken")!)!;
                if (!at.Validate())
                {
                    throw new Exception("Authentication Expired. Please login again.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return RedirectToAction("Index", "Home");
            }

            if (id == "-9999" && msg != "")
            {
                Console.WriteLine("Received error message on Recipe/Index: " + msg);
                RecipeIndexVM erroredViewModel = new RecipeIndexVM();
                erroredViewModel.msg = msg;
                return View(erroredViewModel);
            }

            List<Recipe> recipeList;
            RecipeIndexVM viewModel = new RecipeIndexVM();
            string rPrint = "Recipes Received: ";

            try
            {
                recipeList = new List<Recipe>();
                if (id == null)
                {
                    List<string> recipeNameList = DBQueryModel.GetRecipeNodeNames(at.username).Result;
                    foreach (string recipeName in recipeNameList)
                    {
                        rPrint += recipeName + ", ";
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
                        rPrint += recipeName + ", ";
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
                return Index("-9999", "Error getting recipes. Exception " + e);
            }

            Console.WriteLine(rPrint);
            return View(viewModel);
        }

        // Add method (GET): Display the form to create a new recipe
        [HttpGet]
        public IActionResult Add(string msg = "")
        {
            AuthToken at;
            try
            {
                at = JsonConvert.DeserializeObject<AuthToken>(HttpContext.Session.GetString("authToken")!)!;
                if (!at.Validate())
                {
                    throw new Exception("Authentication Expired. Please login again.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return RedirectToAction("Index", "Home");
            }
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

        [HttpPost]
        public IActionResult RemoveRecipe(string recipeToRemove)
        {
            AuthToken at;
            try
            {
                at = JsonConvert.DeserializeObject<AuthToken>(HttpContext.Session.GetString("authToken")!)!;
                if (!at.Validate())
                {
                    throw new Exception("Authentication Expired. Please login again.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return RedirectToAction("Index", "Home");
            }

            bool test;
            try
            {
                test = DBQueryModel.DeleteRecipe(recipeToRemove, at.username).Result;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error, recipe could not be delete. Exception: " + e);

                return RedirectToAction("Index", new { id = "-9999", msg = "Error, recipe could not be deleted. Exception: " + e });
            }

            if (test)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", new { id = "-9999", msg = "Error, recipe could not be deleted. No Exception Thrown." });
            }
        }

        // TODO: Handle nullreference exception when not everything is enter into recipe form
        // Add method (POST): Handles form submission to create a new recipe
        [HttpPost]
        public IActionResult Add(RecipeAddVM recipeVM)
        {
            AuthToken at;
            try
            {
                at = JsonConvert.DeserializeObject<AuthToken>(HttpContext.Session.GetString("authToken")!)!;
                if (!at.Validate())
                {
                    throw new Exception("Authentication Expired. Please login again.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return RedirectToAction("Index", "Home");
            }
            Console.WriteLine("POST Add action hit"); // Logging to console
            //Printing out the whole thing just to be sure.
            Console.WriteLine("RecipeAddVM Properties: \n" +
                "Cookbook: " + recipeVM.cookbookName + "\n" +
                "Recipe: \n" +
                "   Name: " + recipeVM.recipe.Name + "\n" +
                "   Prep Time: " + recipeVM.recipe.PrepTime + "\n" +
                "   Cook Time: " + recipeVM.recipe.CookTime + "\n" +
                "   Rating: " + recipeVM.recipe.Rating + "\n" +
                "   Difficulty: " + recipeVM.recipe.Difficulty + "\n" +
                "   Description: " + recipeVM.recipe.Description + "\n" +
                "   Servings: " + recipeVM.recipe.numServings + "\n" +
                "Equipment String: " + recipeVM.EquipmentInput + "\n" +
                "Tags String: " + recipeVM.TagsInput + "\n" +
                //"Ingredients String: " + recipeVM.IngredientsInput + "\n" +
                "ServingSize String: " + recipeVM.ServingSizeInput + "\n" +
                "Instructions String: " + recipeVM.InstructionsInput + "\n" +
                "\n");

            foreach (IngredientDetail ing in recipeVM.IngredientsInput)
            {
                Console.WriteLine("Ingredient Name: " + ing.Name + "\n" +
                "Quantity: " + ing.Quantity + "\n" +
                "Unit: " + ing.Unit + "\n" +
                "Qualifier: " + ing.Qualifier + "\n");
                if (ing.Qualifier == null)
                {
                    ing.Qualifier = string.Empty;
                }
                if (ing.Unit == null)
                {
                    ing.Unit = string.Empty;
                }
            }
            //if (!ModelState.IsValid)
            //{
            //    Console.WriteLine("Model validation failed:");
            //    foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            //    {
            //        Console.WriteLine(error.ErrorMessage);
            //    }
            //    return View(recipeVM); // Return the form with validation errors
            //}
            bool test;
            try
            {
                // Parse Tags
                if (recipeVM.recipe.Tags.Any())
                {
                    recipeVM.recipe.Tags = recipeVM.TagsInput.Split(',')
                                            .Select(tag => tag.Trim())
                                            .Where(tag => !string.IsNullOrEmpty(tag))
                                            .ToList();
                }


                // Parse Ingredients
                recipeVM.recipe.Ingredients = recipeVM.IngredientsInput; /* .Split('\n')
                                            .Select(line => new IngredientDetail { Name = line.Trim() })
                                            .Where(ingredient => !string.IsNullOrEmpty(ingredient.Name))
                                            .ToList(); */
                foreach (IngredientDetail ing in recipeVM.recipe.Ingredients)
                {
                    ing.Ingredient.Name = ing.Name;
                }

                //Commented this out because the dictionary can't be parsed reliably without knowing the keys the user inputs.
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
                if (recipeVM.recipe.Equipment.Any())
                {
                    recipeVM.recipe.Equipment = recipeVM.EquipmentInput.Split(',')
                                            .Select(tool => tool.Trim())
                                            .Where(tool => !string.IsNullOrEmpty(tool))
                                            .ToList();
                }


                // Parse Instructions
                recipeVM.recipe.Instructions = recipeVM.InstructionsInput; /*.Split('\n')
                                            .Select(instruction => instruction.Trim())
                                            .Where(instruction => !string.IsNullOrEmpty(instruction))
                                            .ToList();*/

                // ** Add the recipe to SeedData here **
                //SeedData.GetRecipeList().Add(recipeVM.recipe);

                Console.WriteLine("Entering try block on Account/Add");
                Console.WriteLine("About to enter CreateRecipeNode");
                /*
                test = DBQueryModel.CreateRecipeNode(at.username,
                    recipeVM.recipe.Name,
                    recipeVM.recipe.Description,
                    recipeVM.recipe.Rating.ToString(),
                    recipeVM.recipe.Difficulty.ToString(),
                    recipeVM.recipe.numServings.ToString(),
                    recipeVM.recipe.servingSize,
                    recipeVM.recipe.CookTime.ToString(),
                    recipeVM.recipe.PrepTime.ToString()).Result; */
                test = CtrlModel.SetRecipe(at.username, recipeVM.recipe);

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
        public IActionResult Edit(string recipeName)
        {
            AuthToken at;
            try
            {
                at = JsonConvert.DeserializeObject<AuthToken>(HttpContext.Session.GetString("authToken")!)!;
                if (!at.Validate())
                {
                    throw new Exception("Authentication Expired. Please login again.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return RedirectToAction("Index", "Home");
            }
            // Get the recipe data (replace with actual fetch logic)
            Recipe recipe = DBQueryModel.GetRecipe(at.username, recipeName).Result;
            Console.WriteLine("REcipe Name: " + recipe.Name);
            if (recipe == null)
            {
                return NotFound();
            }

            var viewModel = new RecipeEditVM
            {
                recipe = recipe,
                TagsInput = string.Join(", ", recipe.Tags),
                IngredientsInput = recipe.Ingredients,
                ServingSizeInput = string.Empty, //recipe.servingSize != null ? string.Join(", ", recipe.servingSize.Select(kv => $"{kv.Key}, {kv.Value}")) : "",
                EquipmentInput = string.Join(", ", recipe.Equipment),
                InstructionsInput = recipe.Instructions
            };

            return View(viewModel);
        }

        // Edit method (Post): Save Form Data
        [HttpPost]
        public IActionResult EditRecipe(RecipeEditVM RecipeVM)
        {
            AuthToken at;
            try
            {
                at = JsonConvert.DeserializeObject<AuthToken>(HttpContext.Session.GetString("authToken")!)!;
                if (!at.Validate())
                {
                    throw new Exception("Authentication Expired. Please login again.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return RedirectToAction("Index", "Home");
            }
            string name = RecipeVM.recipe.Name;
            return RedirectToAction("Look", "Recipe", new { recipeName = name });
        }
        // Look method (GET): Displays a specific recipe in a specific cookbook
        [HttpGet]
        public IActionResult Look(string recipeName, string msg = "")
        {
            AuthToken at;
            try
            {
                at = JsonConvert.DeserializeObject<AuthToken>(HttpContext.Session.GetString("authToken")!)!;
                if (!at.Validate())
                {
                    throw new Exception("Authentication Expired. Please login again.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return RedirectToAction("Index", "Home");
            }
            if (msg != "")
            {
                RecipeLookVM rlvm = new RecipeLookVM { recipe = new Recipe() };
                rlvm.msg = msg;
                rlvm.recipe.Name = "Error";
                return View(rlvm);
            }
            //Recipe? recipeModel = SeedData.GetRecipe(recipeName);
            //Debug.WriteLine(recipeModel != null ? $"Recipe found: {recipeModel.Name}" : "Recipe not found");
            Recipe recipeModel;

            try
            {
                recipeModel = CtrlModel.GetRecipe(at.username, recipeName)!; //DBQueryModel.GetRecipe(at.username, recipeName).Result;

                Console.WriteLine(@$"Recipe Found. Name: {recipeModel.Name}");
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
            Console.WriteLine("Checking Recipe before sending back ViewModel...");
            viewModel.recipe.PrintAllStats();
            return View(viewModel);
        }

        // Select method (GET): Displays a list of recipes for selection (if needed)
        [HttpGet]
        public IActionResult Select(string selectedRecipe)
        {
            AuthToken at;
            try
            {
                at = JsonConvert.DeserializeObject<AuthToken>(HttpContext.Session.GetString("authToken")!)!;
                if (!at.Validate())
                {
                    throw new Exception("Authentication Expired. Please login again.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return RedirectToAction("Index", "Home");
            }
            return View(); // Can expand this later
        }
    }
}
