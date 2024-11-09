using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RecipeBuilder.Models;
using RecipeBuilder.ViewModels;
using Newtonsoft.Json;

namespace RecipeBuilder.Controllers
{
    public class PantryController : Controller
    {
        // Display all items in the pantry
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
        
        [HttpPost]
        public IActionResult AddItemToShoppingList(string itemName)
        {
            bool success = CtrlModel.MoveItemBetweenLists(itemName, toPantry: false);
            return success ? RedirectToAction("Index") : NotFound();
        }

        [HttpPost]
        public IActionResult AddItemFromShoppingList(string itemName)
        {
            bool success = CtrlModel.MoveItemBetweenLists(itemName, toPantry: true);
            return success ? RedirectToAction("Index") : NotFound();
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
}