using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using recipe_builder.Models;

namespace recipe_builder.Controllers;

public class CookbooksController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Index(String cookbookName)
    {
        return View();
    }

    [HttpGet]
    public IActionResult Cookbook()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Cookbook(String recipeName)
    {
        // Update to get recipe model instance using recipeName & pass that recipe to view
        // Update to redirectToAction 
        return View("~/Views/Recipe/Index.aspx");
    }

}