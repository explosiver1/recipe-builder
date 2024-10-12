using System;
using System.ComponentModel.DataAnnotations;
using RecipeBuilder.Models;

namespace RecipeBuilder.ViewModels;

public class RecipeIndexVM()
{
    //public required string userName {get;set;}
    public required string cookbookName {get;set;}
    public required Recipe recipe {get;set;}
}