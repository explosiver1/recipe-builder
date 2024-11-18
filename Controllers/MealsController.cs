using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RecipeBuilder.Models;
using RecipeBuilder.ViewModels;
using Newtonsoft.Json;

namespace RecipeBuilder.Controllers;

public class MealsController : Controller
{
    [HttpGet]
    public IActionResult Index()
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

        var mealVM = new MealsIndexVM
        {
            // Use the placeholder method to simulate combining data from Neo4j
            // IDK What was happening here. I've left a chunk of the old statement behind the comment.
            meals = new List<MealSet>() //CtrlModel.GetAllMeals(at.username) //.Concat(CtrlModel.GetMealsFromNeo4j()).ToList()
        };

        foreach (string m in CtrlModel.GetAllMealNames(at.username))
        {
            MealSet meal = new MealSet()
            {
                Name = m
            };
            mealVM.meals.Add(meal);
        }

        return View(mealVM);
    }

    //public IActionResult Index()
    //{
    //    MealsIndexVM mealVM = new MealsIndexVM();
    //    mealVM.meals = CtrlModel.getMeals();
    //    return View(mealVM);
    //}
    [HttpGet]
    public IActionResult Create(string msg = "")
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
        MealsCreateVM cavm = new MealsCreateVM { msg = msg, meal = new Models.MealSet(), UserRecipesNames = CtrlModel.GetRecipeNameList(at.username) };
        return View(cavm);
    }

    [HttpPost]
    public IActionResult Create(MealsCreateVM mealsVM)
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
        //Uses a predicate to match which list elements it should remove.
        //This is necessary for removing possible empty strings the new drop down lists may create.
        mealsVM.meal.RecipeNames.RemoveAll(t => t == "");

        if (CtrlModel.CreateMeal(at.username, mealsVM.meal))
        {
            return RedirectToAction("Index", "Meals");
        }
        else
        {
            return View(new MealsCreateVM { msg = "Error, meal could not be created." });
        }
    }

    [HttpGet]
    public IActionResult Edit(string mealName, string msg = "")
    {
        // Uncomment the following line if you want to use seed data
        // var cookbook = RecipeSeedData.cookbooks.FirstOrDefault(c => c.Title == cookbookName);
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

        MealSet meal;

        try
        {
            meal = CtrlModel.getMeal(mealName, at.username);
        }
        catch (Exception e)
        {
            meal = new MealSet();
            meal.Name = "Error, cookbook " + mealName + "could not be retrieved. Exception: " + e;
        }

        var viewModel = new MealsEditVM
        {
            mealData = meal,//new MealSet { Name = meal.Name,Description = meal.Description, RecipeNames = meal.RecipeNames },
            UserRecipesNames = CtrlModel.GetRecipeNameList(at.username)
        };

        if (viewModel.mealData.RecipeNames == null)
        {
            viewModel.mealData.RecipeNames = new List<string>();
        }
        foreach (Recipe r in viewModel.mealData.Recipes)
        {
            viewModel.mealData.RecipeNames.Add(r.Name);
        }

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Edit(MealsEditVM viewModel)
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
        //Changes to dropdown options allowed for empty strings being passed into list.
        //Now we have to check for them.
        viewModel.RecipesToAdd.RemoveAll(r => r == "" || r == null);
        viewModel.mealData.RecipeNames = viewModel.RecipesToAdd;
        // Update meal
        if (!CtrlModel.EditMeal(at.username, viewModel.mealData))
        {
            return Edit(viewModel.mealData.Name, "Error meal could not be edited.");
        }

        return RedirectToAction("Look", new { id = viewModel.mealData.Name });
    }

    public IActionResult Look(string id)
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

        MealsLookVM mealVM = new MealsLookVM();
        mealVM.meal = CtrlModel.getMeal(id, at.username);

        // Debugging code
        Console.WriteLine(mealVM.meal.Name);
        if (mealVM.meal.Recipes != null && mealVM.meal.Recipes.Any())
        {
            foreach (var recipe in mealVM.meal.Recipes)
            {
                Console.WriteLine(recipe.Name);
            }
        }
        if (mealVM.meal.RecipeNames != null && mealVM.meal.RecipeNames.Any())
        {
            foreach (var recipeName in mealVM.meal.RecipeNames)
            {
                Console.WriteLine(recipeName);
            }
        }

        // Load view with data
        return View(mealVM);
    }

    [HttpPost]
    public IActionResult RemoveRecipe(string mealName, string recipeToRemove)
    {
        AuthToken at;
        bool test;
        if (recipeToRemove != "")
        {
            try
            {
                Console.WriteLine("Meal Title:" + mealName);
                Console.WriteLine("Recipe to Remove: " + recipeToRemove);
                at = JsonConvert.DeserializeObject<AuthToken>(HttpContext.Session.GetString("authToken")!)!;
                test = CtrlModel.RemoveFromMeal(at.username, mealName, recipeToRemove); //DBQueryModel.CookbookRemoveRecipe(at.username, ccvm.cookbook.Title, ccvm.recipeToRemove);
                if (!test)
                {
                    return RedirectToAction("Index", new MealsIndexVM { msg = "Error, recipe could not be removed." });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
            }
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult RemoveMeal(string mealToRemove)
    {
        AuthToken at;
        bool test;
        if (mealToRemove != "")
        {
            try
            {
                at = JsonConvert.DeserializeObject<AuthToken>(HttpContext.Session.GetString("authToken")!)!;
                test = CtrlModel.RemoveMeal(at.username, mealToRemove);
                if (!test)
                {
                    Console.WriteLine("CtrlModel.RemoveMeal returned false.");
                    return RedirectToAction("Index", new MealsIndexVM { msg = "Error, meal could not be removed." });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
            }
        }
        else
        {
            Console.WriteLine("Error: mealToRemove is blank.");
        }
        return RedirectToAction("Index");
    }
}
