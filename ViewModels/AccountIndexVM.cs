using System;
using System.ComponentModel.DataAnnotations;
using RecipeBuilder.Models;

namespace RecipeBuilder.ViewModels;

public class AccountIndexVM()
{
    public required Cookbook cookbook {get; set;}
}