using System;
using System.Net.NetworkInformation;

namespace RecipeBuilder.Models;

public static class SeedData
{
    /* SAMPLE DATA */ 

    /* INGREDIENTS */
    private static Ingredient Sugar = new Ingredient{Name = "Sugar", Description="White granulated sugar", Unit="Cups"};
    private static Ingredient Egg = new Ingredient{Name = "Egg", Description="Large White Chicken Eggs", Unit="Dozen"};
    private static Ingredient BrownSugar = new Ingredient{Name = "Brown Sugar", Description="Light brown sugar", Unit="Cups"};
    private static Ingredient PowderedSugar = new Ingredient{Name = "Powdered Sugar", Description="Powdered sugar", Unit="Cups"};
    private static Ingredient Flour = new Ingredient{Name = "Flour", Description="Bleached all-purpose flour", Unit="Cups"};
    private static Ingredient Vanilla = new Ingredient{Name = "Vanilla", Description="Pure vanilla extract", Unit="tsp"};
    private static Ingredient BakingSoda = new Ingredient{Name = "Baking Soda", Description="Sodium Bicarbonate", Unit="tsp"};
    private static Ingredient BakingPowder = new Ingredient{Name = "Baking Powder", Description="Baking powder", Unit="tsp"};
    private static Ingredient Salt = new Ingredient{Name = "Salt", Description="Sodium Chloride", Unit="tsp"};
    private static Ingredient Butter = new Ingredient{Name = "Butter", Description="Butter", Unit="sticks"};
    private static Ingredient Oil = new Ingredient{Name = "Oil", Description="Neutral cooking oil: canola, sunflower, peanut, avocado, vegetable, etc.", Unit="tsp"};
    private static Ingredient ChocolateChips = new Ingredient{Name = "Chocolate Chips", Description="Semi-Sweet Chocolate Chips", Unit="Cups"};
    private static Ingredient SweetPotato = new Ingredient{Name="Sweet Potato", Description="", Unit=""};
    // private static Ingredient Lentils = new Ingredient{Name="Lentils", Description="Brown or Green", Unit=""};
    // private static Ingredient Name = new Ingredient{Name="", Description="", Unit=""};
    //private static Ingredient Name = new Ingredient{Name="", Description="", Unit=""};
    
    /* INGREDIENT LIST */
    private static List<Ingredient> myIngredients = [Sugar, Egg, BrownSugar, PowderedSugar, Flour, Vanilla, BakingSoda, BakingPowder, Butter, Oil, ChocolateChips];
    
    /* RECIPES */
    private static Recipe ChocolateChipCookies = new Recipe{
        Name="Nestle Toll House Chocolate Chip Cookies", Description="Best chocolate chip cookies!", 
        Difficulty=1, Rating=5, PrepTime=0, CookTime=0, 
        servingSize = {{"Cup", 1}}, numServings=12, 
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

    private static Recipe SugarCookies = new Recipe{
        Name="Sugar Cookies", Description="", 
        Difficulty=0, Rating=0, PrepTime=0, CookTime=0, 
        servingSize = {{"Cookie", 1}}, numServings=48, 
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
    // private static Recipe Name = new Recipe{
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
    private static List<Recipe> myRecipes = [ChocolateChipCookies, SugarCookies];
        
    /* COOKBOOKS */
    private static Cookbook CookiesCookbook = new Cookbook{
        Title = "Cookies", Description ="Cookie Recipes",
        Recipes =[ChocolateChipCookies, SugarCookies]
    };
    private static Cookbook DessertsCookbook = new Cookbook{
        Title = "Desserts", Description ="Dessert Recipes",
        Recipes =[ChocolateChipCookies, SugarCookies]
    };
    private static Cookbook ChocolateCookbook = new Cookbook{
        Title = "Chocolate Cookbook", Description ="Recipes with Chocolate",
        Recipes =[ChocolateChipCookies]
    };
    /* COOKBOOK LIST */
    private static List<Cookbook> myCookbooks = [CookiesCookbook,DessertsCookbook,ChocolateCookbook];
    /* SHOPPING LIST DATA */
    /* PANTRY DATA */
    /* MEALS DATA */
    private static List<MealSet> meals = [cookieMeal, cookieMeal];
    private static MealSet cookieMeal = new MealSet{
        Name = "Cookies",
        Description = "Cookie Monster's Favorite Meal.",
        Recipes = [ChocolateChipCookies, SugarCookies]
        };
        
    /* USER DATA */
    // private static ;

    /* GET METHODS */ 
    /* User Lists */
    public static List<Cookbook> GetCookbookList()
    {
        return myCookbooks;
    }

    public static List<Recipe> GetRecipeList()
    {
        return myRecipes;
    }

    public static List<Ingredient> GetIngredientList()
    {
        return myIngredients;
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

    


}