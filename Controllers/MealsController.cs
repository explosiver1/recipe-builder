using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RecipeBuilder.Models;
using RecipeBuilder.ViewModels;

namespace RecipeBuilder.Controllers;

public class MealsController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        var mealVM = new MealsIndexVM
        {
            // Use the placeholder method to simulate combining data from Neo4j
            meals = CtrlModel.GetAllMeals().Concat(CtrlModel.GetMealsFromNeo4j()).ToList()
        };

        return View(mealVM);
    }
    
    //public IActionResult Index()
    //{
    //    MealsIndexVM mealVM = new MealsIndexVM();
    //    mealVM.meals = CtrlModel.getMeals();
    //    return View(mealVM);
    //}
    public IActionResult Create()
    {
        return View();
    }

    public IActionResult Look(string id)
    {
        MealsLookVM mealVM = new MealsLookVM();
        mealVM.meal = CtrlModel.getMeal(id);
        return View(mealVM);
    }
}