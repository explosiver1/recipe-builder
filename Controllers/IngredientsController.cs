using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RecipeBuilder.Models;
using RecipeBuilder.ViewModels;
using Newtonsoft.Json;

namespace RecipeBuilder.Controllers;

public class IngredientsController : Controller
{
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
        catch (Exception)
        {
            return RedirectToAction("Index", "Home");
        }
        
        List<string> ingredients = new List<string>(); //CtrlModel.GetIngredientNameList();
        Dictionary<string, List<string>> ingredientNames = new Dictionary<string, List<string>>(); //CtrlModel.GetABCListDict(ingredients);
        try
        {
            ingredients = CtrlModel.GetIngredientNameList(at.username);
            ingredientNames = CtrlModel.GetABCListDict(ingredients);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error, ingredients could not be retrieved. Exception: " + e);
        }


        IngredientsIndexVM viewModel = new IngredientsIndexVM { ingredientNamesDict = ingredientNames };
        return View(viewModel);
    }

    public IActionResult Ingredient(string ingName)
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
        
        Ingredient ing = new Ingredient();
        try
        {

            ing = CtrlModel.GetIngredient(at.username, ingName);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error, ingredient could not be retrieved. Exception: " + e);
        }
        return View(ing);
    }

}
