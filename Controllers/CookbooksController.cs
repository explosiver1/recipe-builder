using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using recipe_builder.Models;

namespace recipe_builder.Controllers;

public class CookbooksController : Controller
{
    CtrlModel mod = new CtrlModel();
    public IActionResult Index()
    {
        return View();
    }

}