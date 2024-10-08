using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RecipeBuilder.Models;

namespace RecipeBuilder.Controllers;

public class RecipeController : Controller
{
    [HttpGet]
    public IActionResult Index(String selectedRecipe)
    {
        //Fix for possible recipe not found?
        Recipe recipe = CtrlModel.GetRecipe(selectedRecipe);
        //Should we make a view model & determine how best to go between view model & data model
        return View(recipe);
    }
    
    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Edit(String selectedRecipe)
    {
        return View();
    }

}