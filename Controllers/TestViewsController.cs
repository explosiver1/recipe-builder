using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using RecipeBuilder.Models;
using RecipeBuilder.ViewModels;

namespace RecipeBuilder.Controllers;

public class TestViewsController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        SeedData.TestDataAccess();
        return View();
    }

    [HttpPost]
    public IActionResult Index(TestViewModel model)
    {      
        CtrlModel.ResetTesterAccount(model.TestUserName);
        return RedirectToAction("Index", "Home");
    }

    // public IActionResult Results(TestViewModel data)
    // {
    //     // data.fName ="Sam";
    //     return View(data);
    // }



}

