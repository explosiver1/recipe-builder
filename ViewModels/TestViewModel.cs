using System;
using System.ComponentModel.DataAnnotations;

namespace recipe_builder.ViewModels;

public class TestViewModel
{
    [Required(ErrorMessage ="First Name is required")]
    public string? fName { get; set; }
    public string? lName { get; set; }
}
