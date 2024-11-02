using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecipeBuilder.Models
{
    public class MPDay
    {
        public DateOnly Date { get; set; } // Represents the date for the day
        public List<MPMeal> Meals { get; set; } = new List<MPMeal>(); // List of meals for the day
    }
}