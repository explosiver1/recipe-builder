using System;
using System.Net.NetworkInformation;

namespace RecipeBuilder.Models;

public static class SeedData
{
    /* SAMPLE DATA */

    /* INGREDIENTS */
    public static Ingredient Sugar = new Ingredient { Name = "Sugar", Description = "White granulated sugar"/*, Unit = "Cups"*/ };
    public static Ingredient Egg = new Ingredient { Name = "Egg", Description = "Large White Chicken Eggs"/* , Unit = "Dozen"*/ };
    public static Ingredient BrownSugar = new Ingredient { Name = "Brown Sugar", Description = "Light brown sugar"/* , Unit = "Cups"*/ };
    public static Ingredient PowderedSugar = new Ingredient { Name = "Powdered Sugar", Description = "Powdered sugar"/*, Unit = "Cups" */};
    public static Ingredient Flour = new Ingredient { Name = "Flour", Description = "Bleached all-purpose flour"/*, Unit = "Cups"*/ };
    public static Ingredient Vanilla = new Ingredient { Name = "Vanilla", Description = "Pure vanilla extract"/* , Unit = "tsp" */};
    public static Ingredient BakingSoda = new Ingredient { Name = "Baking Soda", Description = "Sodium Bicarbonate"/*, Unit = "tsp" */};
    public static Ingredient BakingPowder = new Ingredient { Name = "Baking Powder", Description = "Baking powder"/* , Unit = "tsp" */};
    public static Ingredient Salt = new Ingredient { Name = "Salt", Description = "Sodium Chloride"/* , Unit = "tsp" */};
    public static Ingredient Butter = new Ingredient { Name = "Butter", Description = "Butter"/* , Unit = "sticks" */};
    public static Ingredient Oil = new Ingredient { Name = "Oil", Description = "Neutral cooking oil: canola, sunflower, peanut, avocado, vegetable, etc."/* , Unit = "tsp"*/ };
    public static Ingredient ChocolateChips = new Ingredient { Name = "Chocolate Chips", Description = "Semi-Sweet Chocolate Chips"/* , Unit = "Cups"*/ };
    public static Ingredient SweetPotato = new Ingredient { Name = "Sweet Potato", Description = ""/* , Unit = ""*/ };
    // public static Ingredient Lentils = new Ingredient{Name="Lentils", Description="Brown or Green", Unit=""};
    // public static Ingredient Name = new Ingredient{Name="", Description="", Unit=""};
    //public static Ingredient Name = new Ingredient{Name="", Description="", Unit=""};

    /* INGREDIENT LIST */
    public static List<Ingredient> myIngredients = new List<Ingredient> { Sugar, Egg, BrownSugar, PowderedSugar, Flour, Vanilla, BakingSoda, BakingPowder, Butter, Oil, ChocolateChips };

    /* RECIPES */
    public static Recipe ChocolateChipCookies = new Recipe
    {
        Name = "Nestle Toll House Chocolate Chip Cookies",
        Description = "Best chocolate chip cookies!",
        Difficulty = 1,
        Rating = 5,
        PrepTime = 0,
        CookTime = 0,
        servingSize = "1Cup",
        numServings = 12,
        Tags = ["Desserts", "Cookies", "Chocolate"],
        Equipment = ["Mixer", "Oven", "Cookie Sheet", "Cooling Rack"],
        Ingredients = [
            new IngredientDetail{Ingredient = Butter, Qualifier = "Salted, Softened", Quantity = 2, Unit = "Sticks"},
            new IngredientDetail{Ingredient = Sugar, Qualifier = "", Quantity = 0.75, Unit = "Cup"},
            new IngredientDetail{Ingredient = BrownSugar, Qualifier = "Packed", Quantity = 0.75, Unit = "Cup"},
            new IngredientDetail{Ingredient = Vanilla, Qualifier = "", Quantity = 1, Unit = "tsp"},
            new IngredientDetail{Ingredient = Egg, Qualifier = "Large", Quantity = 2, Unit = "Eggs"},
            new IngredientDetail{Ingredient = Flour, Qualifier = "All-Purpose", Quantity = 2.25, Unit = "Cup"},
            new IngredientDetail{Ingredient = Flour, Qualifier = "All-Purpose", Quantity = 2, Unit = "Tbsp"},
            new IngredientDetail{Ingredient = BakingSoda, Qualifier = "", Quantity = 1, Unit = "tsp"},
            new IngredientDetail{Ingredient = Salt, Qualifier = "", Quantity = 1, Unit = "tsp"},
            new IngredientDetail{Ingredient = ChocolateChips, Qualifier = "", Quantity = 2, Unit = "Cups"}
        ],
        Instructions = [
            "Preheat the oven to 375 F.",
            "Cream together the butter & sugars with mixer.",
            "Add eggs, one at a time, mixing until thoroughly incorporated.",
            "Add vanilla, baking soda, & salt. Mix well.",
            "Slowly add flour, mixing until a soft dough forms.",
            "Mix in chocolate chips.",
            "Drop rounded tablespoons of dough onto an ungreased cookie sheet.",
            "Bake at 375 F for 7-9 minutes or until golden brown on the edges.",
            "Allow cookies to cool on the pan for 2 minutes before transferring to a wire cooling rack."
        ]
    };

    public static Recipe SugarCookies = new Recipe
    {
        Name = "Sugar Cookies",
        Description = "",
        Difficulty = 0,
        Rating = 0,
        PrepTime = 0,
        CookTime = 0,
        servingSize = "1Cookie",
        numServings = 48,
        Tags = ["Desserts", "Cookies"],
        Equipment = ["Mixer", "Oven", "Cookie Sheet", "Cooling Rack"],
        Ingredients = [
            new IngredientDetail{Ingredient = Flour, Qualifier = "All-Purpose", Quantity = 2.75, Unit = "Cups"},
            new IngredientDetail{Ingredient = BakingSoda, Qualifier = "", Quantity = 1, Unit = "tsp"},
            new IngredientDetail{Ingredient = BakingPowder, Qualifier = "", Quantity = 0.5, Unit = "tsp"},
            new IngredientDetail{Ingredient = Butter, Qualifier = "Softened", Quantity = 1, Unit = "Cup"},
            new IngredientDetail{Ingredient = Sugar, Qualifier = "", Quantity = 1.5, Unit = "Cups"},
            new IngredientDetail{Ingredient = Egg, Qualifier = "", Quantity = 1, Unit = "Egg"},
            new IngredientDetail{Ingredient = Vanilla, Qualifier = "", Quantity = 1, Unit = "tsp"}
        ],
        Instructions = [
            "Preheat the oven to 375 F.",
            "Stir flour, baking soda, & baking powder together in a small bowl.",
            "In a large bowl, beat together the butter & sugar with a mixer until smooth.",
            "Beat in egg & vanilla.",
            "Gradually blend in flour mixture.",
            "Roll dough into walnut-sized balls & place 2 inches apart onto an ungreased cookie sheet.",
            "Bake at 375 F for 8-10 minutes or until golden brown on the edges.",
            "Allow cookies to cool on the pan briefly before transferring to a wire cooling rack."
        ]
    };

    // public static Recipe Name = new Recipe{
    //     Name="", Description="",
    //     Difficulty=0, Rating=0, PrepTime=0, CookTime=0,
    //     servingSize = {{"Cup", 1}}, numServings=12,
    //     Tags = [""],
    //     Equipment = [""],
    //     Ingredients = [
    // new IngredientDetail{Ingredient = Name, Qualifier = "", Quantity = 0, Unit = ""},
    // new IngredientDetail{Ingredient = Name, Qualifier = "", Quantity = 0, Unit = ""}
    // ],
    //     Instructions = [
    // "",
    // ""
    //  ]
    // };
    /* RECIPE LIST */
    public static List<Recipe> myRecipes = [ChocolateChipCookies, SugarCookies];

    /* COOKBOOKS */
    public static Cookbook CookiesCookbook = new Cookbook
    {
        Title = "Cookies",
        Description = "Cookie Recipes",
        Recipes = [ChocolateChipCookies, SugarCookies],
        RecipeNames = [ChocolateChipCookies.Name, SugarCookies.Name]
    };
    public static Cookbook DessertsCookbook = new Cookbook
    {
        Title = "Desserts",
        Description = "Dessert Recipes",
        Recipes = [ChocolateChipCookies, SugarCookies],
        RecipeNames = [ChocolateChipCookies.Name, SugarCookies.Name]
    };
    public static Cookbook ChocolateCookbook = new Cookbook
    {
        Title = "Chocolate Cookbook",
        Description = "Recipes with Chocolate",
        Recipes = [ChocolateChipCookies],
        RecipeNames = [ChocolateChipCookies.Name]
    };
    /* COOKBOOK LIST */
    public static List<Cookbook> myCookbooks = [CookiesCookbook, DessertsCookbook, ChocolateCookbook];
    /* SHOPPING LIST DATA */
    /* PANTRY DATA */
    /* MEALS DATA */
    public static List<MealSet> meals = [
        new MealSet{
        Name = "Cookies",
        Description = "Cookie Monster's Favorite Meal.",
        Recipes = [ChocolateChipCookies, SugarCookies]
        },
        new MealSet{
        Name = "Chocolate Cookies",
        Description = "Cookie Monster's Second Favorite Meal.",
        Recipes = [ChocolateChipCookies]
        }];
    public static MealSet cookieMeal = new MealSet
    {
        Name = "Cookies",
        Description = "Cookie Monster's Favorite Meal.",
        Recipes = [ChocolateChipCookies, SugarCookies]
    };

    public static MealSet cookieMeal2 = new MealSet
    {
        Name = "Chocolate Cookies",
        Description = "Cookie Monster's Second Favorite Meal.",
        Recipes = [ChocolateChipCookies]
    };
    /* USER DATA */
    // public static ;

    /* GET METHODS */
    /* User Lists */
    public static List<Cookbook> GetCookbookList()
    {
        return myCookbooks;
    }

    public static List<Recipe> GetRecipeList()
    {
        foreach (Recipe recipe in myRecipes)
        {
            foreach (IngredientDetail ingredientDetail in recipe.Ingredients)
            {
                CtrlModel.updateIngredientDetailName(ingredientDetail);
            }
        }
        return myRecipes;
    }

    public static List<Ingredient> GetIngredientList()
    {
        return myIngredients;
    }

    public static List<IngredientDetail> GetIngredientDetailList()
    {
        List<IngredientDetail> ingredientDetails = new List<IngredientDetail>();
        foreach (Ingredient ingredient in myIngredients)
        {
            ingredientDetails.Add(new IngredientDetail { Name = ingredient.Name });
        }
        return ingredientDetails;
    }

    public static List<IngredientDetail> pantryItems = [
            new IngredientDetail{Ingredient = Flour, Qualifier = "All-Purpose", Quantity = 2.75, Unit = "Cups"},
            new IngredientDetail{Ingredient = BakingSoda, Qualifier = "", Quantity = 1, Unit = "tsp"},
            new IngredientDetail{Ingredient = BakingPowder, Qualifier = "", Quantity = 0.5, Unit = "tsp"},
            new IngredientDetail{Ingredient = Butter, Qualifier = "Softened", Quantity = 1, Unit = "Cup"},
            new IngredientDetail{Ingredient = Sugar, Qualifier = "", Quantity = 1.5, Unit = "Cups"},
            new IngredientDetail{Ingredient = Egg, Qualifier = "", Quantity = 1, Unit = "Egg"},
            new IngredientDetail{Ingredient = Vanilla, Qualifier = "", Quantity = 1, Unit = "tsp"}
        ];

    public static List<IngredientDetail> shoppingListItems = [
            new IngredientDetail{Ingredient = Butter, Qualifier = "Salted, Softened", Quantity = 2, Unit = "Sticks"},
            new IngredientDetail{Ingredient = Sugar, Qualifier = "", Quantity = 0.75, Unit = "Cup"},
            new IngredientDetail{Ingredient = BrownSugar, Qualifier = "Packed", Quantity = 0.75, Unit = "Cup"},
            new IngredientDetail{Ingredient = Vanilla, Qualifier = "", Quantity = 1, Unit = "tsp"},
            new IngredientDetail{Ingredient = Egg, Qualifier = "Large", Quantity = 2, Unit = "Eggs"},
            new IngredientDetail{Ingredient = Flour, Qualifier = "All-Purpose", Quantity = 2.25, Unit = "Cup"},
            new IngredientDetail{Ingredient = Flour, Qualifier = "All-Purpose", Quantity = 2, Unit = "Tbsp"},
            new IngredientDetail{Ingredient = BakingSoda, Qualifier = "", Quantity = 1, Unit = "tsp"},
            new IngredientDetail{Ingredient = Salt, Qualifier = "", Quantity = 1, Unit = "tsp"},
            new IngredientDetail{Ingredient = ChocolateChips, Qualifier = "", Quantity = 2, Unit = "Cups"}
        ];
    public static List<IngredientDetail> GetPantryItems()
    {
        foreach (var item in pantryItems)
        {
            CtrlModel.updateIngredientDetailName(item);
        }
        return pantryItems;
    }


    public static List<IngredientDetail> GetShoppingListItems()
    {
        foreach (var item in shoppingListItems)
        {
            CtrlModel.updateIngredientDetailName(item);
        }
        return shoppingListItems;
    }

    public static List<string> GetIngredientNameList()
    {
        List<string> ingredients = new List<string>();

        foreach (Ingredient ingredient in myIngredients)
        {
            ingredients.Add(ingredient.Name);
        }

        return ingredients;
    }

    public static List<Recipe> GetRecipesForIngredient(string IngredientName)
    {
        List<Recipe> recipesWithIngredient = new List<Recipe>();
        foreach (Recipe recipe in myRecipes)
        {
            foreach (IngredientDetail ingredient in recipe.Ingredients)
            {
                if (ingredient.Ingredient.Name == IngredientName)
                {
                    recipesWithIngredient.Add(recipe);
                    break;
                }
            }
        }
        return recipesWithIngredient;
    }

    /* Look up & return specific items */
    public static Recipe? GetRecipe(string RecipeName)
    {
        foreach (Recipe recipe in myRecipes)
        {
            if (recipe.Name == RecipeName)
            {
                return recipe;
            }
        }
        return null;
    }

    public static Cookbook? GetCookbook(string CookbookName)
    {
        foreach (Cookbook cookbook in myCookbooks)
        {
            if (cookbook.Title == CookbookName)
            {
                return cookbook;
            }
        }
        return null;
    }

    public static List<MealSet> getMeals()
    {
        return meals;
    }

    // public static MealSet? getMeal(string mealName)
    // {
    //     foreach (MealSet meal in meals)
    //     {
    //         if (meal.Name == mealName)
    //         {
    //             return meal;
    //         }
    //     }
    //     return null;
    // }

    public static MealSet getMeal(string mealName)
    {
        foreach (MealSet meal in meals)
        {
            if (meal.Name == mealName)
            {
                return meal;
            }
        }
        return new MealSet();
    }


    public static bool SeedDatabase(string username)
    {

        try
        {
            foreach (Recipe r in GetRecipeList())
            {
                if (!CtrlModel.SetRecipe(username, r))
                {
                    Console.WriteLine("Failed at seeding recipes");
                    return false;
                }
            }
            foreach (Cookbook c in GetCookbookList())
            {
                if (!CtrlModel.CreateCookbook(username, c))
                {
                    Console.WriteLine("Failed at seeding cookbooks");
                    return false;
                }
            }
            foreach (IngredientDetail ingD in GetPantryItems())
            {
                if (!CtrlModel.AddItemToPantry(ingD, username))
                {
                    Console.WriteLine("Failed at seeding pantry");
                    return false;
                }
            }

            foreach (MealSet m in getMeals())
            {
                if (!DBQueryModel.CreateMealNode(username, m).Result)
                {
                    Console.WriteLine("Failed at seeding meals");
                    return false;
                }
            }

            //Insert block for scheduling recipes onto meal planner.

            foreach (IngredientDetail ingD in GetShoppingListItems())
            {
                if (!DBQueryModel.AddToShoppingList(username, ingD).Result)
                {
                    Console.WriteLine("Failed at seeding shoppinglist");
                    return false;
                }
            }
        }
        catch (Exception e)
        {
            return false;
        }

        return true;
    }

    public static bool UnSeedDatabase(string username)
    {
        try
        {
            foreach (Recipe r in CtrlModel.GetRecipeList(username))
            {
                if (!DBQueryModel.DeleteRecipe(r.Name, username).Result)
                {
                    Console.WriteLine("Couldn't delete recipes");
                    return false;
                }
            }
            foreach (Cookbook c in CtrlModel.GetCookbookList(username))
            {
                if (!DBQueryModel.DeleteCookbook(c.Title, username).Result)
                {
                    Console.WriteLine("Couldn't delete cookbooks");
                    return false;
                }
            }
            foreach (IngredientDetail ing in CtrlModel.GetPantryItems(username))
            {
                if (!CtrlModel.RemoveItemFromPantry(ing.Name, username))
                {
                    Console.WriteLine("Couldn't delete ingredients from pantry");
                    return false;
                }
            }

            foreach (MealSet m in CtrlModel.GetAllMeals(username))
            {
                if (!DBQueryModel.DeleteMeal(username, m.Name).Result)
                {
                    {
                        Console.WriteLine("Failed at deleting meals");
                        return false;
                    }
                }

                //Insert block for unscheduling recipes onto meal planner.

                foreach (IngredientDetail ingD in CtrlModel.GetShoppingListItems(username))
                {
                    if (!DBQueryModel.RemoveFromShoppingList(username, ingD.Name).Result)
                    {
                        Console.WriteLine("Failed at deleting shoppinglist");
                        return false;
                    }
                }
            }
        }
        catch
        {
            return false;
        }
        return true;
    }

}
