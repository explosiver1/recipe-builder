using System;

namespace RecipeBuilder.Models;

public static class CtrlModel
{
    // Fetch recipe from DB & return it to controller
    public static Recipe GetRecipe(String recipeName)
    {
        // Will need updated to match DBQueryModel's Method & Parameters
        //Recipe recipe = DBQueryModel.GetRecipe(recipeName);

        // Update so that the recipe returned is created by the dbmodel & returned here to pass on to ctrler
        return new Recipe {Name = recipeName};//recipe;
    }

    // Send recipe data from user to DB (& return recipe to the controller?)
    public static Recipe SetRecipe(Recipe recipe)
    {

        return new Recipe {Name = "Chicken Parm"};
    }
}
