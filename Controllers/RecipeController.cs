using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RecipeBuilder.Models;
using RecipeBuilder.ViewModels;

namespace RecipeBuilder.Controllers;

public class RecipeController : Controller
{
    [HttpGet]
    public IActionResult Index(String cookbookName, String recipeName)//String userName 
    {
        // Update recipe= to be set to appropriate dbmodel method
        RecipeIndexVM viewModel = new RecipeIndexVM{cookbookName=cookbookName, recipe=RecipeSeedData.GetRecipe(cookbookName, recipeName)};//userName=userName, 
        return View(viewModel);
    }
    
    [HttpGet]
    public IActionResult Add(string cookbookName)//String userName 
    {
        RecipeAddVM viewModel = new RecipeAddVM{ cookbookName=cookbookName, recipe=new Recipe()};//userName=userName, 
        return View(viewModel);
    }

[HttpPost]
    public IActionResult Add(RecipeAddVM recipeVM)
    {
        // Send new recipe data to db
        Console.WriteLine("Got recipe data for " + recipeVM.recipe.Name);

        // Display view of new recipe
        // Update to conditional statement using bool returned from dbmodel whether successful save
        if (RecipeSeedData.AddRecipe(recipeVM.cookbookName, recipeVM.recipe))
        {
            return RedirectToAction("Index", recipeVM.cookbookName, recipeVM.recipe.Name);
            }
            else
            {
                return View(recipeVM.cookbookName);
            }
         
    }

    [HttpGet]
    public IActionResult Edit(string cookbookName, string recipeName)
    {
        return View();
    }

    [HttpGet]
    public IActionResult Look(String selectedRecipe)
    {
        return View();
    }

    [HttpGet]
    public IActionResult Select(String selectedRecipe)
    {
        return View();
    }

}