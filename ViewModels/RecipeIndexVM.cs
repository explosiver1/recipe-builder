using System;
using System.ComponentModel.DataAnnotations;
using RecipeBuilder.Models;

namespace RecipeBuilder.ViewModels
{
    public class RecipeIndexVM
    {
        public required Cookbook Cookbook { get; set; } = new Cookbook(); // Initialize a new Cookbook object
    }
}