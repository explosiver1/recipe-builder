using System;
using System.ComponentModel.DataAnnotations;
using RecipeBuilder.Models;

namespace RecipeBuilder.ViewModels
{
    public class RecipeEditVM
    {
        public required Recipe Recipe { get; set; } = new Recipe(); // Initialize a new Recipe object
    }
}