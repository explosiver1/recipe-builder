using System;
using System.ComponentModel.DataAnnotations;

namespace RecipeBuilder.ViewModels;

public class TestViewModel
{
    [Required(ErrorMessage = "First Name is required")]
    public required string fName { get; set; }
    public string? lName { get; set; }
}
