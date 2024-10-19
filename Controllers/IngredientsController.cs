using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RecipeBuilder.Models;
using RecipeBuilder.Controllers;
using RecipeBuilder.ViewModels;

namespace RecipeBuilder.Controllers;

public class IngredientsController : Controller
{
    public IActionResult Index()
    {

        List<string> ingredients = CtrlModel.GetIngredients();
        Dictionary<string, List<string>> ingredientNames = CtrlModel.GetABCListDict(ingredients);

        IngredientsIndexVM viewModel = new IngredientsIndexVM{ingredientNamesDict = ingredientNames};
        return View(viewModel);
    }

    public IActionResult Ingredient()
    {
        return View();
    }

}
