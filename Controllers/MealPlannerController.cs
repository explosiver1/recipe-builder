using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RecipeBuilder.Models;

namespace recipe_builder.Controllers;

public class MealPlannerController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult Plan()
    {
        return View();
    }

    public IActionResult Daily()
    {
        return View();
    }

    public IActionResult Week()
    {
        return View();
    }

}