using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RecipeBuilder.Models;
using RecipeBuilder.ViewModels;

namespace RecipeBuilder.Controllers;

public class CookbooksController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        CookbooksIndexVM viewModel = new CookbooksIndexVM {cookbooks = RecipeSeedData.cookbooks};
        return View(viewModel);
    }

    [HttpGet]
    public IActionResult Cookbook(String id)
    {
        CookbooksCookbookVM viewModel = new CookbooksCookbookVM{cookbook=RecipeSeedData.GetCookbook(id)};
        return View(viewModel);
    }

}