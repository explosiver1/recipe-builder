using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RecipeBuilder.Models;
using RecipeBuilder.ViewModels;

namespace RecipeBuilder.Controllers;

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