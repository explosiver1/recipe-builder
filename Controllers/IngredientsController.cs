using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RecipeBuilder.Models;

namespace RecipeBuilder.Controllers;

public class IngredientsController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Ingredient()
    {
        return View();
    }

}