using System;
using System.ComponentModel.DataAnnotations;
using RecipeBuilder.Models;

namespace RecipeBuilder.ViewModels;

public class AccountLoginVM()
{
    public required String userName {get; set;}
    public required String password {get; set;}
}