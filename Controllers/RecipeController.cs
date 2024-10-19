using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RecipeBuilder.Models;

namespace recipe_builder.Controllers;

public class RecipeController : Controller
{
    CtrlModel mod = new CtrlModel();
    public IActionResult Index()
{
    Recipe recipe = mod.GetRecipe();
    return View(recipe);
}

}