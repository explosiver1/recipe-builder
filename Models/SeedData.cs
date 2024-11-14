using System;
using System.Collections.Generic;
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
    public static Ingredient Steak = new Ingredient { Name = "Steak", Description = "beef"};
    public static Ingredient Broccoli = new Ingredient { Name = "Broccoli" };
    public static Ingredient Garlic = new Ingredient { Name = "Garlic" };
    public static Ingredient Worcestershire = new Ingredient { Name = "Worcestershire Sauce" };
    public static Ingredient Rosemary = new Ingredient { Name = "Rosemary" };
    public static Ingredient WhiteRiceCooked = new Ingredient { Name = "White Rice Cooked" };
    public static Ingredient PeasAndCarrots = new Ingredient { Name = "Peas And Carrots" };
    public static Ingredient MincedGinger = new Ingredient { Name = "Minced Ginger" };
    public static Ingredient SoySauce = new Ingredient { Name = "Soy Sauce" };
    public static Ingredient BlackSoySauce = new Ingredient { Name = "Black Soy Sauce" };
    public static Ingredient Mirin = new Ingredient { Name = "Mirin" };
    public static Ingredient Potatoes = new Ingredient { Name = "Potatoes" };
    public static Ingredient Milk = new Ingredient { Name = "Milk" };
    public static Ingredient BlackPepper = new Ingredient { Name = "Black Pepper" };
    public static Ingredient Gravy = new Ingredient { Name = "Gravy" };
    public static Ingredient BeefRoast = new Ingredient { Name = "Beef Roast" };
    public static Ingredient Onion = new Ingredient { Name = "Onion" };

    /* INGREDIENT LIST */
    public static List<Ingredient> myIngredients = new List<Ingredient> { Sugar, Egg, BrownSugar, PowderedSugar, Flour, Vanilla, BakingSoda, BakingPowder, Butter, Oil, 
        ChocolateChips, SweetPotato, Steak, Broccoli, Garlic, Worcestershire, Rosemary,WhiteRiceCooked, PeasAndCarrots,MincedGinger,SoySauce,BlackSoySauce,Mirin,Potatoes,Milk,BlackPepper,Gravy,BeefRoast, Onion};
    /* RECIPES */

    public static Recipe FriedRice = new Recipe
    {
        Name = "FriedRice",
        Description = "",
        Difficulty = 2,
        Rating = 4,
        PrepTime = 5,
        CookTime = 10,
        servingSize = "cup",
        numServings = 3,
        Tags = ["Entree", "Side", "Asian"],
        Equipment = ["Large Frying Pan or Wok"],
        Ingredients = [
            new IngredientDetail{Ingredient = WhiteRiceCooked, Name = "", Qualifier = "Day-old, semi-dry", Quantity = 1, Unit = "cup"},
            new IngredientDetail{Ingredient = Egg,Name = "Egg", Qualifier = "", Quantity = 2, Unit = ""},
            new IngredientDetail{Ingredient = Oil, Name = "Oil",Qualifier = "", Quantity = 2, Unit = "Tbsp"},
            new IngredientDetail{Ingredient = MincedGinger, Name = "Ginger",Qualifier = "minced", Quantity = 2, Unit = "Tbsp"},
            new IngredientDetail{Ingredient = Garlic,Name = "Garlic", Qualifier = "minced", Quantity = 1, Unit = "Tbsp"},
            new IngredientDetail{Ingredient = SoySauce, Name = "Soy Sauce",Qualifier = "Light, Tamari", Quantity = 3, Unit = "Tbsp"},
            new IngredientDetail{Ingredient = BlackSoySauce, Name = "Black or Dark Soy Sauce",Qualifier = "", Quantity = 1, Unit = "Tbsp"},
            new IngredientDetail{Ingredient = Mirin, Name = "Mirin",Qualifier = "", Quantity = 3, Unit = "Tbsp"},
            new IngredientDetail{Ingredient = Onion, Name = "Onion",Qualifier = "frozen & chopped", Quantity = 0.5, Unit = "cup"},
            new IngredientDetail{Ingredient = PeasAndCarrots, Name = "Peas and Carrots",Qualifier = "frozen", Quantity = 0.5, Unit = "cup" }
        ],
        Instructions = [
            "Gather all ingredients within reach of stove, open all bottles, etc. Measurements are approximate. Once started, this goes fast.",
            "Break rice up (avoid big clumps)",
            "Crack eggs into a bowl.",
            "Put frying pan over medium high to high heat.",
            "Add oil.",
            "During all remaining steps, constantly move the ingredients around the pan (stir fry) with a spatula. Lower heat if oil starts to spatter, otherwise keep heat high.",
            "Once oil shimmers, add onion.",
            "Once onion is soft, add garlic and ginger and stir for about 10-20 seconds.",
            "Add peas and carrots & stir until they soften & oil is hot again.",
            "Add rice to pan, add all sauces (soy sauces & mirin), working sauce into rice so it doesn't burn",
            "Add eggs & quickly scramble them in the pan as you mix them with everything else.",
            "Ensure rice is softened and flavored to your liking, adjust sauces as necessary.",
            "Serve as entree or as side for stir fry."
     ]
    };

    public static Recipe GrilledSteak = new Recipe
    {
        Name = "GrilledSteak",
        Description = "",
        Difficulty = 1,
        Rating = 5,
        PrepTime = 10,
        CookTime = 10,
        servingSize = "steaks",
        numServings = 2,
        Tags = ["Entree"],
        Equipment = ["George Foreman Grill", "Baking dish or Bowl"],
        Ingredients = [
            new IngredientDetail{Ingredient = Steak,Name = "Steak", Qualifier = "", Quantity = 2, Unit = "Steaks"},
            new IngredientDetail{Ingredient = Rosemary,Name = "Rosemary", Qualifier = "whole", Quantity = 1, Unit = "tsp"},
            new IngredientDetail{Ingredient = Garlic,Name = "Garlic", Qualifier = "minced", Quantity = 1, Unit = "Tbsp"},
            new IngredientDetail{Ingredient = Worcestershire,Name = "Worcestershire Sauce", Qualifier = "", Quantity = 1/2, Unit = "cup"},
    ],
        Instructions = [
            "Place steaks in a bowl or baking dish and add worcestershire, garlic, and rosemary.",
            "Cover & place in fridge to marinate for at least an hour.",
            "Cook steak on grill until done to your liking."
     ]
    }; 
    
    public static Recipe SteamedBroccoli = new Recipe
    {
        Name = "Steamed Broccoli",
        Description = "",
        Difficulty = 1,
        Rating = 3,
        PrepTime = 0,
        CookTime = 10,
        servingSize = "serving",
        numServings = 1,
        Tags = ["side"],
        Equipment = ["pot", "colander"],
        Ingredients = [
    new IngredientDetail{Ingredient = Broccoli, Name = "Broccoli", Qualifier = "florrets", Quantity = 1, Unit = "cup"},
     new IngredientDetail{Ingredient = Butter, Name="Butter", Qualifier = "", Quantity = 2, Unit = "Tbsp"},
     new IngredientDetail{Ingredient = Salt, Name="Salt", Qualifier = "", Quantity = 0.25, Unit = "tsp"},
            new IngredientDetail{Ingredient = BlackPepper, Name="Black Pepper", Qualifier = "", Quantity = 0.25, Unit = "tsp"}
    ],
        Instructions = [
    "Place broccoli in colander or steamer basket over a pot with a small amount of water.",
            "Bring water to a boil & steam broccoli until bright green (make sure the pot doesn't boil dry).",
            "Drain water from pot, add butter and broccoli to pot.",
            "Heat on low if needed to melt butter.",
            "Salt & pepper to taste."
     ]
    };

    public static Recipe ButteredPeasAndCarrots = new Recipe
    {
        Name = "Buttered Peas and Carrots",
        Description = "",
        Difficulty = 1,
        Rating = 3,
        PrepTime = 0,
        CookTime = 10,
        servingSize = "serving",
        numServings = 1,
        Tags = ["side"],
        Equipment = ["pot"],
        Ingredients = [
new IngredientDetail{Ingredient = PeasAndCarrots, Name = "Peas and Carrots", Qualifier = "frozen", Quantity = 1, Unit = "cup"},
     new IngredientDetail{Ingredient = Butter, Name="Butter", Qualifier = "", Quantity = 2, Unit = "Tbsp"},
     new IngredientDetail{Ingredient = Salt, Name="Salt", Qualifier = "", Quantity = 0.25, Unit = "tsp"}
],
        Instructions = [
            "Place peas and carrots into a small pot of water & bring to a boil.",
            "Cook until peas are bright and plump and carrots are soft.",
            "Drain water from pot, add butter to the peas and carrots in the pot.",
            "Heat on low if needed to melt butter.",
            "Salt to taste."
 ]
    };

    public static Recipe RoastedSweetPotatoes = new Recipe
    {
        Name = "RoastedSweetPotatoes",
        Description = "",
        Difficulty = 0,
        Rating = 0,
        PrepTime = 0,
        CookTime = 0,
        servingSize = "",
        numServings = 12,
        Tags = [""],
        Equipment = ["Air Fryer"],
        Ingredients = [
    new IngredientDetail{Ingredient = SweetPotato, Name = "Sweet Potatoes", Qualifier = "Cut Uniformly", Quantity = 1, Unit = "potato"},
     new IngredientDetail{Ingredient = Salt, Name = "Salt", Qualifier = "Sea Salt, fresh cracked", Quantity = 1, Unit = "tsp"},
            new IngredientDetail{Ingredient = Oil, Name = "Oil", Qualifier = "", Quantity = 1, Unit = "Tbsp"}
    ],
        Instructions = [
            "Wash & cut sweet potato.",
            "Preheat air fryer: 400F, 12min, turn reminder on.",
            "While preheating, toss sweet potatoes in oil.",
            "Once preheated, add sweet potatoes.",
            "Toss sweet potatoes half-way through cooking.",
            "Once done (there will be brown spots on potatoes if fully cooked), move to plate and salt to taste."
     ]
    }; 
    
    public static Recipe BakedSweetPotato = new Recipe
    {
        Name = "BakedSweetPotato",
        Description = "",
        Difficulty = 1,
        Rating = 2,
        PrepTime = 1,
        CookTime = 10,
        servingSize = "potato",
        numServings = 1,
        Tags = ["side"],
        Equipment = ["Air Fryer"],
        Ingredients = [
            new IngredientDetail{Ingredient = SweetPotato, Name = "Sweet Potatoes", Qualifier = "Whole, pierced", Quantity = 1, Unit = "potato"},
     new IngredientDetail{Ingredient = Butter, Name = "Butter", Qualifier = "", Quantity = 1, Unit = "Tbsp"},
            new IngredientDetail{Ingredient = BrownSugar, Name = "BrownSugar", Qualifier = "", Quantity = 1, Unit = "Tbsp"},
    ],
        Instructions = [
            "Preheat air fryer: 370F, 45min",
            "Wash potato & pierce several times with a fork.",
            "Air fry for 35-45 min until soft.",
            "Cut open, mash, add butter & brown sugar."
     ]
    };

    public static Recipe RoastBeefwGravy = new Recipe
    {
        Name = "Roast Beef with Gravy",
        Description = "",
        Difficulty = 1,
        Rating = 4,
        PrepTime = 5,
        CookTime = 120,
        servingSize = "Cup",
        numServings = 12,
        Tags = ["Dinner", "Comfort Food"],
        Equipment = ["Slow Cooker"],
        Ingredients = [
        new IngredientDetail{Ingredient = BeefRoast, Name = "Beef Roast", Qualifier = "", Quantity = 3, Unit = "lbs"},
        new IngredientDetail{Ingredient = Gravy, Name = "Gravy", Qualifier = "Heinz Beef or Mushroom", Quantity = 2, Unit = "Jars"}
    ],
        Instructions = [
            "Cover bottom of slowcooker with thin layer of water.",
             "Place roast in slowcooker.",
            "Cover & Cook on high for several hours until it easily falls apart",
            "Turn off slow cooker",
            "Move roast to a large plate or cutting board for shredding (allow to cool a bit first)",
            "Shred roast and return it to slow cooker",
            "Add jars of gravy",
            "Turn on slow cooker to heat until gravy is warm",
            "Serve with or over mashed potatoes"
     ]
    };

    public static Recipe MashedPotatoes = new Recipe
    {
        Name = "Mashed Potatoes",
        Description = "",
        Difficulty = 0,
        Rating = 5,
        PrepTime = 15,
        CookTime = 20,
        servingSize = "cups",
        numServings = 12,
        Tags = ["Side", "Comfort Food"],
        Equipment = ["Large Pot","Stove","Mixer", "Collinder"],
        Ingredients = [
        new IngredientDetail{Ingredient = Potatoes, Qualifier = "Red Skinned", Quantity = 5, Unit = "potatoes"},
        new IngredientDetail{Ingredient = Milk, Qualifier = "", Quantity = 3, Unit = "tbsp"},
        new IngredientDetail{Ingredient = Salt, Qualifier = "", Quantity = 2, Unit = "tsp"},
        new IngredientDetail{Ingredient = BlackPepper, Qualifier = "", Quantity = 2, Unit = "tsp"},
        new IngredientDetail{Ingredient = Butter, Qualifier = "", Quantity = 5, Unit = "tbsp"}
    ],
        Instructions = [
        "Wash & dice potatoes.",
        "Add potatoes to large pot of water.",
            "Bring to a boil & cook (reduce temperature as needed to avoid boiling over) until potatoes are soft (check with fork).",
            "Drain the potatoes & add them to the mixing bowl (metal or glass).",
            "Add the butter and start the mixer slowly.",
            "Add milk slowly until potatoes are smooth",
            "Salt & Pepper to taste."
     ]
    };

    public static Recipe ChocolateChipCookies = new Recipe
    {
        Name = "Nestle Toll House Chocolate Chip Cookies",
        Description = "Best chocolate chip cookies!",
        Difficulty = 1,
        Rating = 5,
        PrepTime = 0,
        CookTime = 0,
        servingSize = "Cookies",
        numServings = 12,
        Tags = ["Desserts", "Cookies", "Chocolate"],
        Equipment = ["Mixer", "Oven", "Cookie Sheet", "Cooling Rack"],
        Ingredients = [
            new IngredientDetail{Ingredient = Butter, Name = "Butter",  Qualifier = "Salted, Softened", Quantity = 2, Unit = "Sticks"},
            new IngredientDetail{Ingredient = Sugar, Name = "Sugar", Qualifier = "", Quantity = 0.75, Unit = "Cup"},
            new IngredientDetail{Ingredient = BrownSugar, Name = "Brown Sugar", Qualifier = "Packed", Quantity = 0.75, Unit = "Cup"},
            new IngredientDetail{Ingredient = Vanilla, Name = "Vanilla", Qualifier = "", Quantity = 1, Unit = "tsp"},
            new IngredientDetail{Ingredient = Egg, Name = "Egg", Qualifier = "Large", Quantity = 2, Unit = "Eggs"},
            new IngredientDetail{Ingredient = Flour, Name = "Flour", Qualifier = "All-Purpose", Quantity = 2.25, Unit = "Cup"},
            new IngredientDetail{Ingredient = Flour, Name = "Flour", Qualifier = "All-Purpose", Quantity = 2, Unit = "Tbsp"},
            new IngredientDetail{Ingredient = BakingSoda, Name = "Baking Soda", Qualifier = "", Quantity = 1, Unit = "tsp"},
            new IngredientDetail{Ingredient = Salt, Name = "Salt", Qualifier = "", Quantity = 1, Unit = "tsp"},
            new IngredientDetail{Ingredient = ChocolateChips, Name = "Chocolate Chips", Qualifier = "", Quantity = 2, Unit = "Cups"}
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
        servingSize = "Cookies",
        numServings = 48,
        Tags = ["Desserts", "Cookies"],
        Equipment = ["Mixer", "Oven", "Cookie Sheet", "Cooling Rack"],
        Ingredients = [
            new IngredientDetail{Ingredient = Flour, Name = "Flour", Qualifier = "All-Purpose", Quantity = 2.75, Unit = "Cups"},
            new IngredientDetail{Ingredient = BakingSoda, Name = "Baking Soda", Qualifier = "", Quantity = 1, Unit = "tsp"},
            new IngredientDetail{Ingredient = BakingPowder, Name = "Baking Powder",Qualifier = "", Quantity = 0.5, Unit = "tsp"},
            new IngredientDetail{Ingredient = Butter, Name = "Butter",  Qualifier = "Softened", Quantity = 1, Unit = "Cup"},
            new IngredientDetail{Ingredient = Sugar, Name = "Sugar", Qualifier = "", Quantity = 1.5, Unit = "Cups"},
            new IngredientDetail{Ingredient = Egg, Name = "Egg", Qualifier = "", Quantity = 1, Unit = "Egg"},
            new IngredientDetail{Ingredient = Vanilla, Name = "Vanilla", Qualifier = "", Quantity = 1, Unit = "tsp"}
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
    public static List<Recipe> myRecipes = [ChocolateChipCookies, SugarCookies, RoastBeefwGravy, MashedPotatoes, FriedRice, GrilledSteak, SteamedBroccoli, RoastedSweetPotatoes, BakedSweetPotato, ButteredPeasAndCarrots];


    /* COOKBOOKS */
    public static Cookbook Sides = new Cookbook()
    {
        Title = "Sides",
        Description = "Side Dishes",
        Recipes = [MashedPotatoes, FriedRice, SteamedBroccoli, RoastedSweetPotatoes, BakedSweetPotato, ButteredPeasAndCarrots],
        RecipeNames = [MashedPotatoes.Name, FriedRice.Name, SteamedBroccoli.Name, RoastedSweetPotatoes.Name, BakedSweetPotato.Name, ButteredPeasAndCarrots.Name]
    };
    public static Cookbook Entrees = new Cookbook()
    {
        Title = "Entrees",
        Description = "Main Dishes",
        Recipes = [RoastBeefwGravy, FriedRice, GrilledSteak],
        RecipeNames = [RoastBeefwGravy.Name, FriedRice.Name, GrilledSteak.Name]
    };
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
    public static List<Cookbook> myCookbooks = [CookiesCookbook, DessertsCookbook, ChocolateCookbook, Sides, Entrees];
    
    /* MEALS DATA */
    
    public static MealSet cookieMeal = new MealSet
    {
        Name = "Cookies",
        Description = "Cookie Monster's Favorite Meal.",
        Recipes = [ChocolateChipCookies, SugarCookies],
        RecipeNames = [ChocolateChipCookies.Name, SugarCookies.Name]
    };

    public static MealSet cookieMeal2 = new MealSet
    {
        Name = "Chocolate Cookies",
        Description = "Cookie Monster's Second Favorite Meal.",
        Recipes = [ChocolateChipCookies],
        RecipeNames = [ChocolateChipCookies.Name]
    };

    public static MealSet roastBeefMeal = new MealSet
    {
        Name = "Roast Beef & Gravy Dinner",
        Description = "Major Comfort Food",
        Recipes = [RoastBeefwGravy, MashedPotatoes, ButteredPeasAndCarrots],
        RecipeNames = [RoastBeefwGravy.Name, MashedPotatoes.Name, ButteredPeasAndCarrots.Name]
    };

    public static List<MealSet> meals = [
        new MealSet{
        Name = "Cookies",
        Description = "Cookie Monster's Favorite Meal.",
        Recipes = [ChocolateChipCookies, SugarCookies],
        RecipeNames = [ChocolateChipCookies.Name, SugarCookies.Name]
        },
        new MealSet{
        Name = "Chocolate Cookies",
        Description = "Cookie Monster's Second Favorite Meal.",
        Recipes = [ChocolateChipCookies],
        RecipeNames = [ChocolateChipCookies.Name]
        },
        roastBeefMeal
        ];
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
    
    /* PANTRY DATA */
    public static List<IngredientDetail> pantryItems = [
            new IngredientDetail{Ingredient = Flour, Name = "Flour", Qualifier = "All-Purpose", Quantity = 2.75, Unit = "Cups"},
            new IngredientDetail{Ingredient = BakingSoda, Name = "Baking Soda", Qualifier = "", Quantity = 1, Unit = "tsp"},
            new IngredientDetail{Ingredient = BakingPowder, Name = "Baking Powder",Qualifier = "", Quantity = 0.5, Unit = "tsp"},
            new IngredientDetail{Ingredient = Butter, Name = "Butter",  Qualifier = "Softened", Quantity = 1, Unit = "Cup"},
            new IngredientDetail{Ingredient = Sugar, Name = "Sugar", Qualifier = "", Quantity = 1.5, Unit = "Cups"},
            new IngredientDetail{Ingredient = Egg, Name = "Egg", Qualifier = "", Quantity = 1, Unit = "Egg"},
            new IngredientDetail{Ingredient = Vanilla, Name = "Vanilla", Qualifier = "", Quantity = 1, Unit = "tsp"}
        ];
    /* SHOPPING LIST DATA */
    public static List<IngredientDetail> shoppingListItems = [
            new IngredientDetail{Ingredient = Butter, Name = "Butter",  Qualifier = "Salted, Softened", Quantity = 2, Unit = "Sticks"},
            new IngredientDetail{Ingredient = Sugar, Name = "Sugar", Qualifier = "", Quantity = 0.75, Unit = "Cup"},
            new IngredientDetail{Ingredient = BrownSugar, Name = "Brown Sugar", Qualifier = "Packed", Quantity = 0.75, Unit = "Cup"},
            new IngredientDetail{Ingredient = Vanilla, Name = "Vanilla", Qualifier = "", Quantity = 1, Unit = "tsp"},
            new IngredientDetail{Ingredient = Egg, Name = "Egg", Qualifier = "Large", Quantity = 2, Unit = "Eggs"},
            new IngredientDetail{Ingredient = Flour, Name = "Flour", Qualifier = "All-Purpose", Quantity = 2.25, Unit = "Cup"},
            new IngredientDetail{Ingredient = Flour, Name = "Flour", Qualifier = "All-Purpose", Quantity = 2, Unit = "Tbsp"},
            new IngredientDetail{Ingredient = BakingSoda, Name = "Baking Soda", Qualifier = "", Quantity = 1, Unit = "tsp"},
            new IngredientDetail{Ingredient = Salt, Name = "Salt", Qualifier = "", Quantity = 1, Unit = "tsp"},
            new IngredientDetail{Ingredient = ChocolateChips, Name = "Chocolate Chips", Qualifier = "", Quantity = 2, Unit = "Cups"}
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
   
    public static MealPlanner SunMP = new MealPlanner { ScheduledMeals = [new MealSet { Recipes = [RoastBeefwGravy, MashedPotatoes] }, cookieMeal2] };
    public static MealPlanner MonMP = new MealPlanner { ScheduledMeals = [new MealSet { Recipes = [GrilledSteak, SteamedBroccoli, BakedSweetPotato] }, cookieMeal2] };
    public static MealPlanner TueMP = new MealPlanner { ScheduledMeals = [new MealSet { Recipes = [FriedRice] }, cookieMeal2] };
    public static MealPlanner WedMP = new MealPlanner { ScheduledMeals = [new MealSet { Recipes = [RoastBeefwGravy, MashedPotatoes] }] };
    public static MealPlanner ThuMP = new MealPlanner { ScheduledMeals = [new MealSet { Recipes = [GrilledSteak, ButteredPeasAndCarrots, BakedSweetPotato] }, cookieMeal2] };
    public static MealPlanner FriMP = new MealPlanner { ScheduledMeals = [new MealSet { Recipes = [RoastBeefwGravy, MashedPotatoes, ButteredPeasAndCarrots] }, cookieMeal] };
    public static MealPlanner SatMP = new MealPlanner { ScheduledMeals = [new MealSet { Recipes = [GrilledSteak, ButteredPeasAndCarrots, MashedPotatoes] }, cookieMeal, new MealSet { Recipes = [GrilledSteak, RoastedSweetPotatoes] }] };
    public static List<MealPlanner> GetMealsThisWeek()
    {
        (DateOnly WeekStart, DateOnly WeekEnd) = DateHelper.GetDateRangeForCurrentWeek();
        DateOnly date = WeekStart;
        List<MealPlanner> weekMealPlans = [SunMP,MonMP,TueMP,WedMP,ThuMP,FriMP,SatMP];
        foreach (var dayMP in weekMealPlans)
        {
            dayMP.Date = date;
            date = date.AddDays(1);
        }
        return weekMealPlans;
    }

    // Minimums for preset data:
    // Ten Recipes
    // Chocolate Chip Cookies must be in a cookbook, a meal, and a meal plan
    // Two Cookbooks
    // Two Meals
    // A week of meal plans
    // Three shopping list entries
    // Three Pantry entries
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
            foreach (MealPlanner day in GetMealsThisWeek())
            {
                CtrlModel.SaveMealPlannerToNeo4j(day, username);
            }

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
        catch
        {
            return false;
        }
        return true;
    }
    public static void TestMealsData()
    {
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("Meals Data Test");
        var meals = SeedData.getMeals();
        foreach (var meal in meals)
        {
            Console.WriteLine(meal.Name);
            foreach (var recipe in meal.Recipes)
            {
                Console.WriteLine();
                Console.WriteLine(recipe.Name);
                foreach (var ingredient in recipe.Ingredients)
                {
                    Console.Write(ingredient.Quantity + " " + ingredient.Unit + " " + ingredient.Name);
                    if (ingredient.Qualifier != "")
                    {
                        Console.WriteLine(" (" + ingredient.Qualifier + ")");
                    }
                    else { Console.WriteLine(); }

                }
            }
        }
    }

    public static void TestPantryData()
    {
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("Pantry Data Test");
        foreach (var ingredient in SeedData.GetPantryItems())
        {
            Console.WriteLine(ingredient.Quantity + " " + ingredient.Unit + " " + ingredient.Name);
        }
    }

    public static void TestShoppingListData()
    {
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("Shopping List Data Test");
        foreach (var ingredient in SeedData.GetShoppingListItems())
        {
            Console.WriteLine(ingredient.Quantity + " " + ingredient.Unit + " " + ingredient.Name);
        }
    }

    public static void TestMealPlannerData()
    {
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("Meal Planner Data Test");
        foreach (var dayMP in SeedData.GetMealsThisWeek())
        {
            Console.WriteLine("Date: " + dayMP.Date);
            int mealNum = 1;
            foreach (var meal in dayMP.ScheduledMeals)
            {
                Console.WriteLine("Meal " + mealNum + ": ");
                foreach (var recipe in meal.Recipes)
                {
                    Console.WriteLine(recipe.Name);
                }
                Console.WriteLine();
                mealNum++;
            }
            Console.WriteLine();
        }
    }

    public static void TestDataAccess()
    {
        TestMealsData();
        TestPantryData();
        TestShoppingListData();
        TestMealPlannerData();
    }
}
