using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using recipe_builder.Models;

namespace recipe_builder.Controllers;

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