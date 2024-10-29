using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RecipeBuilder.Models;
using RecipeBuilder.ViewModels;

namespace RecipeBuilder.Controllers;

public class MealsController : Controller
{
    public IActionResult Index()
    {
        MealsIndexVM mealVM = new MealsIndexVM();
        mealVM.meals = CtrlModel.getMeals();
        return View(mealVM);
    }

    public IActionResult Create()
    {
        return View();
    }
    
    // public IActionResult Look()
    // {
    //     MealsLookVM mealVM = new MealsLookVM();
    //     mealVM.meal = CtrlModel.getMeal();
    //     return View();
    // }
}