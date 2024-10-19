using System;
using System.ComponentModel.DataAnnotations;
using RecipeBuilder.Models;

namespace RecipeBuilder.ViewModels;

public class AccountCreateVM()
{
    public required String userName {get; set;}
}