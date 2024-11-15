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

            List<IngredientDetail> pantryItems = CtrlModel.GetPantryItems(at.username);//SeedData.GetPantryItems();
            PantryIndexVM viewModel = new PantryIndexVM
            {
                ABCPantry = CtrlModel.GetABCListDict(pantryItems),
                newIngredient = new IngredientDetail()
            };

            // Log items to confirm they are being passed to the view
            Console.WriteLine("Current pantry items:");
            foreach (var letterList in viewModel.ABCPantry)
            {
                Console.WriteLine(letterList.Key);
                foreach (var item in letterList.Value)
                {
                    Console.WriteLine(item.Name);
                }
            }

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddItemToPantry(PantryIndexVM pantryVM)
        {
            pantryVM.newIngredient.DisplayIngredientDetail();
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
            CtrlModel.AddItemToPantry(pantryVM.newIngredient, at.username);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult EditPantryItem(string name, string unit, string quantity, string qualifier)
        {
            Console.WriteLine($"Entered EditPantryItem with {name}, {unit}, {quantity}, {qualifier}");
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
            CtrlModel.EditItemInPantry(new IngredientDetail() { Name = name, Quantity = Convert.ToDouble(quantity), Qualifier = qualifier, Unit = unit }, at.username);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddItemToShoppingList(string itemName)
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
            bool success = CtrlModel.MoveItemBetweenLists(at.username, itemName, toPantry: false);
            return success ? RedirectToAction("Index") : NotFound();
        }

        [HttpPost]
        public IActionResult AddItemFromShoppingList(string itemName)
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
            bool success = CtrlModel.MoveItemBetweenLists(at.username, itemName, toPantry: true);
            return success ? RedirectToAction("Index") : NotFound();
        }

        // Remove an item from the pantry
        [HttpPost]
        public IActionResult RemoveFromPantry(string ingredientName)
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
            //var ingredient = CtrlModel.GetIngredientByNameFromShoppingList(itemName, at.username);
            //if (ingredient == null)
            //{
            //    return NotFound();
            //}

            CtrlModel.RemoveItemFromPantry(ingredientName, at.username);
            return RedirectToAction("Index");
        }
    }
}
