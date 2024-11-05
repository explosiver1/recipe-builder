using System;

namespace RecipeBuilder.Models;

/* This class exists to provide a single class for all controllers to call on without the complexity of the DB model
Since multiple controllers need to call the same queries, this allows one method to be changed if needed 
rather than multiple */
public static class CtrlModel
{
    private static ShoppingList shoppingList = new ShoppingList();
    private static List<Ingredient> pantryItems = new List<Ingredient>();

    /* GET METHODS */

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
    public static bool AddItemToShoppingList(Ingredient ingredient)
    {
        if (ingredient == null) return false;
            
        if (shoppingList.Items == null)
        {
            shoppingList.Items = new List<Ingredient>();
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
        AddItemToPantry(SeedData.Butter);
        AddItemToPantry(SeedData.BakingSoda);
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

    /* Returns list of all user's saved meals */
    public static List<MealSet> getMeals()
    {
        List<MealSet> mealSet = SeedData.meals;
        return mealSet;
    }

    public static MealSet getMeal(string mealName)
    {
        return SeedData.getMeal(mealName);
    }

    // Placeholder method to simulate saving MealPlanner to Neo4j
    public static void SaveMealPlannerToNeo4j(MealPlanner mealPlanner)
    {
        // Placeholder logic for saving data to Neo4j
        Console.WriteLine($"Simulating saving meal planner with date: {mealPlanner.DateString}");
        foreach (var meal in mealPlanner.ScheduledMeals)
        {
            Console.WriteLine($"Simulating saving meal: {meal.Name}, Description: {meal.Description}");
        }
    }


     // Placeholder method to simulate getting all meals
    public static List<MealSet> GetAllMeals()
    {
        // Placeholder logic - return a list of simulated meals
        Console.WriteLine("Simulating retrieval of all meals...");

        return new List<MealSet>
        {
            new MealSet { Name = "Cookies", Description = "Description for Placeholder Meal 1" },
            new MealSet { Name = "Chocolate Cookies", Description = "Description for Placeholder Meal 2" }
        };
    }

    // Placeholder method to simulate retrieving meals from Neo4j
    public static List<MealSet> GetMealsFromNeo4j()
    {
        // Placeholder logic for retrieving data
        Console.WriteLine("Simulating retrieving meals from Neo4j...");

        // Return a simulated list of meals
        return new List<MealSet>
        {
            new MealSet { Name = "Cookies", Description = "Simulated Description 1" },
            new MealSet { Name = "Chocolate Cookies", Description = "Simulated Description 2" }
        };
    }

    // Placeholder method to simulate retrieving meals for a specific date
    public static List<MealSet> getMealsForDate(DateOnly date)
    {
        Console.WriteLine($"Simulating retrieval of meals for date: {date}");

        // Simulate unique data for different dates for testing
        return new List<MealSet>
        {
            new MealSet { Name = $"Meal for {date}", Description = $"Description for meal on {date}" }
        };
    }

    // Get MealPlanner Monthly Data
    public static MPMonth getMealsForMonth(DateOnly date)
    {
        Console.WriteLine("Starting CtrlModel.getMealsForMonth method for {0}", date);
        // Get the date range for the current month
        var (startOfMonth, endOfMonth) = DateHelper.GetDateRangeForCurrentMonth();
        Console.WriteLine("Got Date Range for Month. Start: {0} End: {1}", startOfMonth, endOfMonth);
        
        // Get first day of last week of month
        var startOfLastWeekOfMonth = DateHelper.GetStartOfWeek(endOfMonth);
        Console.WriteLine("Start of Last week of month: {0}",startOfLastWeekOfMonth);
        
        // Get first day of first week of month
        var startOfFirstWeekOfMonth = DateHelper.GetStartOfWeek(startOfMonth);
        Console.WriteLine("Start of First week of month: {0}",startOfFirstWeekOfMonth);

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
                    Meals = CtrlModel.getMealsForDate(day).Select(meal => new MPMeal
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
}
