using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RecipeBuilder.Models;

namespace RecipeBuilder.Controllers;

public class AccountController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Login()
    {
        return View();
    }
    public IActionResult Create()
    {
        return View();
    }
    public IActionResult Recovery()
    {
        return View();
    }

}