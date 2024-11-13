using System;

namespace RecipeBuilder.Models;

/* This class exists to provide a single class for all controllers to call on without the complexity of the DB model
Since multiple controllers need to call the same queries, this allows one method to be changed if needed
rather than multiple */
public static class CtrlModel
{
    private static ShoppingList shoppingList = new ShoppingList();
    private static List<IngredientDetail> pantryItems = new List<IngredientDetail>();

    /* GET METHODS */

    public static List<Cookbook> GetCookbookList(string username)//string userName)
    {
        List<Cookbook> cookbookList = DBQueryModel.GetCookbooks(username).Result; //SeedData.GetCookbookList();// Update to be DBQueryModel Function Call
        return cookbookList;
    }

    /* Fetch Cookbook from DB by name & return to controller */
    public static Cookbook? GetCookbook(string cookbookName, string username)
    {
        Cookbook? cookbookObj = DBQueryModel.GetCookbook(cookbookName, username).Result; //SeedData.GetCookbook(cookbookName);// Update to be DBQueryModel Function Call
        return cookbookObj;
    }

    static CtrlModel()
    {
        //pantryItems.AddRange(SeedData.myIngredients.Take(2)); // Example: add first two items for testing
        shoppingList.Items = new List<IngredientDetail>(); //{ SeedData.ChocolateChips };
    }

    public static List<Recipe> GetRecipeList(string username)//string userName)

    {
        List<Recipe> recipeList = new List<Recipe>();
        foreach (string recipeName in GetRecipeNameList(username))
        {
            recipeList.Add(GetRecipe(username, recipeName)!);
        } //DBQueryModel.GetRecipeNodeNames(username).Result; //SeedData.GetRecipeList();// Update to be DBQueryModel Function Call
        return recipeList;
    }

    public static List<string> GetRecipeNameList(string username)
    {
        return DBQueryModel.GetRecipeNodeNames(username).Result;
    }

    public static List<string> GetCookbookNameList(string username)
    {
        return DBQueryModel.GetCookbookNameList(username).Result;
    }

    /* Fetch recipe from DB by name & return it to controller */
    public static Recipe GetRecipe(string username, string recipeName)
    {
        // Will need updated to match DBQueryModel's Method & Parameters
        //Recipe recipe = DBQueryModel.GetRecipe(recipeName);
        Recipe recipe = DBQueryModel.GetRecipe(username, recipeName).Result; //SeedData.GetRecipe(recipeName);
        //Console.WriteLine("Printing recipe retrieved in GetRecipe()");
        //recipe.PrintAllStats();
        return recipe;
    }

    public static List<IngredientDetail> GetIngredientsForRecipe(string username, string recipeName)
    {
        List<IngredientDetail> recipesIngredients = new List<IngredientDetail>();
        foreach (Ingredient ing in DBQueryModel.GetIngredientsByRecipe(username, recipeName).Result)
        {
            IngredientDetail ingD = DBQueryModel.GetIngredientDetail(username, recipeName, ing.Name).Result;
            recipesIngredients.Add(ingD);
        }
        return recipesIngredients;
    }

    public static List<Ingredient> GetIngredientList(string username)
    {
        List<Ingredient> myIngredients = new List<Ingredient>(); //SeedData.GetIngredientList();
        foreach (string ingName in GetIngredients(username))
        {
            myIngredients.Add(DBQueryModel.GetIngredient(ingName, username).Result);
        }
        return myIngredients;
    }

    public static Ingredient GetIngredient(string username, string ingName)
    {
        return DBQueryModel.GetIngredient(ingName, username).Result;
    }

    public static IngredientDetail GetIngredientDetail(string username, string ingName, string recipeName, string list = "")
    {
        return DBQueryModel.GetIngredientDetail(username, ingName, recipeName).Result;
    }

    public static List<string> GetIngredientNameList(string username)
    {
        List<string> myIngredients = DBQueryModel.GetIngredientNodeNames(username).Result; //SeedData.GetIngredientNameList();
        return myIngredients;
    }

    public static List<Recipe> GetRecipesForIngredient(string IngredientName, string username)
    {
        List<Recipe> recipesWithIngredient = new List<Recipe>(); //DBQueryModel.GetRecipeNodeNamesByIngredient(username, IngredientName).Result; //SeedData.GetRecipesForIngredient(IngredientName);
        foreach (string recipeName in GetRecipeNamesForIngredient(IngredientName, username))
        {
            recipesWithIngredient.Add(DBQueryModel.GetRecipe(username, recipeName).Result);
        }

        return recipesWithIngredient;
    }

    public static List<string> GetRecipeNamesForIngredient(string IngredientName, string username)
    {
        return DBQueryModel.GetRecipeNodeNamesByIngredient(username, IngredientName).Result;
    }

    // Send recipe data from user to DB (& return recipe to the controller?)
    // Return bool to know if it worked. Test at the controller to know what to return to view.
    public static bool SetRecipe(string username, Recipe recipe)
    {
        //RecipeSeedData.AddRecipe(cookbookName, recipe);
        // Update to DB method

        bool test = DBQueryModel.CreateRecipeNode(username,
            recipe.Name,
            recipe.Description,
            recipe.Rating.ToString(),
            recipe.Difficulty.ToString(),
            recipe.numServings.ToString(),
            recipe.servingSize,
            recipe.CookTime.ToString(),
            recipe.PrepTime.ToString())
            .Result;

        //These aren't read as a success or failure for the program yet. Only success/fail of the recipe node is returned at present.
        foreach (string tag in recipe.Tags)
        {
            bool tmp = DBQueryModel.CreateTagNode(tag, recipe.Name, username).Result;
            if (tmp)
            {
                Console.WriteLine("Creating tag " + tag + " Succeeded");
            }
            else
            {
                Console.WriteLine("Creating tag " + tag + " Failed");
            }
        }

        foreach (string tool in recipe.Equipment)
        {
            bool tmp = DBQueryModel.CreateToolNode(username, recipe.Name, tool).Result;

            if (tmp)
            {
                Console.WriteLine("Creating tool " + tool + " Succeeded");
            }
            else
            {
                Console.WriteLine("Creating tool " + tool + " Failed");
            }
        }

        foreach (IngredientDetail ingredient in recipe.Ingredients)
        {
            Console.WriteLine("Passing ingredient " + ingredient.Name + " Succeeded");

            bool tmp = DBQueryModel.CreateIngredientNode(username, ingredient.Name).Result;

            if (tmp)
            {
                Console.WriteLine("Creating ingredient " + ingredient.Name + " Succeeded");
                tmp = DBQueryModel.ConnectIngredientNode(username, recipe.Name, ingredient.Name, ingredient.Unit, ingredient.Qualifier, ingredient.Quantity).Result;

                if (tmp)
                {
                    Console.WriteLine("Connecting ingredient " + ingredient.Name + " Succeeded");
                }
                else
                {
                    Console.WriteLine("Connecting ingredient " + ingredient.Name + " Succeeded");
                }
            }
            else
            {
                Console.WriteLine("Creating ingredient " + ingredient.Name + " Failed");
            }
        }
        int i = 0;
        foreach (string instruction in recipe.Instructions)
        {
            i += 1;
            bool tmp = DBQueryModel.CreateStepNode(username, recipe.Name, i.ToString(), instruction).Result;
            if (tmp)
            {
                Console.WriteLine("Creating instruction " + instruction + " Succeeded");
            }
            else
            {
                Console.WriteLine("Creating instruction " + instruction + " Failed");
            }
        }
        return test;
    }

    public static bool SetIngredient(string username, IngredientDetail ingD, string recipeName)
    {
        if (DBQueryModel.CreateIngredientNode(username, ingD.Ingredient.Name).Result)
        {
            return DBQueryModel.ConnectIngredientNode(username, recipeName, ingD.Name, ingD.Unit, ingD.Qualifier, ingD.Quantity).Result;
        }
        else
        {
            return false;
        }
    }

    /* Returns an alphabetized list of all ingredients a user has */
    public static List<string> GetIngredients(string username)
    {
        List<string> ingredients = DBQueryModel.GetIngredientNodeNames(username).Result; //["Oranges", "Apples", "Bananas", "Pears", "Tomatoes", "Spinach", "Sausage"];
        ingredients.Sort();
        return ingredients;
    }

    // Retrieves all items in the shopping list
    public static List<IngredientDetail> GetShoppingListItems(string username)
    {
        return DBQueryModel.GetShoppingList(username).Result; //shoppingList.Items ?? new List<IngredientDetail>();
    }

    // Adds an ingredient to the shopping list
    public static bool AddItemToShoppingList(IngredientDetail ingredient)
    {
        if (ingredient == null) return false;

        if (shoppingList.Items == null)
        {
            shoppingList.Items = new List<IngredientDetail>();
        }

        if (!shoppingList.Items.Contains(ingredient))
        {
            shoppingList.Items.Add(ingredient);
            Console.WriteLine($"{ingredient.Name} added to the shopping list.");
            return true;
        }
        else
        {
            Console.WriteLine($"{ingredient.Name} is already in the shopping list.");
            return false;
        }
    }

    // Removes an ingredient from the shopping list
    public static void RemoveItemFromShoppingList(IngredientDetail ingredient)
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
    //
    public static void CheckItemOffShoppingList(IngredientDetail ingredient)
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
    public static IngredientDetail? GetIngredientByNameFromShoppingList(string ingName, string username)
    {
        //var ingredient = shoppingList.Items?.FirstOrDefault(i => i.Name?.Equals(name, StringComparison.OrdinalIgnoreCase) ?? false)
        //            ?? pantryItems.FirstOrDefault(i => i.Name?.Equals(name, StringComparison.OrdinalIgnoreCase) ?? false);
        //return ingredient;

        try
        {
            return DBQueryModel.GetShoppingListIngredient(username, ingName).Result;
        }
        catch (Exception e)
        {
            return new IngredientDetail();
        }
    }

    // Retrieve all pantry items
    public static List<IngredientDetail> GetPantryItems(string username)
    {
        //AddItemToPantry(SeedData.Butter);
        //AddItemToPantry(SeedData.BakingSoda);
        return DBQueryModel.GetPantry(username).Result; //pantryItems;
    }

    // Add an ingredient to the pantry
    public static bool AddItemToPantry(IngredientDetail ingredient, string username)
    {
        //if (ingredient == null || string.IsNullOrEmpty(ingredient.Name))
        //{
        //    Console.WriteLine("Cannot add ingredient without a name.");
        //    return;
        //}

        //if (!pantryItems.Any(i => i.Name == ingredient.Name))
        //{
        //    pantryItems.Add(ingredient);
        //    Console.WriteLine($"{ingredient.Name} added to the pantry.");
        //}
        //else
        // {
        //    Console.WriteLine($"{ingredient.Name} is already in the pantry.");
        //}

        try
        {
            if (!DBQueryModel.AddToPantry(username, "Pantry", ingredient.Name, ingredient.Unit, ingredient.Qualifier, ingredient.Quantity).Result)
            {
                throw new Exception("DBQueryModel.AddToPantry returned false.");
            }
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine("Item could not be added to pantry. Exception: " + e);
            return false;
        }
    }

    // Remove an ingredient from the pantry
    public static bool RemoveItemFromPantry(string ingredientName, string username)
    {
        //if (ingredient == null || !pantryItems.Contains(ingredient))
        // {
        //    Console.WriteLine("Ingredient not found in the pantry.");
        //    return;
        //}
        try
        {
            if (!DBQueryModel.RemoveFromPantry(username, ingredientName).Result)
            {
                throw new Exception("RemoveFromPantry returned False");
            }
            Console.WriteLine($"{ingredientName} removed from the pantry.");
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine("Pantry Item could not be removed. Exception: " + e);
            return false;
        }
        //pantryItems.Remove(ingredient);

    }

    public static void EditItemInPantry(IngredientDetail itemToEdit, string userName)
    {

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

    public static Dictionary<string, List<IngredientDetail>> GetABCListDict(List<IngredientDetail> myList)
    {
        List<IngredientDetail> ABCList = myList.OrderBy(x => x.Name).ToList();
        Dictionary<string, List<IngredientDetail>> myDictionary = new Dictionary<string, List<IngredientDetail>>();
        foreach (IngredientDetail item in ABCList)
        {
            string firstLetter = item.Name[0].ToString().ToUpper();

            if (!myDictionary.ContainsKey(firstLetter))
            {
                myDictionary.Add(firstLetter, new List<IngredientDetail>());
            }
            myDictionary[firstLetter].Add(item);
        }
        return myDictionary;
    }


    /* Returns list of all user's saved meals */
    public static List<MealSet> getMeals(string username)
    {
        List<MealSet> mealSet = DBQueryModel.GetMeals(username).Result;
        //SeedData.meals;
        return mealSet;
    }

    public static MealSet getMeal(string mealName, string username)
    {
        return DBQueryModel.GetMeal(username, mealName).Result; //SeedData.getMeal(mealName);
    }

    // Placeholder method to simulate saving MealPlanner to Neo4j
    public static void SaveMealPlannerToNeo4j(MealPlanner mealPlanner, string username)
    {
        try
        {
            // Placeholder logic for saving data to Neo4j
            //Console.WriteLine($"Simulating saving meal planner with date: {mealPlanner.DateString}");
            foreach (MealSet meal in mealPlanner.ScheduledMeals)
            {
                int i = 0;
                foreach (Recipe recipe in meal.Recipes)
                {
                    if (!DBQueryModel.ScheduleRecipe(username, recipe.Name, meal.Name, meal.Date.ToString(), i).Result)
                    {
                        i++;
                        throw new Exception("Recipe could not be added. Return of false from DBQueryModel.ScheduleRecipe()");
                    }

                }
                //Console.WriteLine($"Simulating saving meal: {meal.Name}, Description: {meal.Description}");
            }
            Console.WriteLine("Meal Planner Saved");
        }
        catch (Exception e)
        {
            Console.WriteLine("Meal Planner could not be saved. Exception: " + e);
        }

    }


    // Placeholder method to simulate getting all meals
    public static List<MealSet> GetAllMeals(string username)
    {
        //This behavior is duplicated, so I'm just redirecting it to where the behavior is done.
        //This is preferable in case this method name is still being used.
        return getMeals(username);
        // Placeholder logic - return a list of simulated meals
        //Console.WriteLine("Simulating retrieval of all meals...");

        //return new List<MealSet>
        //{
        //    new MealSet { Name = "Cookies", Description = "Description for Placeholder Meal 1" },
        //    new MealSet { Name = "Chocolate Cookies", Description = "Description for Placeholder Meal 2" }
        //};
    }


    // Placeholder method to simulate retrieving meals for a specific date
    public static List<MealSet> getMealsForDate(DateOnly date, string username)
    {
        //Console.WriteLine($"Simulating retrieval of meals for date: {date}");

        // Simulate unique data for different dates for testing
        return DBQueryModel.GetMealPlanByDay(username, date.ToString()).Result;
    }

    // Get MealPlanner Monthly Data
    public static MPMonth getMealsForMonth(DateOnly date, string username)
    {
        Console.WriteLine("Starting CtrlModel.getMealsForMonth method for {0}", date);
        // Get the date range for the current month
        var (startOfMonth, endOfMonth) = DateHelper.GetDateRangeForCurrentMonth();
        Console.WriteLine("Got Date Range for Month. Start: {0} End: {1}", startOfMonth, endOfMonth);

        // Get first day of last week of month
        var startOfLastWeekOfMonth = DateHelper.GetStartOfWeek(endOfMonth);
        Console.WriteLine("Start of Last week of month: {0}", startOfLastWeekOfMonth);

        // Get first day of first week of month
        var startOfFirstWeekOfMonth = DateHelper.GetStartOfWeek(startOfMonth);
        Console.WriteLine("Start of First week of month: {0}", startOfFirstWeekOfMonth);

        // Variable to track current day working with
        DateOnly day = startOfFirstWeekOfMonth;

        // Variable for Month data
        MPMonth month = new MPMonth();
        Console.WriteLine("Ready to begin loops to populate month data");

        // Loop through each week, new week
        for (DateOnly startOfWeek = startOfFirstWeekOfMonth; startOfWeek <= startOfLastWeekOfMonth; startOfWeek = startOfWeek.AddDays(7))
        {
            MPWeek currentWeek = new MPWeek();
            Console.WriteLine("Created new Week");
            // Loop through days of week, populating day data
            for (int i = 0; i < 7; i++)
            {
                Console.WriteLine("Creating new day for {0}", day);
                // Get data for day
                MPDay currentDay = new MPDay
                {
                    Date = day,
                    Meals = CtrlModel.getMealsForDate(day, username).Select(meal => new MPMeal
                    {
                        mealDescription = meal.Description,
                        recipes = meal.Recipes
                    }).ToList()
                };
                Console.WriteLine("Data for new day obtained");
                // Add day to week
                currentWeek.Days.Add(currentDay);
                Console.WriteLine("Day added to current week");
                // Increment day to next date
                day = day.AddDays(1);
                Console.WriteLine("Ready for next day: {0}", day);
            }
            // Add week to month
            month.weeks.Add(currentWeek);
            Console.WriteLine("Added new week");
        }
        Console.WriteLine("Returning Month Data");
        return month;
    }

    public static void MoveItemFromShoppingListToPantry(IngredientDetail ingredient, string username)
    {
        //if (ingredient == null || string.IsNullOrEmpty(ingredient.Name))
        //{
        //    Console.WriteLine("Cannot add ingredient without a name.");
        //    return;
        //}

        //if (!pantryItems.Any(i => i.Name == ingredient.Name))
        //pantryItems.Add(ingredient);
        try
        {
            if (!DBQueryModel.RemoveFromShoppingList(username, ingredient.Name).Result)
            {
                throw new Exception("DBQueryModel.RemoveFromShoppingList returned false");
            }
            if (!DBQueryModel.AddToPantry(username, "Pantry", ingredient.Name, ingredient.Unit, ingredient.Qualifier, ingredient.Quantity).Result)
            {
                throw new Exception("DBQueryModel.AddToPantry returned false");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Item could not be moved to pantry from shopping list. Exception: " + e);
        }

    }


    public static bool MoveItemBetweenLists(string username, string itemName, bool toPantry)
    {
        IngredientDetail ingredient = new IngredientDetail();//GetIngredientByName(itemName);

        if (toPantry)
        {
            ingredient = GetIngredientDetail(username, itemName, "", "ShoppingList");
            if (ingredient == null) return false;
            RemoveItemFromShoppingList(ingredient);
            AddItemToPantry(ingredient, username);
        }
        else
        {
            ingredient = GetIngredientDetail(username, itemName, "", "Pantry");
            if (ingredient == null) return false;
            //RemoveItemFromPantry(ingredient);
            AddItemToShoppingList(ingredient);
        }
        return true;
    }

    public static bool RemoveFromCookbook(string username, string cookbookName, string recipeName)
    {
        try
        {
            if (!DBQueryModel.RemoveFromCookbook(username, cookbookName, recipeName).Result)
            {
                throw new Exception("DBQueryModel.AddToCookbook returned false");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Recipe could not be removed from the cookbook. Exception: " + e);
            return false;
        }
        return true;
    }

    public static bool AddToCookbook(string username, string cookbookName, string recipeName)
    {
        try
        {
            if (!DBQueryModel.AddToCookbook(username, cookbookName, recipeName).Result)
            {
                throw new Exception("DBQueryModel.AddToCookbook returned false");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Recipe could not be added to cookbook. Exception " + e);
        }
        return true;
    }

    public static IngredientDetail? GetIngredientDetailFromShoppingList(IngredientDetail ingredientDetail, string username)
    {
        // Assuming you already have a method to retrieve the shopping list for the user
        var shoppingListItems = GetShoppingListItems(username); // Retrieve the shopping list for the user

        // Search for an ingredient that matches on Name, Quantity, and Unit
        return shoppingListItems.FirstOrDefault(item =>
            item.Name == ingredientDetail.Name &&
            item.Quantity == ingredientDetail.Quantity &&
            item.Unit == ingredientDetail.Unit
        );
    }

    public static void updateIngredientDetailName(IngredientDetail detail)
    {
        detail.Name = detail.Ingredient.Name;
    }

    public static bool CreateCookbook(string username, Cookbook cb)
    {
        try
        {
            if (DBQueryModel.CreateCookbookNode(username, cb.Title, cb.Description).Result)
            {
                try
                {
                    foreach (string rName in cb.RecipeNames)
                    {
                        if (!DBQueryModel.AddToCookbook(username, cb.Title, rName).Result)
                        {
                            Console.WriteLine("Adding " + rName + " to recipe failed.");
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Cookbook created, but recipes couldn't be added. Exception: " + e);
                }
            }
            else
            {
                return false;
            }
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine("Error at CtrlModel.CreateCookbook. Exception: " + e);
            return false;
        }
    }

    public static async void ResetTesterAccount(string userName)
    {
        // Check if user exists - if yes, clear data

        if (DBQueryModel.CheckUserExistence(userName).Result)
        {
            //Delete user data
            SeedData.UnSeedDatabase(userName);
        }
        // Otherwise Create User with given username & password of abc123, tester@test.com, 1234567890
        else
        {
            //Create User
            await DBQueryModel.CreateUserNode(username: userName, name: userName, email: "tester@test.com", phone: "1234567890", password: "abc123");
        }

        // Call function to populate user data with preset data
        SeedData.SeedDatabase(userName);
        return;
    }

    public static bool CreateMeal(string username, MealSet meal)
    {
        try
        {
            if (!DBQueryModel.CreateMealNode(username, meal).Result)
            {
                return false;
            }
            foreach (string recipe in meal.RecipeNames)
            {
                if (!DBQueryModel.ConnectMealNode(username, meal.Name, recipe).Result)
                {
                    return false;
                }
            }
            return true;
        }
        catch
        {
            Console.WriteLine("Failed to create meal");
            return false;
        }
    }

    public static bool RemoveFromMeal(string username, string mealName, string recipeName)
    {
        try
        {
            if (!DBQueryModel.RemoveFromMeal(username, recipeName, mealName).Result)
            {
                throw new Exception("DBQueryModel.RemoveFromMeal returned false");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Recipe could not be removed from the meal. Exception: " + e);
            return false;
        }
        return true;
    }

    public static bool AddToMeal(string username, string mealName, string recipeName)
    {
        try
        {
            if (!DBQueryModel.RemoveFromMeal(username, recipeName, mealName).Result)
            {
                throw new Exception("DBQueryModel.AddToMeal returned false");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Recipe could not be added tp the meal. Exception: " + e);
            return false;
        }
        return true;
    }

    public static bool RemoveMeal(string username, string mealName)
    {
        try
        {
            if (!DBQueryModel.DeleteMeal(username, mealName).Result)
            {
                throw new Exception("DBQueryModel.DeleteMeal returned false");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Meal could not be removed. Exception: " + e);
            return false;
        }
        return true;
    }

}
