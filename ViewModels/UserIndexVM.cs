using System;
using System.ComponentModel.DataAnnotations;
using RecipeBuilder.Models;

namespace RecipeBuilder.ViewModels;

public class UserIndexVM()
{
    public List<string> cookbooks { get; set; } = new List<string>();
    public List<string> recipes { get; set; } = new List<string>();
}
