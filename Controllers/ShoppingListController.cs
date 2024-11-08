using Microsoft.AspNetCore.Mvc;
using RecipeBuilder.Models;
using RecipeBuilder.ViewModels;

namespace RecipeBuilder.Controllers;

public class ShoppingListController : Controller
{
    // Index method: Displays a list of all items in the shopping list
    [HttpGet]
    public IActionResult Index()
    {
        ShoppingListIndexVM viewModel = new ShoppingListIndexVM
        {
            items = CtrlModel.GetShoppingListItems() // Assume this returns a list of all shopping list items
        };
        return View(viewModel);
    }

    // Add method (GET): Display form to add a new item to the shopping list
    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    // Add method (POST): Handles form submission to add a new item to the shopping list
    [HttpPost]
    public IActionResult Add(Ingredient ingredient)
    {
        if (!ModelState.IsValid)
        {
            return View(ingredient);
        }

        CtrlModel.AddItemToShoppingList(ingredient); // Method to add ingredient to shopping list
        return RedirectToAction("Index");
    }

    // Remove method: Removes an item from the shopping list
    [HttpPost]
    public IActionResult Remove(string ingredientName)
    {
        var ingredient = CtrlModel.GetIngredientByName(ingredientName); // Retrieve ingredient by name
        if (ingredient == null)
        {
            return NotFound();
        }

        CtrlModel.RemoveItemFromShoppingList(ingredient); // Method to remove ingredient
        return RedirectToAction("Index");
    }

    // CheckItemOff method: Marks an item as checked off in the shopping list
    [HttpPost]
    public IActionResult CheckItemOff(string ingredientName)
    {
        var ingredient = CtrlModel.GetIngredientByName(ingredientName);
        if (ingredient == null)
        {
            return NotFound();
        }

        CtrlModel.CheckItemOffShoppingList(ingredient); // Method to check off item
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult AddItemToPantry(string itemName)
    {
        var ingredient = CtrlModel.GetIngredientByName(itemName);
        if (ingredient == null)
        {
            return NotFound();
        }

        CtrlModel.AddItemToPantry(ingredient);
        CtrlModel.RemoveItemFromShoppingList(ingredient); 
        return RedirectToAction("Index");
    }
}