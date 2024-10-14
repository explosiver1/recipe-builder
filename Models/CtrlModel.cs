using System;

namespace RecipeBuilder.Models;

public class CtrlModel
{
    // Used to send recipe from DB to user via controller
    public Recipe GetRecipe() //String recipeName)
    {
        // Update so that the recipe returned is created by the dbmodel & returned here to pass on to ctrler
        return new Recipe {Name = "Chicken Parm"};
    }

    // Used to send recipe from user via controller to DB
    public Recipe SetRecipe(Recipe recipe)
    {
        return new Recipe {Name = "Chicken Parm"};
    }
}