using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecipeBuilder.Models;

public class MPWeek()
{
    public List<MPDay> days = new List<MPDay>();
}