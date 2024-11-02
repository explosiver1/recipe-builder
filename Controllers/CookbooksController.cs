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
        // Uncomment the following line if you want to use seed data
        // CookbooksIndexVM viewModel = new CookbooksIndexVM { Cookbooks = RecipeSeedData.cookbooks };
        AuthToken at;
        // For now, return an empty view model or integrate with a database
        CookbooksIndexVM viewModel = new CookbooksIndexVM();
        //cookbooks = CtrlModel.GetCookbookList()// Empty list for now, until data source is implemented
        try
        {

            at = JsonConvert.DeserializeObject<AuthToken>(HttpContext.Session.GetString("authToken")!)!;
            viewModel.cookbooks = DBQueryModel.GetCookbooks(at).Result;
        }
        catch
        {
            Console.WriteLine("Error Getting Cookbooks");
        }

        return View(viewModel);
    }

    // Cookbook method: Displays a specific cookbook by ID
    [HttpGet]
    public IActionResult Cookbook(string name)
    {
        AuthToken at;
        Cookbook cookbookModel;
        try
        {

            at = JsonConvert.DeserializeObject<AuthToken>(HttpContext.Session.GetString("authToken")!)!;
            cookbookModel = DBQueryModel.GetCookbook(name, at).Result;
        }
        catch (Exception e)
        {
            Console.WriteLine("Error Getting Cookbooks");
            cookbookModel = new Cookbook();
            cookbookModel.Title = "Error, cookbook" + name + "could not be retrieved. Exception: " + e;
        }


        CookbooksCookbookVM viewModel = new CookbooksCookbookVM { cookbook = cookbookModel };
        return View(viewModel);
    }

    [HttpGet]
    public IActionResult Add(string msg = "")
    {
        if (msg != "")
        {
            CookbooksAddVM cavm = new CookbooksAddVM();
            cavm.msg = msg;
            return View(cavm);
        }
        return View();
    }

    // Add method (POST): Handles form submission to create a new cookbook
    [HttpPost]
    public IActionResult Add(CookbooksAddVM newCookbook)
    {
        Console.WriteLine("Creating Cookbook: \n" +
            "Name: " + newCookbook.CookbookTitle);
        //if (!ModelState.IsValid)
        //{
        //    Console.WriteLine("Model State Invalid");
        //    return View(newCookbook); // Returns form with errors if validation fails
        // }

        AuthToken at;
        CookbooksAddVM cavm = new CookbooksAddVM();
        bool test;
        try
        {
            at = JsonConvert.DeserializeObject<AuthToken>(HttpContext.Session.GetString("authToken")!)!;
            Console.WriteLine("User " + at.username + " Deserialized");
            test = DBQueryModel.CreateCookbookNode(at.username, newCookbook.CookbookTitle, newCookbook.CookbookDescription).Result;
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
        AuthToken at;
        Cookbook cb;

        try
        {
            at = JsonConvert.DeserializeObject<AuthToken>(HttpContext.Session.GetString("authToken")!)!;
            cb = DBQueryModel.GetCookbook(cookbookName, at).Result;
        }
        catch (Exception e)
        {
            cb = new Cookbook();
            cb.Title = "Error, cookbook " + cookbookName + "could not be retrieved. Exception: " + e;
        }
        /*
        if (cookbook == null)
        {
            return NotFound(); // If no cookbook found
        } */


        var viewModel = new CookbooksEditVM
        {
            cookbookName = cb.Title,
            recipe = new Recipe()
        };

        return View(viewModel);
    }

    // Edit method (POST): Handles form submission to update an existing cookbook
    [HttpPost]
    public IActionResult Edit(CookbooksEditVM viewModel)
    {
        // if (!ModelState.IsValid)
        // {
        //    return View(viewModel); // Return form with errors
        //}

        // Uncomment the following line if you want to use seed data
        // var cookbook = RecipeSeedData.cookbooks.FirstOrDefault(c => c.Title == viewModel.CookbookName);

        // For now, simulate updating a cookbook (e.g., with a service or database)
        Cookbook cookbook = new Cookbook { Title = viewModel.cookbookName };

        if (cookbook == null)
        {
            return NotFound();
        }

        // Update the cookbook title
        cookbook.Title = viewModel.cookbookName;

        return RedirectToAction("Index");
    }
}
