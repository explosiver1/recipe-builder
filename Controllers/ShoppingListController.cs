using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RecipeBuilder.Models;
using RecipeBuilder.ViewModels;

namespace RecipeBuilder.Controllers;

public class ShoppingListController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

}