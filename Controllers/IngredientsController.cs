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
        List<string> ingredients = new List<string>(); //CtrlModel.GetIngredientNameList();
        Dictionary<string, List<string>> ingredientNames = new Dictionary<string, List<string>>(); //CtrlModel.GetABCListDict(ingredients);
        try
        {
            AuthToken at = JsonConvert.DeserializeObject<AuthToken>(HttpContext.Session.GetString("authToken")!)!;
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
        Ingredient ing = new Ingredient();
        try
        {

            AuthToken at = JsonConvert.DeserializeObject<AuthToken>(HttpContext.Session.GetString("authToken")!)!;
            ing = CtrlModel.GetIngredient(at.username, ingName);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error, ingredient could not be retrieved. Exception: " + e);
        }
        return View(ing);
    }

}
