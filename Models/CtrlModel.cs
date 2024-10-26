using System;

namespace RecipeBuilder.Models;

/* This class exists to provide a single class for all controllers to call on without the complexity of the DB model
Since multiple controllers need to call the same queries, this allows one method to be changed if needed 
rather than multiple */
public static class CtrlModel
{
    private static ShoppingList shoppingList = new ShoppingList();
    private static List<Ingredient> pantryItems = new List<Ingredient>();

    public static List<Cookbook>  GetCookbookList()//string userName)
    {
        List<Cookbook> cookbookList = SeedData.GetCookbookList();// Update to be DBQueryModel Function Call
        return cookbookList;
    }

    /* Fetch Cookbook from DB by name & return to controller */
    public static Cookbook?  GetCookbook(string cookbookName)
    {
        Cookbook? cookbookObj = SeedData.GetCookbook(cookbookName);// Update to be DBQueryModel Function Call
        return cookbookObj;
    }

    public static List<Recipe>  GetRecipeList()//string userName)
    
    {
        List<Recipe> recipeList = SeedData.GetRecipeList();// Update to be DBQueryModel Function Call
        return recipeList;
    }

    /* Fetch recipe from DB by name & return it to controller */
    public static Recipe? GetRecipe(string recipeName)
    {
        // Will need updated to match DBQueryModel's Method & Parameters
        //Recipe recipe = DBQueryModel.GetRecipe(recipeName);
        Recipe? recipe = SeedData.GetRecipe(recipeName);
        return recipe;
    }

    public static List<Ingredient> GetIngredientList()
    {
        List<Ingredient> myIngredients = SeedData.GetIngredientList();
        return myIngredients;
    }

    public static List<string> GetIngredientNameList()
    {
        List<string> myIngredients = SeedData.GetIngredientNameList();
        return myIngredients;
    }

    public static List<Recipe> GetRecipesForIngredient(string IngredientName)
    {
        List<Recipe> recipesWithIngredient = SeedData.GetRecipesForIngredient(IngredientName);
        return recipesWithIngredient;
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

    // Retrieves all items in the shopping list
    public static List<Ingredient> GetShoppingListItems()
    {
        return shoppingList.Items ?? new List<Ingredient>();
    }

    // Adds an ingredient to the shopping list
    public static void AddItemToShoppingList(Ingredient ingredient)
    {
        if (ingredient == null) return;
            
        if (shoppingList.Items == null)
        {
            shoppingList.Items = new List<Ingredient>();
        }

        if (!shoppingList.Items.Contains(ingredient))
        {
            shoppingList.Items.Add(ingredient);
            Console.WriteLine($"{ingredient.Name} added to the shopping list.");
        }
        else
        {
            Console.WriteLine($"{ingredient.Name} is already in the shopping list.");
        }
    }

    // Removes an ingredient from the shopping list
    public static void RemoveItemFromShoppingList(Ingredient ingredient)
    {
        if (ingredient == null || shoppingList.Items == null || !shoppingList.Items.Contains(ingredient))
        {
            Console.WriteLine("Ingredient not found in the shopping list.");
            return;
        }

        shoppingList.Items.Remove(ingredient);
        Console.WriteLine($"{ingredient.Name} removed from the shopping list.");
    }

    // Checks off an ingredient in the shopping list (can also remove it if desired)
    public static void CheckItemOffShoppingList(Ingredient ingredient)
    {
        if (ingredient == null || shoppingList.Items == null || !shoppingList.Items.Contains(ingredient))
        {
            Console.WriteLine("Ingredient not found in the shopping list.");
            return;
        }

        shoppingList.Items.Remove(ingredient);
        Console.WriteLine($"{ingredient.Name} checked off the shopping list.");
    }

    // Retrieves an ingredient by name from the shopping list, allowing a nullable return
    public static Ingredient? GetIngredientByName(string name)
    {
    if (shoppingList.Items == null) return null;

    return shoppingList.Items.FirstOrDefault(i => i.Name?.Equals(name, StringComparison.OrdinalIgnoreCase) ?? false);
    }

    // Retrieve all pantry items
    public static List<Ingredient> GetPantryItems()
    {
        return pantryItems;
    }

    // Add an ingredient to the pantry
    public static void AddItemToPantry(Ingredient ingredient)
    {
        if (ingredient == null || string.IsNullOrEmpty(ingredient.Name)) 
        {
            Console.WriteLine("Cannot add ingredient without a name.");
            return;
        }

        if (!pantryItems.Any(i => i.Name == ingredient.Name))
        {
            pantryItems.Add(ingredient);
            Console.WriteLine($"{ingredient.Name} added to the pantry.");
        }
        else
        {
            Console.WriteLine($"{ingredient.Name} is already in the pantry.");
        }
    }

    // Remove an ingredient from the pantry
    public static void RemoveItemFromPantry(Ingredient ingredient)
    {
        if (ingredient == null || !pantryItems.Contains(ingredient))
        {
            Console.WriteLine("Ingredient not found in the pantry.");
            return;
        }

        pantryItems.Remove(ingredient);
        Console.WriteLine($"{ingredient.Name} removed from the pantry.");
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
