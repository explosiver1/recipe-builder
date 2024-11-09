using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RecipeBuilder.Models;
using RecipeBuilder.ViewModels;
using Newtonsoft.Json;

namespace RecipeBuilder.Controllers;

public class UserController : Controller
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
        catch (Exception e)
        {
            return RedirectToAction("Index", "Home");
        }
        
        UserIndexVM viewModel = new UserIndexVM();
        try
        {
            viewModel.cookbooks = CtrlModel.GetCookbookList(at.username);
            viewModel.recipes = CtrlModel.GetRecipeList(at.username);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error retrieving cookbooks and recipes. Exception: " + e);
            RedirectToAction("Index", "Account");
        }


        return View(viewModel);
    }

    // public IActionResult Settings()
    // {
    //     return View();
    // }
}