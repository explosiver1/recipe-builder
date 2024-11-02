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
            var viewModel = new MealPlannerIndexVM
            {
                mealPlanner = new MealPlanner()
            };
            return View(viewModel);
        }

        // GET: /MealPlanner/Month
        [HttpGet]
        public IActionResult Month()
        {
            var viewModel = new MealPlannerMonthVM
            {
                mealPlanner = new MealPlanner()
            };
            return View(viewModel);
        }

        // // POST: /MealPlanner/Month
        // [HttpPost]
        // public IActionResult Plan(MealPlannerMonthVM viewModel)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         // Logic to save or update the meal plan
        //         viewModel.mealPlanner.ScheduleMeal(new MealSet { Name = "Sample Meal", Description = "A test meal" });
        //         // interact with the database
        //         return RedirectToAction("Index");
        //     }
        //     return View(viewModel);
        // }

        // GET: /MealPlanner/Daily
        [HttpGet]
        public IActionResult Daily(DateOnly date)
        {
            var viewModel = new MealPlannerDailyVM();
            viewModel.mealPlanner.ScheduledMeals = [SeedData.cookieMeal, SeedData.cookieMeal2];
            viewModel.mealPlanner.Date = date;

            return View(viewModel);
        }

        // GET: /MealPlanner/Week
        [HttpGet]
        public IActionResult Week()
        {
            var viewModel = new MealPlannerWeekVM
            {
                mealPlanner = new MealPlanner()
            };
            return View(viewModel);
        }

        // // POST: /MealPlanner/Remove
        // [HttpPost]
        // public IActionResult Remove(string mealSetName)
        // {
        //     var mealPlanner = new MealPlanner();
        //     mealPlanner.RemoveMeal(mealSetName);

        //     return RedirectToAction("Index");
        // }
    }
}