using Microsoft.AspNetCore.Mvc;
using RecipeBuilder.Models;
using RecipeBuilder.ViewModels;
using Newtonsoft.Json;

namespace RecipeBuilder.Controllers;

public class ShoppingListController : Controller
{
    // Index method: Displays a list of all items in the shopping list
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

        ShoppingListIndexVM viewModel = new ShoppingListIndexVM
        {
            ABCShoppingList = CtrlModel.GetABCListDict(CtrlModel.GetShoppingListItems(at.username))
        };
        return View(viewModel);
    }

    //// Add method (GET): Display form to add a new item to the shopping list
    //[HttpGet]
    //public IActionResult Add()
    //{
    //    //If user isn't logged in, don't allow access to this page - redirect to main site page
    //    AuthToken at;
    //    try
    //    {
    //        at = JsonConvert.DeserializeObject<AuthToken>(HttpContext.Session.GetString("authToken")!)!;
    //        if (!at.Validate())
    //        {
    //            throw new Exception("Authentication Expired. Please login again.");
    //        }
    //    }
    //    catch (Exception e)
    //    {
    //        return RedirectToAction("Index", "Home");
    //    }

    //    return View();
    //}

    // Add method (POST): Handles form submission to add a new item to the shopping list
    [HttpPost]
    public IActionResult AddItemToShoppingList(IngredientDetail item)
    {
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
        if (!ModelState.IsValid)
        {
            return RedirectToAction("Index");
        }

        CtrlModel.AddItemToShoppingList(at.username, item); // Method to add ingredient to shopping list
        return RedirectToAction("Index");
    }

    // Remove method: Removes an item from the shopping list
    [HttpPost]
    public IActionResult Remove(string ingredientName)
    {
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

        //var item = CtrlModel.GetIngredientDetailFromShoppingList(ingredient, at.username);
        //if (item == null)
        //{
        //    return NotFound();
        //}

        bool tmp = CtrlModel.RemoveItemFromShoppingList(at.username, ingredientName);
        return RedirectToAction("Index");
    }

    // CheckItemOff method: Marks an item as checked off in the shopping list
    [HttpPost]
    public IActionResult CheckItemOff(string ingredientName)
    {
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
        //var ingredient = CtrlModel.GetIngredientByNameFromShoppingList(ingredientName, at.username);
        //if (ingredient == null)
        //{
        //    return NotFound();
        //}

        bool tmp = CtrlModel.CheckItemOffShoppingList(at.username, ingredientName); // Method to check off item
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult UnCheckItemOff(string ingredientName)
    {
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
        //var ingredient = CtrlModel.GetIngredientByNameFromShoppingList(ingredientName, at.username);
        //if (ingredient == null)
        //{
        //    return NotFound();
        //}

        bool tmp = CtrlModel.UnCheckItemOffShoppingList(at.username, ingredientName); // Method to check off item
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult AddItemToPantry(string itemName)
    {
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
        var ingredient = CtrlModel.GetIngredientByNameFromShoppingList(itemName, at.username);
        if (ingredient == null)
        {
            return NotFound();
        }

        CtrlModel.AddItemToPantry(ingredient, at.username);
        CtrlModel.RemoveItemFromShoppingList(at.username, ingredient.Name);
        return RedirectToAction("Index");
    }
}
