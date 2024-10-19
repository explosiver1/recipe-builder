using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RecipeBuilder.Models;
using RecipeBuilder.ViewModels;

namespace RecipeBuilder.Controllers;

public class IngredientsController : Controller
{
    public IActionResult Index()
    {
        List<string> ingredients = ["Pears", "Apples", "Bananas"];
        // Dictionary<string, List<string>> ingredientNames = new
        ingredients.Sort();
        IngredientsIndexVM viewModel = new IngredientsIndexVM{ingredientNames=ingredients};
        return View(viewModel);
    }

    public IActionResult Ingredient()
    {
        return View();
    }

}