using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecipeBuilder.Models
{
    public class MPWeek
    {
        public List<MPDay> Days { get; set; } = new List<MPDay>(); // Holds each day's meals in the week
    }
}