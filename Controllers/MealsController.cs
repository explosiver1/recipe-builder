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
        catch (Exception e)
        {
            return RedirectToAction("Index", "Home");
        }

        var mealVM = new MealsIndexVM
        {
            // Use the placeholder method to simulate combining data from Neo4j
            // IDK What was happening here. I've left a chunk of the old statement behind the comment.
            meals = CtrlModel.GetAllMeals(at.username) //.Concat(CtrlModel.GetMealsFromNeo4j()).ToList()
        };

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
        catch (Exception e)
        {
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
        catch (Exception e)
        {
            return RedirectToAction("Index", "Home");
        }

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
    public IActionResult Edit(string mealName)
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
        catch (Exception e)
        {
            return RedirectToAction("Index", "Home");
        }

        MealSet meal;

        try
        {
            meal = DBQueryModel.GetMeal(at.username, mealName).Result;
        }
        catch (Exception e)
        {
            meal = new MealSet();
            meal.Name = "Error, cookbook " + mealName + "could not be retrieved. Exception: " + e;
        }

        var viewModel = new MealsEditVM
        {
            mealData = new MealSet { Name = meal.Name,Description = meal.Description, RecipeNames = meal.RecipeNames },
            UserRecipesNames = CtrlModel.GetRecipeNameList(at.username)
        };

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Edit(MealsEditVM viewModel)
    {
        MealSet meal = new MealSet { Name = viewModel.mealData.Name };

        if (meal == null)
        {
            return NotFound();
        }

        // Update meal
        //CtrlModel.SaveMealEdits(viewModel.mealData);

        return RedirectToAction("Index");
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
        catch (Exception e)
        {
            return RedirectToAction("Index", "Home");
        }

        MealsLookVM mealVM = new MealsLookVM();
        mealVM.meal = CtrlModel.getMeal(id, at.username);
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
