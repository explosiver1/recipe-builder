using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RecipeBuilder.Models;

namespace recipe_builder.Controllers;

public class GroupsController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Group()
    {
        return View();
    }

    public IActionResult UserProfile()
    {
        return View();
    }

}