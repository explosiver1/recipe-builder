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
    public IActionResult Create(MealsCreateVM MealsVM)
    {
        Console.WriteLine("Creating Meal: \n" +
            "Name: " + MealsVM.meal.Name);
        AuthToken at;
        bool test;
        try
        {
            at = JsonConvert.DeserializeObject<AuthToken>(HttpContext.Session.GetString("authToken")!)!;
            Console.WriteLine("User " + at.username + " Deserialized");
            test = CtrlModel.CreateMeal(at.username, MealsVM.meal);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: Exception " + e);
            test = false;
        }

        if (!test)
        {
            Console.WriteLine("Meal Creation Failure. Sending Error MEssage to View.");
            return Create("Error, meal could not be created.");
        }
        // Uncomment the following line if you want to add to seed data
        // RecipeSeedData.cookbooks.Add(newCookbook);

        // For now, you can integrate with a database or just return a success view
        // The index will display the new cookbook in the list, so it's fine.
        Console.WriteLine("Meal creation success. Redirecting to Meal Index");
        return RedirectToAction("Index"); // Redirect back to index
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
        else return View(new MealsCreateVM { msg = "Error, meal could not be created." };)
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
                Console.WriteLine("Cookbook Title:" + cookbookTitle);
                Console.WriteLine("Recipe to Remove: " + recipeToRemove);
                at = JsonConvert.DeserializeObject<AuthToken>(HttpContext.Session.GetString("authToken")!)!;
                test = CtrlModel.RemoveFromMeal(at.username, mealName, recipeToRemove); //DBQueryModel.CookbookRemoveRecipe(at.username, ccvm.cookbook.Title, ccvm.recipeToRemove);
                if (!test)
                {
                    return RedirectToAction("Index", new MealsIndexVM { msg = "Error, recipe could not be removed." };);
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
                    return RedirectToAction("Index", new MealsIndexVM { msg = "Error, meal could not be removed." };);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
            }
        }
        else
        {
            Console.WriteLine("Error: cookbookToRemove is blank.");
        }
        return RedirectToAction("Index");
    }
}
