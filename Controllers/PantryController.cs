using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RecipeBuilder.Models;
using RecipeBuilder.ViewModels;

namespace RecipeBuilder.Controllers;

public class PantryController : Controller
{
    // Display all items in the pantry
    [HttpGet]
    public IActionResult Index()
    {
        PantryIndexVM viewModel = new PantryIndexVM
        {
            items = CtrlModel.GetPantryItems()
        };

        // Log items to confirm they are being passed to the view
        Console.WriteLine("Current pantry items:");
        foreach (var item in viewModel.items)
        {
            Console.WriteLine(item.Name);
        }

        return View(viewModel);
    }

    // Add an item to the pantry from the shopping list
    [HttpPost]
    public IActionResult AddItemFromShoppingList(string itemName)
    {
        var ingredient = CtrlModel.GetIngredientByName(itemName);
        if (ingredient == null)
        {
            return NotFound();
        }

        CtrlModel.AddItemToPantry(ingredient);
        return RedirectToAction("Index");
    }

    // Remove an item from the pantry
    [HttpPost]
    public IActionResult Remove(string itemName)
    {
        var ingredient = CtrlModel.GetIngredientByName(itemName);
        if (ingredient == null)
        {
            return NotFound();
        }

        CtrlModel.RemoveItemFromPantry(ingredient);
        return RedirectToAction("Index");
    }
}