using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RecipeBuilder.Models;
using RecipeBuilder.ViewModels;
using Newtonsoft.Json;

namespace RecipeBuilder.Controllers;

public class MealsController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        //If user isn't logged in, don't allow access to this page - redirect to main site page
        AuthToken at;
        try
        {
            at = JsonConvert.DeserializeObject<AuthToken>(HttpContext.Session.GetString("authToken")!)!;
            if (!at.Validate())
            {
                throw new Exception("Authentication Expired. Please login again.");
            }
        }
        catch (Exception e)
        {
            return RedirectToAction("Index", "Home");
        }

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
        //If user isn't logged in, don't allow access to this page - redirect to main site page
        AuthToken at;
        try
        {
            at = JsonConvert.DeserializeObject<AuthToken>(HttpContext.Session.GetString("authToken")!)!;
            if (!at.Validate())
            {
                throw new Exception("Authentication Expired. Please login again.");
            }
        }
        catch (Exception e)
        {
            return RedirectToAction("Index", "Home");
        }

        return View();
    }

    public IActionResult Look(string id)
    {
        //If user isn't logged in, don't allow access to this page - redirect to main site page
        AuthToken at;
        try
        {
            at = JsonConvert.DeserializeObject<AuthToken>(HttpContext.Session.GetString("authToken")!)!;
            if (!at.Validate())
            {
                throw new Exception("Authentication Expired. Please login again.");
            }
        }
        catch (Exception e)
        {
            return RedirectToAction("Index", "Home");
        }
        
        MealsLookVM mealVM = new MealsLookVM();
        mealVM.meal = CtrlModel.getMeal(id);
        return View(mealVM);
    }
}