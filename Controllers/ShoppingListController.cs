using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RecipeBuilder.Models;

namespace recipe_builder.Controllers;

public class ShoppingListController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

}