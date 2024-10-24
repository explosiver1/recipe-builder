using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RecipeBuilder.Models;
using RecipeBuilder.ViewModels;

namespace RecipeBuilder.Controllers;

public class CookbooksController : Controller
{
    // Index method: Displays a list of cookbooks
    [HttpGet]
    public IActionResult Index()
    {
    // Uncomment the following line if you want to use seed data
    // CookbooksIndexVM viewModel = new CookbooksIndexVM { Cookbooks = RecipeSeedData.cookbooks };

    // For now, return an empty view model or integrate with a database
    CookbooksIndexVM viewModel = new CookbooksIndexVM 
    { 
        cookbooks = CtrlModel.GetCookbookList()// Empty list for now, until data source is implemented
    };

    return View(viewModel);
    }

    // Cookbook method: Displays a specific cookbook by ID
    [HttpGet]
    public IActionResult Cookbook(string id)
    {
    // Uncomment the following line if you want to use seed data
    // var cookbook = RecipeSeedData.cookbooks.FirstOrDefault(c => c.Id == id);

    // For now, set cookbook to null or fetch from a database
    var cookbook = new Cookbook { Title = "Sample Cookbook", Recipes = new List<Recipe>() };

    if (cookbook == null)
    {
        return NotFound(); // Handle case where no cookbook is found
    }

    CookbooksCookbookVM viewModel = new CookbooksCookbookVM
    {
        cookbook = cookbook
    };

        return View(viewModel);
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }
    // Add method (POST): Handles form submission to create a new cookbook
    [HttpPost]
    public IActionResult Add(Cookbook newCookbook)
    {
        if (!ModelState.IsValid)
        {
            return View(newCookbook); // Returns form with errors if validation fails
        }

        // Uncomment the following line if you want to add to seed data
        // RecipeSeedData.cookbooks.Add(newCookbook);

        // For now, you can integrate with a database or just return a success view
        return RedirectToAction("Index"); // Redirect back to index
    }


    // Edit method (GET): Display the form to edit an existing cookbook
    [HttpGet]
    public IActionResult Edit(string cookbookName)
    {
    // Uncomment the following line if you want to use seed data
    // var cookbook = RecipeSeedData.cookbooks.FirstOrDefault(c => c.Title == cookbookName);

    // For now, return a dummy cookbook object or fetch from a database
    var cookbook = new Cookbook { Title = cookbookName, Recipes = new List<Recipe>() };

    if (cookbook == null)
    {
        return NotFound(); // If no cookbook found
    }

    var viewModel = new CookbooksEditVM
    {
        cookbookName = cookbook.Title,
        recipe = new Recipe() 
    };

        return View(viewModel);
    }

    // Edit method (POST): Handles form submission to update an existing cookbook
    [HttpPost]
    public IActionResult Edit(CookbooksEditVM viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel); // Return form with errors
        }

        // Uncomment the following line if you want to use seed data
        // var cookbook = RecipeSeedData.cookbooks.FirstOrDefault(c => c.Title == viewModel.CookbookName);

        // For now, simulate updating a cookbook (e.g., with a service or database)
        var cookbook = new Cookbook { Title = viewModel.cookbookName };

        if (cookbook == null)
        {
            return NotFound();
        }

        // Update the cookbook title
        cookbook.Title = viewModel.cookbookName;

        return RedirectToAction("Index");
    }
}