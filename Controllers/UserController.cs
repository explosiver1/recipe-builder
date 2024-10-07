using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using recipe_builder.Models;

namespace recipe_builder.Controllers;

public class UserController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Settings()
    {
        return View();
    }
}