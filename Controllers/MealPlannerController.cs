using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RecipeBuilder.Models;
using RecipeBuilder.ViewModels;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;

namespace RecipeBuilder.Controllers;

public class MealPlannerController : Controller
{
    // GET: /MealPlanner/
    [HttpGet]
    public IActionResult Index(DateOnly date)
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
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return RedirectToAction("Index", "Home");
        }

        MealPlannerIndexVM viewModel = new MealPlannerIndexVM();
        viewModel.ScheduledMealsToday = CtrlModel.GetMealsForDate(date, at.username);
        viewModel.ScheduledMealsThisWeek = CtrlModel.GetMealsForWeek(date, at.username);
        //var currentDate = date;
        //var startOfWeek = DateHelper.GetStartOfWeek(currentDate);
        //var datesInWeek = DateHelper.GetDatesForWeek(startOfWeek);

        //var viewModel = new MealPlannerIndexVM
        //{
        //    ScheduledMealsThisWeek = new MPWeek
        //    {
        //        Days = datesInWeek.Select(date =>
        //            new MPDay
        //            {
        //                Date = date, // Ensure the Date property exists in MPDay
        //                Meals = CtrlModel.getMealsForDate(date, at.username).Select(meal => new MPMeal
        //                {
        //                    mealDescription = meal.Description,
        //                    recipes = meal.Recipes
        //                }).ToList()
        //            }).ToList()
        //    },

        //    ScheduledMealsToday = new MealPlanner
        //    {
        //        Date = date,
        //        ScheduledMeals = CtrlModel.getMealsForDate(date, at.username)
        //    }
        //};

        return View(viewModel);
    }

    /* public IActionResult createScheduledMeal()
    {
        MealPlanner.ScheduleMeal(new MealSet());
        return View();
    } */




    // GET: /MealPlanner/Month/ given date
    [HttpGet]
    public IActionResult Month(DateOnly date)
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
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return RedirectToAction("Index", "Home");
        }

        // Initialize VM to be sent to view
        MealPlannerMonthVM monthVM = new MealPlannerMonthVM();
        Console.WriteLine("Created blank monthVM for {0}", date);
        // Set Month Name
        monthVM.monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(date.Month);
        Console.WriteLine("Month Name Set: {0}", monthVM.monthName);

        Console.WriteLine("Calling CtrlModel to get Data");
        // Get month data to VM
        monthVM.monthPlans = CtrlModel.GetMealsForMonth(date, at.username);
        Console.WriteLine("Data received from CtrlModel. Sending to View");

        return View(monthVM);
    }

    // GET: /MealPlanner/Daily
    [HttpGet]
    public IActionResult Daily(DateOnly date)
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
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return RedirectToAction("Index", "Home");
        }

        MealPlannerDailyVM viewModel = new MealPlannerDailyVM();
        viewModel.mealPlansForDay = CtrlModel.GetMealsForDate(date, at.username);

        return View(viewModel);
    }


    // GET: /MealPlanner/Week
    [HttpGet]
    public IActionResult Week(DateOnly date)
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
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return RedirectToAction("Index", "Home");
        }

        var viewModel = new MealPlannerWeekVM();
        viewModel.selectedWeek = CtrlModel.GetMealsForWeek(date, at.username);
        return View(viewModel);
    }

    //    // Helper method to get meal plans for a date range (for the month view)
    //    private List<MealPlanner> GetMealPlansForDateRange(DateOnly startDate, DateOnly endDate, string username)
    //    {

    //        var mealPlans = new List<MealPlanner>();

    //        for (var date = startDate; date <= endDate; date = date.AddDays(1))
    //        {
    //            var dailyMeals = CtrlModel.getMealsForDate(date, username);
    //            if (dailyMeals.Any())
    //            {
    //                mealPlans.Add(new MealPlanner
    //                {
    //                    Date = date,
    //                    ScheduledMeals = dailyMeals
    //                });
    //            }
    //        }

    //        return mealPlans;
    //}

    [HttpPost]
    public IActionResult RemoveFromMealPlanner(MealPlannerDailyVM data)
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
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return RedirectToAction("Index", "Home");
        }

        //CtrlModel.RemoveFromMealPlanner(data., at.username);
        return RedirectToAction("Index");
    }

        


    //     // POST: /MealPlanner/Remove
    //     [HttpPost]
    //    public IActionResult Remove(string mealSetName)
    //    {
    //        var mealPlanner = new MealPlanner();
    //        mealPlanner.RemoveMeal(mealSetName);

    //        return RedirectToAction("Index");
    //    }


}

