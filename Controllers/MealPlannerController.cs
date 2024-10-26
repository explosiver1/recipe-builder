using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RecipeBuilder.Models;
using RecipeBuilder.ViewModels;

namespace RecipeBuilder.Controllers
{
    public class MealPlannerController : Controller
    {
        // GET: /MealPlanner/
        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = new MealPlannerIndexVm
            {
                mealPlanner = new MealPlanner()
            };
            return View(viewModel);
        }

        // GET: /MealPlanner/Plan
        [HttpGet]
        public IActionResult Plan()
        {
            var viewModel = new MealPlannerPlanVm
            {
                mealPlanner = new MealPlanner()
            };
            return View(viewModel);
        }

        // POST: /MealPlanner/Plan
        [HttpPost]
        public IActionResult Plan(MealPlannerPlanVm viewModel)
        {
            if (ModelState.IsValid)
            {
                // Logic to save or update the meal plan
                viewModel.mealPlanner.ScheduleMeal(new MealSet { Name = "Sample Meal", Description = "A test meal" });
                // interact with the database
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        // GET: /MealPlanner/Daily
        [HttpGet]
        public IActionResult Daily()
        {
            var viewModel = new MealPlannerDailyVm
            {
                mealPlanner = new MealPlanner()
            };
            return View(viewModel);
        }

        // GET: /MealPlanner/Week
        [HttpGet]
        public IActionResult Week()
        {
            var viewModel = new MealPlannerWeekVm
            {
                mealPlanner = new MealPlanner()
            };
            return View(viewModel);
        }

        // POST: /MealPlanner/Remove
        [HttpPost]
        public IActionResult Remove(string mealSetName)
        {
            var mealPlanner = new MealPlanner();
            mealPlanner.RemoveMeal(mealSetName);

            return RedirectToAction("Index");
        }
    }
}