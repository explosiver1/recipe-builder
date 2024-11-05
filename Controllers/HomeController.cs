using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RecipeBuilder.Models;
using RecipeBuilder.ViewModels;
using Newtonsoft.Json;

namespace RecipeBuilder.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult Private()
    {
        HomePrivateVM viewModel = new HomePrivateVM();
        try
        {
            AuthToken at = JsonConvert.DeserializeObject<AuthToken>(HttpContext.Session.GetString("authToken")!)!;
            viewModel.cookbooks = CtrlModel.GetCookbookList(at.username);
            viewModel.recipes = CtrlModel.GetRecipeList(at.username);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error retrieving cookbooks and recipes. Exception: " + e);
            RedirectToAction("Index", "Account");
        }


        return View(viewModel);
    }
}
