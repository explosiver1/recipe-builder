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
        return View();
    }

    [HttpPost]
    public IActionResult Index(TestViewModel model)
    {
        //For both returns, the string is the view or controller method
        //If view or controller method in a different view folder or controller, need full path
        //model sends data to the view/action
        //View will just load the view with model data directly
        //RedirectToAction will call the action with name Results and pass it data model
        //This allows for more processing if needed
        //return View("Results", model);//Might need to use when needing to hide data
        //If need to redirect to action in different controller, use second string argument of controller name
        return RedirectToAction("Results", model);
    }
    public IActionResult ReturnHome()
    {
        return RedirectToAction("Index", "Home");
    }
    public IActionResult Results(TestViewModel data)
    {
        // data.fName ="Sam";
        return View(data);
    }



}

