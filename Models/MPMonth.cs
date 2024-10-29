using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecipeBuilder.Models;

public class MPMonth()
{
    public List<MPWeek> weeks = new List<MPWeek>();
}