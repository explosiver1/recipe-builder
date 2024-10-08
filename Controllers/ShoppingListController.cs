using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RecipeBuilder.Models;

namespace RecipeBuilder.Controllers;

public class ShoppingListController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

}