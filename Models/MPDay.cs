using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecipeBuilder.Models;

public class MPDay()
{
    public List<MPMeal> weeks = new List<MPMeal>();
}