using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RecipeBuilder.Models;
using RecipeBuilder.ViewModels;
using Newtonsoft.Json;


namespace RecipeBuilder.Controllers;

public class AccountController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Login()
    {
        //Authenticate(string username, string password)
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string username, string password)
    {
        //TODO - Sanitize UI
        bool isAuthTokenValid = await DBQueryModel.Authenticate(username, password);
        if (isAuthTokenValid)
        {
            HttpContext.Session.SetString("user", username);
            HttpContext.Session.SetString("password", password);
            HttpContext.Session.SetString("creation", DateTime.UtcNow.ToString());
            HttpContext.Session.SetString("expiration", DateTime.UtcNow.AddHours(0.5f).ToString());
            return RedirectToAction("Home", "Private");
        }
        else
        {
            HttpContext.Session.SetString("user", "ERROR");
            return View(new AccountLoginVM { authValid = false });
        }
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
