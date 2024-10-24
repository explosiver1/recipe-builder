using System;

namespace RecipeBuilder.Models;

/* This class exists to provide a single class for all controllers to call on without the complexity of the DB model
Since multiple controllers need to call the same queries, this allows one method to be changed if needed 
rather than multiple */
public static class CtrlModel
{
    public static List<Cookbook>  GetCookbookList()//string userName)
    
    {
        List<Cookbook> cookbookList = RecipeSeedData.cookbooks;// Update to be DBQueryModel Function Call
        return cookbookList;
    }

    /* Fetch Cookbook from DB by name & return to controller */
    public static Cookbook  GetCookbook(string cookbookName)
    {
        Cookbook cookbookObj = RecipeSeedData.GetCookbook(cookbookName);// Update to be DBQueryModel Function Call
        return cookbookObj;
    }


    /* Fetch recipe from DB by name & return it to controller */
    public static Recipe GetRecipe(string cookbookName, string recipeName)
    {
        // Will need updated to match DBQueryModel's Method & Parameters
        //Recipe recipe = DBQueryModel.GetRecipe(recipeName);
        Recipe recipe = RecipeSeedData.GetRecipe(cookbookName, recipeName);
        return recipe;
    }

    // Send recipe data from user to DB (& return recipe to the controller?)
    public static Recipe SetRecipe(string cookbookName, Recipe recipe)
    {
        // Update to DB method
        bool recipeAdded = RecipeSeedData.AddRecipe(cookbookName, recipe);
        if (recipeAdded)
        {
            return recipe;
        }
        else
        {
            return new Recipe(); //Update for how to handle recipe addition failure
        }
    }

    /* Returns an alphabetized list of all ingredients a user has */
    public static List<string> GetIngredients()
    {
        List<string> ingredients = ["Oranges","Apples","Bananas", "Pears", "Tomatoes", "Spinach", "Sausage"];
        return ingredients;
    }

    public static Dictionary<string, List<string>> GetABCListDict(List<string> myList)
    {
        myList.Sort();
        Dictionary<string, List<string>> myDictionary = new Dictionary<string, List<string>>();
        foreach (string item in myList)
        {
            string firstLetter = item[0].ToString().ToUpper();

            if (!myDictionary.ContainsKey(firstLetter))
            {
                myDictionary.Add(firstLetter, new List<string>());
            }
            myDictionary[firstLetter].Add(item);
        }
        return myDictionary;
    }
}
