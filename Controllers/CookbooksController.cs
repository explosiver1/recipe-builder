using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RecipeBuilder.Models;
using RecipeBuilder.ViewModels;
using Newtonsoft.Json;

namespace RecipeBuilder.Controllers;

public class CookbooksController : Controller
{
    // Index method: Displays a list of cookbooks
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
        CookbooksIndexVM viewModel = new CookbooksIndexVM();
        //cookbooks = CtrlModel.GetCookbookList()// Empty list for now, until data source is implemented
        try
        {
            viewModel.cookbooks = DBQueryModel.GetCookbooks(at.username).Result;
        }
        catch (Exception e)
        {
            Console.WriteLine("Error Getting Cookbooks: " + e);
        }

        return View(viewModel);
    }

    // Cookbook method: Displays a specific cookbook by ID
    [HttpGet]
    public IActionResult Cookbook(string name, string msg = "")
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
        Cookbook cookbookModel;
        try
        {
            cookbookModel = DBQueryModel.GetCookbook(name, at.username).Result;
            //if (cookbookModel.RecipeNames != null && cookbookModel.RecipeNames.Any())
            //{
            //    foreach (var recipeName in cookbookModel.RecipeNames)
            //    {
            //        Console.WriteLine(recipeName);
            //    }
            //}
            
        }
        catch (Exception e)
        {
            Console.WriteLine("Error Getting Cookbooks");
            cookbookModel = new Cookbook();
            cookbookModel.Title = "Error, cookbook" + name + "could not be retrieved. Exception: " + e;
        }


        CookbooksCookbookVM viewModel = new CookbooksCookbookVM { cookbook = cookbookModel };
        //if (viewModel.cookbook.RecipeNames.Any())
        //{
        //    foreach (var recipeName in viewModel.cookbook.RecipeNames)
        //    {
        //        Console.WriteLine(recipeName);
        //    }
        //}
        
        return View(viewModel);
    }

    [HttpGet]
    public IActionResult Add(string msg = "")
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

        CookbooksAddVM cavm = new CookbooksAddVM { msg = msg, newcookbook = new Models.Cookbook(), UserRecipesNames = CtrlModel.GetRecipeNameList(at.username) };
        return View(cavm);
    }

    // Add method (POST): Handles form submission to create a new cookbook
    [HttpPost]
    public IActionResult Add(CookbooksAddVM CookbookVM)
    {
        Console.WriteLine("Creating Cookbook: \n" +
            "Name: " + CookbookVM.newcookbook.Title);
        AuthToken at;
        bool test;
        try
        {
            at = JsonConvert.DeserializeObject<AuthToken>(HttpContext.Session.GetString("authToken")!)!;
            Console.WriteLine("User " + at.username + " Deserialized");
            test = CtrlModel.CreateCookbook(at.username, CookbookVM.newcookbook);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: Exception " + e);
            test = false;
        }

        if (!test)
        {
            Console.WriteLine("Cookbook Creation Failure. Sending Error MEssage to View.");
            return Add("Error, cookbook could not be created.");
        }
        // Uncomment the following line if you want to add to seed data
        // RecipeSeedData.cookbooks.Add(newCookbook);

        // For now, you can integrate with a database or just return a success view
        // The index will display the new cookbook in the list, so it's fine.
        Console.WriteLine("Cookbook creation success. Redirecting to Cookbooks Index");
        return RedirectToAction("Index"); // Redirect back to index
    }


    // Edit method (GET): Display the form to edit an existing cookbook
    [HttpGet]
    public IActionResult Edit(string cookbookName)
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

        Cookbook cb;

        try
        {
            cb = DBQueryModel.GetCookbook(cookbookName, at.username).Result;
        }
        catch (Exception e)
        {
            cb = new Cookbook();
            cb.Title = "Error, cookbook " + cookbookName + "could not be retrieved. Exception: " + e;
        }

        var viewModel = new CookbooksEditVM
        {
            cookbookName = cb.Title,
            recipeNew = new Recipe(),
            cookbookDescription = cb.Description,
            cookbookRecipes = cb.RecipeNames,
            UserRecipesNames = CtrlModel.GetRecipeNameList(at.username),
            recipesToAdd = new List<string>()
        };

        return View(viewModel);
    }

    // Edit method (POST): Handles form submission to update an existing cookbook
    [HttpPost]
    public IActionResult Edit(CookbooksEditVM viewModel)
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

        CtrlModel.EditCookbook(at.username, viewModel.cookbookName, viewModel.cookbookDescription);

        if (viewModel.recipesToAdd.Any()) 
        {
            foreach (string recipeName in viewModel.recipesToAdd)
            {
                CtrlModel.AddToCookbook(at.username, viewModel.cookbookName, recipeName);
            }
        }

        return RedirectToAction("Cookbook", "Cookbooks", viewModel.cookbookName);
    }


    [HttpPost]
    public IActionResult RemoveRecipe(string cookbookTitle, string recipeToRemove)
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
                test = false; //DBQueryModel.CookbookRemoveRecipe(at.username, ccvm.cookbook.Title, ccvm.recipeToRemove);
                if (!test)
                {
                    CookbooksCookbookVM ccvm = new CookbooksCookbookVM();
                    ccvm.msg = "Error: Recipe could not be removed.";
                    return RedirectToAction("Cookbook", ccvm);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
            }
        }
        return RedirectToAction("Cookbook");
    }

    [HttpPost]
    public IActionResult RemoveCookbook(string cookbookToRemove)
    {
        AuthToken at;
        bool test;
        if (cookbookToRemove != "")
        {
            try
            {
                Console.WriteLine("Cookbook To Remove: " + cookbookToRemove!);
                at = JsonConvert.DeserializeObject<AuthToken>(HttpContext.Session.GetString("authToken")!)!;
                test = DBQueryModel.DeleteCookbook(cookbookToRemove, at.username).Result;
                if (!test)
                {
                    CookbooksIndexVM civm = new CookbooksIndexVM();
                    civm.msg = "Error: Recipe could not be removed.";
                    return RedirectToAction("Index", civm);
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
