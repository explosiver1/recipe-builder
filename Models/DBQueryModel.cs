namespace RecipeBuilder.Models;
using Neo4j.Driver;

public class DBQueryModel
{

    //private static IDriver driver;
    private static QueryConfig qConf;

    //Taken from the neo4j dotnet driver documentation
    //private static IDriver CreateDriverWithBasicAuth(string uri, string user, string password)
    //{
    //    return GraphDatabase.Driver(uri, AuthTokens.Basic(user, password));
    //}

    //static constructor for initialization.
    static DBQueryModel()
    {
        //Console.WriteLine("Creating Driver...");
        //Add an instantiation and closing of driver in every method.
        //I thought execute async would open close the session as a transaction, but it doesn't.
        //driver = GraphDatabase.Driver(ServerSettings.neo4jURI, AuthTokens.Basic(ServerSettings.dbUser, ServerSettings.dbPassword));
        qConf = new QueryConfig(database: "neo4j");
        //Console.WriteLine("Driver Created.");
    }

    //Optionals can be added as "type name = 'value'" in the argument list. Not specifying a default value makes it required.

    // CreateUser()
    public static async Task<bool> CreateUserNode(string username, string name, string email, string phone, string password)
    {
        using var driver = GraphDatabase.Driver(ServerSettings.neo4jURI, AuthTokens.Basic(ServerSettings.dbUser, ServerSettings.dbPassword));
        Console.WriteLine("Creating user...");
        var query = @"
            MERGE (u:User {username: $username})
            ON CREATE SET
                u.name = $name,
                u.email = $email,
                u.phone = $phone,
                u.password = $password
            RETURN COUNT(u) > 0
        ";

        // Opens a session for Neo4j
        var session = driver.AsyncSession();
        try
        {
            Console.WriteLine("Executing Query...");
            // Run the query with parameters and put results in var
            var response = await session.RunAsync(query, new { username, name, email, phone, password });

            // Pulls all responses from query
            IReadOnlyList<IRecord> records = await response.ToListAsync();

            // Checks if there is a record && gets the first record which should be the bool response
            bool userCreated = records.Any() && records.First()[0].As<bool>();
            Console.WriteLine("Returning Result: " + userCreated);
            if (userCreated)
            {
                bool pantryCreated = CreatePantryNode(username).Result;
                if (pantryCreated)
                {
                    bool shoppingListCreated = CreatePantryNode(username).Result;
                    if (shoppingListCreated)
                    {
                        return true;
                    }
                    Console.WriteLine("Failed at shopping list");
                }
                Console.WriteLine("Failed at pantry");
            }
            Console.WriteLine("Failed at user");
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return false;
        }
        finally
        {
            // Ensures the session is closed
            await session.CloseAsync();
        }
    }

    // CreateRecipe()
    // TODO - Test results
    public static async Task<bool> CreateRecipeNode(string username, string recipe, string description = "", string rating = "", string difficulty = "", string servings = "", string servingsize = "", string cookTime = "", string prepTime = "")
    {
        using var driver = GraphDatabase.Driver(ServerSettings.neo4jURI, AuthTokens.Basic(ServerSettings.dbUser, ServerSettings.dbPassword));
        Console.WriteLine("Entering CreateRecipeNode with parameters:\n" +
            "username: " + username + "\n" +
            "recipe: " + recipe + "\n" +
            "description: " + description + "\n" +
            "rating: " + rating + "\n" +
            "difficulty: " + difficulty + "\n" +
            "servings: " + servings + "\n" +
            "servingize: " + servingsize + "\n" +
            "cookTime: " + cookTime + "\n" +
            "prepTime: " + prepTime + "\n");

        description = string.IsNullOrEmpty(description) ? "" : description;
        rating = string.IsNullOrEmpty(rating) ? "" : rating;
        difficulty = string.IsNullOrEmpty(difficulty) ? "" : difficulty;
        servings = string.IsNullOrEmpty(servings) ? "" : servings;
        servingsize = string.IsNullOrEmpty(servingsize) ? "" : servingsize;
        cookTime = string.IsNullOrEmpty(cookTime) ? "" : cookTime;
        prepTime = string.IsNullOrEmpty(prepTime) ? "" : prepTime;


        var query = @"
            MATCH (user:User {username: $username})
            MERGE (recipe:Recipe {name: $recipeName})
            ON CREATE SET
                recipe.description = $description,
                recipe.rating = $rating,
                recipe.difficulty = $difficulty,
                recipe.servings = $servings,
                recipe.servingSize = $servingsize,
                recipe.cookTime = $cookTime,
                recipe.prepTime = $prepTime
            MERGE (user)-[x:OWNS]->(recipe)
            RETURN COUNT(x) > 0
        ";

        var recipeName = username + recipe;

        var session = driver.AsyncSession();
        try
        {
            var response = await session.RunAsync(query, new { username, recipeName, description, rating, difficulty, servings, servingsize, cookTime, prepTime });

            // Pulls all responses from the query
            IReadOnlyList<IRecord> records = await response.ToListAsync();

            // Checks if there is a record and gets the first record which should be the bool response
            bool recipeCreated = records.Any() && records.First()[0].As<bool>();
            Console.WriteLine($"Recipe {recipeName} created!");
            return recipeCreated;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return false;
        }
        finally
        {
            // Ensures the session is closed
            await session.CloseAsync();
        }
    }

    // MergeRecipe()
    // Used when adding a recipe to a cookbook or to a meal
    public static async Task<bool> ConnectRecipeNode(string username, string parent, string recipe)
    {
        using var driver = GraphDatabase.Driver(ServerSettings.neo4jURI, AuthTokens.Basic(ServerSettings.dbUser, ServerSettings.dbPassword));
        string query;

        if (parent.Contains(""))
        {
            query = @"
            test
            ";
        }
        else
        {
            query = @"
            test
            ";
        }

        var parentName = username + parent;
        var recipeName = username + recipe;

        var session = driver.AsyncSession();
        try
        {
            var response = await session.RunAsync(query, new { parentName, recipeName });
            Console.WriteLine($"Parent node {parentName} connected to {recipe}!");

            // Pulls all responses from query
            IReadOnlyList<IRecord> records = await response.ToListAsync();

            // Checks if there is a record && gets the first record which should be the bool response
            bool nodesConnected = records.Any() && records.First()[0].As<bool>();
            Console.WriteLine("Returning Result: " + nodesConnected);
            return nodesConnected;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return false;
        }
        finally
        {
            await session.CloseAsync();
        }
    }

    // Unsure how to handle tool and tag since it needs both new and old names passed to be able to delete the previous node
    public static async Task<bool> EditRecipe(string username, string recipe, string tool = "", string order = "", string stepDescription = "", string recipeDescription = "", string rating = "", string difficulty = "", string servings = "", string servingSize = "", string cookTime = "", string prepTime = "")
    {
        Console.WriteLine($"Entering DBQueryModel.EditRecipe with parameters: username: {username}, recipe: {recipe}, tool {tool}, order: {order}, stepDescription: {stepDescription}, recipeDescription {recipeDescription}, rating: {rating}, difficulty: {servings}, servingSize: {servingSize}, cookTime: {cookTime}, prepTime: {prepTime}");
        using var driver = GraphDatabase.Driver(ServerSettings.neo4jURI, AuthTokens.Basic(ServerSettings.dbUser, ServerSettings.dbPassword));
        string query = @"
            MATCH (recipe:Recipe{name:$recipeName})
            ";

        var recipeName = username + recipe;
        var toolName = username + tool;
        var parameters = new Dictionary<string, object> { { "recipeName", recipeName } };
        var updates = new List<string>();

        // Dynamically add SET clauses for non-empty parameters
        //if (!string.IsNullOrEmpty(stepDescription))
        //{
        //    updates.Add("step.description = $stepDescription");
        //    parameters["description"] = stepDescription;
        //}
        if (!string.IsNullOrEmpty(recipeDescription))
        {
            updates.Add("recipe.description = $recipeDescription");
            parameters["recipeDescription"] = recipeDescription;
        }
        if (!string.IsNullOrEmpty(rating))
        {
            updates.Add("recipe.rating = $rating");
            parameters["rating"] = rating;
        }
        if (!string.IsNullOrEmpty(difficulty))
        {
            updates.Add("recipe.difficulty = $difficulty");
            parameters["difficulty"] = difficulty;
        }
        if (!string.IsNullOrEmpty(servings))
        {
            updates.Add("recipe.servings = $servings");
            parameters["servings"] = servings;
        }
        if (!string.IsNullOrEmpty(servingSize))
        {
            updates.Add("recipe.servingSize = $servingSize");
            parameters["servingSize"] = servingSize;
        }
        if (!string.IsNullOrEmpty(cookTime))
        {
            updates.Add("recipe.cookTime = $cookTime");
            parameters["cookTime"] = cookTime;
        }
        if (!string.IsNullOrEmpty(prepTime))
        {
            updates.Add("recipe.prepTime = $prepTime");
            parameters["prepTime"] = prepTime;
        }

        // Join updates with commas and complete the query
        query += "SET " + string.Join(", ", updates) + " RETURN COUNT(recipe) > 0";

        var session = driver.AsyncSession();
        try
        {
            var response = await session.RunAsync(query, parameters);

            // Pulls all responses from query
            IReadOnlyList<IRecord> records = await response.ToListAsync();

            // Checks if there is a record && gets the first record which should be the bool response
            bool recipeEdited = records.Any() && records.First()[0].As<bool>();
            Console.WriteLine("Returning Result: " + recipeEdited);
            return recipeEdited;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return false;
        }
        finally
        {
            await session.CloseAsync();
        }
    }

    // GetRecipes()
    // Gets a list of all the recipe names a user owns
    // TODO - Add Group Parameter
    public static async Task<List<string>> GetRecipeNodeNames(string username)
    {
        using var driver = GraphDatabase.Driver(ServerSettings.neo4jURI, AuthTokens.Basic(ServerSettings.dbUser, ServerSettings.dbPassword));
        var query = @"
        MATCH (user:User {username: $username})-[:OWNS]->(recipe:Recipe)
        RETURN recipe.name AS recipeName
        ";

        var session = driver.AsyncSession();
        var recipeNames = new List<string>();

        try
        {
            var result = await session.RunAsync(query, new { username });

            await result.ForEachAsync(record =>
            {
                recipeNames.Add(GetCleanString(username, record["recipeName"].As<string>()));
            });

            Console.WriteLine($"Found {recipeNames.Count} recipes for user {username}!");
        }
        finally
        {
            await session.CloseAsync();
        }

        return recipeNames;
    }

    public static async Task<List<string>> GetMealNodeNames(string username)
    {
        using var driver = GraphDatabase.Driver(ServerSettings.neo4jURI, AuthTokens.Basic(ServerSettings.dbUser, ServerSettings.dbPassword));
        var query = @"
        MATCH (user:User {username: $username})-[:OWNS]->(m:Meal)
        RETURN m.name AS mealName
        ";

        var session = driver.AsyncSession();
        var mealNames = new List<string>();

        try
        {
            var result = await session.RunAsync(query, new { username });

            await result.ForEachAsync(record =>
            {
                mealNames.Add(GetCleanString(username, record["mealName"].As<string>()));
            });

            Console.WriteLine($"Found {mealNames.Count} meals for user {username}!");
        }
        finally
        {
            await session.CloseAsync();
        }

        return mealNames;
    }

    // CreateIngredient()
    public static async Task<bool> CreateIngredientNode(string username, string ingredient)
    {
        using var driver = GraphDatabase.Driver(ServerSettings.neo4jURI, AuthTokens.Basic(ServerSettings.dbUser, ServerSettings.dbPassword));
        Console.WriteLine("Entering CreateIngredientNode with parameters: " + username + ", " + ingredient);
        var query = @"
            MATCH (user:User {username: $username})
            MERGE (ingredient:Ingredient {name: $ingredientName})
            MERGE (user)-[x:OWNS]->(ingredient)
            RETURN (user IS NOT NULL) AS userExists,
                (ingredient IS NOT NULL) AS ingredientExists,
                EXISTS((user)-[:OWNS]->(ingredient)) AS relationshipCreated
        ";

        var ingredientName = username + ingredient;


        var session = driver.AsyncSession();

        try
        {
            Console.WriteLine("Executing query to create ingredient node and relationship...");

            // Run the query with parameters and retrieve a single response
            var response = await session.RunAsync(query, new { username, ingredientName });
            var record = await response.SingleAsync();

            // Check if user and ingredient nodes exist and are connected
            bool userExists = record["userExists"].As<bool>();
            bool ingredientExists = record["ingredientExists"].As<bool>();
            bool relationshipCreated = record["relationshipCreated"].As<bool>();

            // Log detailed results
            Console.WriteLine($"User exists: {userExists}, Ingredient exists: {ingredientExists}, Relationship created: {relationshipCreated}");

            // Return true if all conditions are met
            return userExists && ingredientExists && relationshipCreated;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return false;
        }
        finally
        {
            await session.CloseAsync();
        }
    }

    //This was easier than making the other method do more because of the parameter injection on the query string.
    public static async Task<List<string>> GetRecipeNodeNamesByIngredient(string username, string ingredient)
    {
        using var driver = GraphDatabase.Driver(ServerSettings.neo4jURI, AuthTokens.Basic(ServerSettings.dbUser, ServerSettings.dbPassword));
        ingredient = username + ingredient;
        var query = @"
            MATCH (user:User {username: $username})-[:OWNS]->(recipe:Recipe)-[:MADE_WITH]->(i:Ingredient {name: $ingredient})
            RETURN recipe.name AS recipeName
            ";

        var session = driver.AsyncSession();
        var recipeNames = new List<string>();

        try
        {
            var result = await session.RunAsync(query, new { username, ingredient });

            await result.ForEachAsync(record =>
            {
                recipeNames.Add(record["recipeName"].As<string>());
            });

            Console.WriteLine($"Found {recipeNames.Count} recipes for user {username}!");
        }
        finally
        {
            await session.CloseAsync();
        }

        return GetCleanList(username, recipeNames);
    }


    // MergeIngredient()
    // Used when adding ingredients to Recipe, Shopping List, or Pantry (must create the nodes first)
    // The parent is the name of the node you are adding ingredients too
    public static async Task<bool> ConnectIngredientNode(string username, string parent, string ingredient, string unit = "", string qualifier = "", double quantity = 0, string description = "")
    {
        using var driver = GraphDatabase.Driver(ServerSettings.neo4jURI, AuthTokens.Basic(ServerSettings.dbUser, ServerSettings.dbPassword));
        var parentName = username + parent;
        var ingredientName = username + ingredient;

        unit = string.IsNullOrEmpty(unit) ? "" : unit;
        qualifier = string.IsNullOrEmpty(qualifier) ? "" : qualifier;
        description = string.IsNullOrEmpty(description) ? "" : description;

        Console.WriteLine("unit: " + unit + "  qualifier: " + qualifier + "  quantity: " + quantity);

        var parameters = new Dictionary<string, object> { { "parentName", parentName }, { "ingredientName", ingredientName } };

        string query;
        if (parent.Contains("Pantry"))
        {
            Console.WriteLine("Connect Ingredinet Node to Pantry");
            query = @"
                MATCH (ingredient:Ingredient{name:$ingredientName})
                MERGE (parent:Pantry{name:$parentName})
                MERGE (parent)-[x:STORES]->(ingredient)
                SET x.unit = $unit,
                    x.qualifier = $qualifier,
                    x.quantity = $quantity,
                    x.description = $description
                RETURN COUNT(x) > 0
            ";

            parameters["unit"] = unit;
            parameters["qualifier"] = qualifier;
            parameters["quantity"] = quantity;
            parameters["description"] = description;
        }
        else if (parent.Contains("ShoppingList"))
        {
            Console.WriteLine("Connect Ingredinet Node to ShoppingList");
            query = @"
                MATCH (ingredient:Ingredient{name:$ingredientName})
                MERGE (parent:ShoppingList{name:$parentName})
                MERGE (parent)-[x:PLANS_TO_BUY]->(ingredient)
                SET x.unit = $unit,
                    x.qualifier = $qualifier,
                    x.quantity = $quantity,
                    x.checked = true
                RETURN COUNT(x) > 0
            ";

            parameters["unit"] = unit;
            parameters["qualifier"] = qualifier;
            parameters["quantity"] = quantity;
        }
        else
        {
            Console.WriteLine("Connect Ingredinet Node to Recipe");
            query = @"
                MATCH (parent:Recipe{name:$parentName})
                MATCH (ingredient:Ingredient{name:$ingredientName})
                MERGE (parent)-[x:MADE_WITH]->(ingredient)
                SET x.unit = $unit,
                    x.qualifier = $qualifier,
                    x.quantity = $quantity
                RETURN COUNT(x) > 0
            ";

            parameters["unit"] = unit;
            parameters["qualifier"] = qualifier;
            parameters["quantity"] = quantity;
        }

        var session = driver.AsyncSession();
        try
        {
            if (qualifier == null)
            {
                Console.WriteLine("Qualifier is Null!");
            }
            var response = await session.RunAsync(query, parameters);
            Console.WriteLine($"Parent node {parentName} connected to {ingredient}!");

            // Pulls all responses from query
            IReadOnlyList<IRecord> records = await response.ToListAsync();

            // Checks if there is a record && gets the first record which should be the bool response
            bool nodesConnected = records.First()[0].As<bool>(); //records.Any() && records.First()[0].As<bool>();
            Console.WriteLine("Returning Result: " + nodesConnected);
            return nodesConnected;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return false;
        }
        finally
        {
            await session.CloseAsync();
        }
    }

    // GetIngredients()
    // Gets a list of all the names of ingredients a user owns
    // Unsure if works with no ingredients
    public static async Task<List<string>> GetIngredientNodeNames(string username)
    {
        using var driver = GraphDatabase.Driver(ServerSettings.neo4jURI, AuthTokens.Basic(ServerSettings.dbUser, ServerSettings.dbPassword));
        var query = @"
        MATCH (user:User {username: $username})-[:OWNS]->(ingredient:Ingredient)
        RETURN ingredient.name AS ingredientName
        ";

        var session = driver.AsyncSession();
        var ingredientNames = new List<string>();

        try
        {
            var result = await session.RunAsync(query, new { username });

            await result.ForEachAsync(record =>
            {
                ingredientNames.Add(record["ingredientName"].As<string>());
            });

            Console.WriteLine($"Found {ingredientNames.Count} ingredients for user {username}!");
        }
        finally
        {
            await session.CloseAsync();
        }

        return GetCleanList(username, ingredientNames);
    }

    public static async Task<bool> CreatePantryNode(string username)
    {
        using var driver = GraphDatabase.Driver(ServerSettings.neo4jURI, AuthTokens.Basic(ServerSettings.dbUser, ServerSettings.dbPassword));
        Console.WriteLine("Creating pantry...");
        var query = @"
            MATCH (user:User {username: $username})
            MERGE (pantry:Pantry {name: $pantryName})
            MERGE (user)-[x:OWNS]->(pantry)
            RETURN COUNT(x) > 0
        ";

        var pantryName = username + "Pantry";

        var session = driver.AsyncSession();
        try
        {
            Console.WriteLine("Executing Query...");
            var response = await session.RunAsync(query, new { username, pantryName });
            IReadOnlyList<IRecord> records = await response.ToListAsync();

            bool pantryCreated = records.Any() && records.First()[0].As<bool>();
            Console.WriteLine("Returning Result: " + pantryCreated);
            return pantryCreated;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return false;
        }
        finally
        {
            await session.CloseAsync();
        }
    }

    // CreateCookbookNode()
    // TODO - Test no description input
    public static async Task<bool> CreateCookbookNode(string username, string name, string description = "")
    {
        using var driver = GraphDatabase.Driver(ServerSettings.neo4jURI, AuthTokens.Basic(ServerSettings.dbUser, ServerSettings.dbPassword));
        description = string.IsNullOrEmpty(description) ? "" : description;

        var query = @"
            MATCH (user:User{username: $username})
            MERGE (cookbook:Cookbook {name: $cookbookName})
            ON CREATE SET
                cookbook.description = $description
            MERGE (user)-[x:OWNS]->(cookbook)
            RETURN COUNT(x) > 0
        ";

        var cookbookName = username + name;

        var session = driver.AsyncSession();
        try
        {
            var response = await session.RunAsync(query, new { username, cookbookName, description });
            Console.WriteLine($"Cookbook node {cookbookName} created!");

            // Pulls all responses from query
            IReadOnlyList<IRecord> records = await response.ToListAsync();

            // Checks if there is a record && gets the first record which should be the bool response
            bool cookbookCreated = records.Any() && records.First()[0].As<bool>();
            Console.WriteLine("Returning Result: " + cookbookCreated);
            return cookbookCreated;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return false;
        }
        finally
        {
            await session.CloseAsync();
        }
    }

    // CreateTool()
    // TODO - Test results
    public static async Task<bool> CreateToolNode(string username, string recipe, string tool)
    {
        using var driver = GraphDatabase.Driver(ServerSettings.neo4jURI, AuthTokens.Basic(ServerSettings.dbUser, ServerSettings.dbPassword));
        var query = @"
            MATCH (recipe:Recipe {name: $recipeName})
            MERGE (tool:Tool {name: $toolName})
            MERGE (recipe)-[x:USES]->(tool)
            RETURN COUNT(x) > 0
        ";
        string recipeName = username + recipe;
        string toolName = username + tool;

        var session = driver.AsyncSession();
        try
        {
            var response = await session.RunAsync(query, new { recipeName, toolName });

            // Pulls all responses from query
            IReadOnlyList<IRecord> records = await response.ToListAsync();

            // Checks if there is a record && gets the first record which should be the bool response
            bool toolCreated = records.Any() && records.First()[0].As<bool>();
            Console.WriteLine("Returning Result: " + toolCreated);
            Console.WriteLine($"Tool {tool} created!");
            return toolCreated;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return false;
        }
        finally
        {
            await session.CloseAsync();
        }
    }

    public async static Task<List<string>> GetToolsByRecipe(string username, string recipe)
    {
        using var driver = GraphDatabase.Driver(ServerSettings.neo4jURI, AuthTokens.Basic(ServerSettings.dbUser, ServerSettings.dbPassword));
        var session = driver.AsyncSession();
        string recipeName = username + recipe;
        string query = @"MATCH (r:Recipe)-[]->(t:Tool)
            WHERE r.name = $recipeName
            RETURN t";

        try
        {


            var response = await session.RunAsync(query, new { recipeName });
            List<string> tools = new List<string>();
            // Pulls all responses from query
            IReadOnlyList<IRecord> records = await response.ToListAsync();

            // Checks if there is a record && gets the first record which should be the bool response
            foreach (IRecord i in records)
            {
                var tNode = i["t"].As<INode>();
                tools.Add(GetCleanString(username, tNode["name"].As<string>()));
            }
            return tools;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error, tools could not be retrieved for recipe {recipe}. Exception: {e}");
            return new List<string>();
        }
        finally
        {
            await session.CloseAsync();
        }
    }

    public async static Task<bool> RemoveToolsFromRecipe(string username, string recipe)
    {
        using var driver = GraphDatabase.Driver(ServerSettings.neo4jURI, AuthTokens.Basic(ServerSettings.dbUser, ServerSettings.dbPassword));
        var query = @"
               MATCH (recipe:Recipe)-[x:USES]->(t:Tool)
               WHERE recipe.name = $recipeName
               DELETE x
               WITH x
               MATCH(r:Recipe)-[xx:USES]->(t:Tool)
               WHERE r.name = $recipeName
               RETURN NOT(COUNT(xx) > 0)
           ";
        string recipeName = username + recipe;

        var session = driver.AsyncSession();
        try
        {
            var response = await session.RunAsync(query, new { recipeName });

            // Pulls all responses from query
            IReadOnlyList<IRecord> records = await response.ToListAsync();

            // Checks if there is a record && gets the first record which should be the bool response
            bool toolRemoved = records.First()[0].As<bool>();
            Console.WriteLine("Returning Result: " + toolRemoved);
            Console.WriteLine($"Tools removed");
            return toolRemoved;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return false;
        }
        finally
        {
            await session.CloseAsync();
        }
    }

    public static async Task<bool> RemoveIngredientFromRecipe(string ing, string recipe, string username)
    {
        string recipeName = username + recipe;
        string ingName = username + ing;

        using var driver = GraphDatabase.Driver(ServerSettings.neo4jURI, AuthTokens.Basic(ServerSettings.dbUser, ServerSettings.dbPassword));
        var query = @"
               MATCH (recipe:Recipe)-[x:MADE_WITH]->(i:Ingredient)
               WHERE recipe.name = $recipeName AND i.name = $ingName
               DELETE x
               WITH x
               RETURN COUNT(x) > 0
           ";

        var session = driver.AsyncSession();
        try
        {
            var response = await session.RunAsync(query, new { recipeName, ingName });

            // Pulls all responses from query
            IReadOnlyList<IRecord> records = await response.ToListAsync();

            // Checks if there is a record && gets the first record which should be the bool response
            bool toolRemoved = records.Any() && records.First()[0].As<bool>();
            Console.WriteLine("Returning Result: " + toolRemoved);
            return toolRemoved;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return false;
        }
        finally
        {
            await session.CloseAsync();
        }

    }

    public static async Task<bool> RemoveTagFromRecipe(string t, string r, string username)
    {
        using var driver = GraphDatabase.Driver(ServerSettings.neo4jURI, AuthTokens.Basic(ServerSettings.dbUser, ServerSettings.dbPassword));
        var query = @"
           MATCH (recipe:Recipe)-[x:TAGGED_WITH]->(t:Tag)
           WHERE recipe.name = $recipeName AND t.name = $tagName
           DELETE x
           WITH x
           RETURN NOT(COUNT(x) > 0)
       ";
        string recipeName = username + r;
        string tagName = username + t;

        var session = driver.AsyncSession();
        try
        {
            var response = await session.RunAsync(query, new { recipeName, tagName });

            // Pulls all responses from query
            IReadOnlyList<IRecord> records = await response.ToListAsync();

            // Checks if there is a record && gets the first record which should be the bool response
            bool tagRemoved = records.Any() && records.First()[0].As<bool>();
            Console.WriteLine("Returning Result: " + tagRemoved);
            return tagRemoved;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return false;
        }
        finally
        {
            await session.CloseAsync();
        }
    }

    // CreateGroup()
    // public static async Task<bool> CreateGroupNode(string group)
    // {
    //     var query = @"CREATE (group:Group {name: $groupName})";

    //     var session = driver.AsyncSession();
    //     try
    //     {
    //         await session.RunAsync(query, new { groupName = group });
    //         Console.WriteLine($"Group node {group} created!");
    //     }
    //     finally
    //     {
    //         await session.CloseAsync();
    //     }
    //     return true;
    // }

    // CreateShopListNode()
    // TODO - Test results
    public static async Task<bool> CreateShoppingListNode(string username)
    {
        using var driver = GraphDatabase.Driver(ServerSettings.neo4jURI, AuthTokens.Basic(ServerSettings.dbUser, ServerSettings.dbPassword));
        var query = @"
            MERGE (shopList:ShoppingList {name: $shopListName})
            RETURN COUNT(shopList) > 0
            ";
        var shopListName = username + "ShoppingList";

        var session = driver.AsyncSession();
        try
        {
            var response = await session.RunAsync(query, new { shopListName });
            Console.WriteLine($"Shopping List node {shopListName} created!");

            // Pulls all responses from query
            IReadOnlyList<IRecord> records = await response.ToListAsync();

            // Checks if there is a record && gets the first record which should be the bool response
            bool shopListCreated = records.Any() && records.First()[0].As<bool>();
            Console.WriteLine("Returning Result: " + shopListCreated);
            return shopListCreated;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return false;
        }
        finally
        {
            await session.CloseAsync();
        }
    }


    // CreateMealNode()
    // TODO - Test results
    public static async Task<bool> CreateMealNode(string username, MealSet meal)
    {
        using var driver = GraphDatabase.Driver(ServerSettings.neo4jURI, AuthTokens.Basic(ServerSettings.dbUser, ServerSettings.dbPassword));
        if (meal.Description == null)
        {
            meal.Description = string.Empty;
        }
        var query = @"
            MATCH (u:User {username: $username})
            MERGE (meal:Meal { name: $mealName})
            SET meal.description = $description
            MERGE (u)-[:OWNS]->(meal)
            RETURN COUNT(meal) > 0
        ";

        var mealName = username + meal.Name;

        var session = driver.AsyncSession();
        try
        {
            var response = await session.RunAsync(query, new { username, mealName, description = meal.Description });

            Console.WriteLine($"Meal node {mealName} created!");

            // Pulls all responses from query
            IReadOnlyList<IRecord> records = await response.ToListAsync();

            // Checks if there is a record && gets the first record which should be the bool response
            bool mealCreated = records.Any() && records.First()[0].As<bool>();
            return mealCreated;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return false;
        }
        finally
        {
            await session.CloseAsync();
        }
    }


    // MergeMealNode()
    // Used for adding all recipes within a meal
    // TODO - return success/fail
    public static async Task<bool> ConnectMealNode(string username, string meal, string recipe)
    {
        using var driver = GraphDatabase.Driver(ServerSettings.neo4jURI, AuthTokens.Basic(ServerSettings.dbUser, ServerSettings.dbPassword));
        var query = @"
        MATCH (meal:Meal{name:$mealName})
        MATCH (recipe:Recipe{name:$recipeName})
        MERGE (meal)-[x:MADE_WITH]->(recipe)
        RETURN COUNT(x) > 0
        ";
        var mealName = username + meal;
        var recipeName = username + recipe;

        var session = driver.AsyncSession();
        try
        {
            var response = await session.RunAsync(query, new { mealName, recipeName });
            Console.WriteLine($"Meal node {mealName} connected to {recipe}!");

            // Pulls all responses from query
            IReadOnlyList<IRecord> records = await response.ToListAsync();

            // Checks if there is a record && gets the first record which should be the bool response
            bool mealConnected = records.Any() && records.First()[0].As<bool>();
            return mealConnected;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return false;
        }
        finally
        {
            await session.CloseAsync();
        }
    }


    // CreateStepNode()
    // TODO - Test results
    // Order is the step number
    public static async Task<bool> CreateStepNode(string username, string recipe, string order, string description = "")
    {
        using var driver = GraphDatabase.Driver(ServerSettings.neo4jURI, AuthTokens.Basic(ServerSettings.dbUser, ServerSettings.dbPassword));
        description = string.IsNullOrEmpty(description) ? "" : description;

        var query = @"
        MATCH (recipe:Recipe {name: $recipeName})
        MERGE (step:Step {name: $stepName})
        ON CREATE SET
            step.order = $order,
            step.description = $description
        MERGE (recipe)-[x:HAS_STEP]->(step)
        RETURN COUNT(x) > 0
        ";

        var recipeName = username + recipe;
        var stepName = recipeName + "Step " + order;

        var session = driver.AsyncSession();
        try
        {
            var response = await session.RunAsync(query, new { recipeName, stepName, order, description });
            Console.WriteLine($"Step {order} created for {recipeName}!");

            // Pulls all responses from query
            IReadOnlyList<IRecord> records = await response.ToListAsync();

            // Checks if there is a record && gets the first record which should be the bool response
            bool mealCreated = records.Any() && records.First()[0].As<bool>();
            return mealCreated;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return false;
        }
        finally
        {
            await session.CloseAsync();
        }
    }

    //public static async Task<bool> RemoveStepNode(string step, string recipe, string username) {
    //
    //}


    // CreateTagNode()
    // Tag doesn't need special name since you can just add a connection and you never need to edit it
    // TODO - Test Results
    public static async Task<bool> CreateTagNode(string tag, string recipe, string username)
    {
        using var driver = GraphDatabase.Driver(ServerSettings.neo4jURI, AuthTokens.Basic(ServerSettings.dbUser, ServerSettings.dbPassword));
        var query = @"
        MATCH (recipe:Recipe {name: $recipeName})
        MERGE (tag:Tag {name: $tagName})
        MERGE (recipe)-[x:TAGGED_WITH]->(tag)
        RETURN COUNT(x) > 0
        ";

        var recipeName = username + recipe;
        var tagName = username + tag;

        var session = driver.AsyncSession();
        try
        {
            var response = await session.RunAsync(query, new { recipeName, tagName });
            Console.WriteLine($"Tag {tag} created and connected to {recipeName}!");

            // Pulls all responses from query
            IReadOnlyList<IRecord> records = await response.ToListAsync();

            // Checks if there is a record && gets the first record which should be the bool response
            bool tagCreated = records.Any() && records.First()[0].As<bool>();
            return tagCreated;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return false;
        }
        finally
        {
            await session.CloseAsync();
        }
    }


    //DONE
    public async static Task<bool> Authenticate(string username, string password)
    {
        using var driver = GraphDatabase.Driver(ServerSettings.neo4jURI, AuthTokens.Basic(ServerSettings.dbUser, ServerSettings.dbPassword));
        //Console.WriteLine("Authenticating " + username + " with " + password);
        string query = "MATCH (u:User {username: '" + username + "', password: '" + password + "'}) \n"
                                + "WITH COUNT(u) > 0 as exists \n"
                                + "RETURN exists";
        try
        {
            var response = await driver.ExecutableQuery(query).WithConfig(qConf).ExecuteAsync();
            //response is EagerResult<IReadOnlyList<IRecord>>
            IReadOnlyList<IRecord> irol = response.Result; //The Deconstruct() method has several outbound parameters. Result is one of them, and it can be referenced like a property here. First time I've seen this kind of behavior.'
            var record = irol.First(); //This gets the first IRecord of the list. This should be the only one in this case.
            if (record[0] != null) //&& record[0].As<bool>())
            {
                Console.WriteLine(Convert.ToString(record[0].As<bool>()));
                return record[0].As<bool>();
            }
            else
            {
                Console.WriteLine("DBQueryModel Authenticate Failed");
                return false;
            }
        }
        catch
        {
            return false;
        }
        finally
        {
            await driver.DisposeAsync();
        }
    }


    // Playing around with new GetRecipe
    public static async Task<Recipe> GetRecipe(string username, string recipe, string tool = "", string tag = "")
    {
        using var driver = GraphDatabase.Driver(ServerSettings.neo4jURI, AuthTokens.Basic(ServerSettings.dbUser, ServerSettings.dbPassword));
        var query = @"
            MATCH (recipe:Recipe {name: $recipeName})
            RETURN recipe.name AS recipeName,
                recipe.description AS description,
                recipe.servingSize AS servingSize,
                recipe.servings AS numServings,
                recipe.rating AS rating,
                recipe.difficulty AS difficulty,
                recipe.prepTime AS prepTime,
                recipe.cookTime AS cookTime
        ";

        var recipeName = username + recipe;

        // Initialize the Neo4j session
        var session = driver.AsyncSession();
        Recipe resultRecipe = new Recipe();

        try
        {
            // Run the query and pass in parameters
            var result = await session.RunAsync(query, new { recipeName });

            // Process each record in the result
            await result.ForEachAsync(record =>
            {
                // Initialize and populate a new Recipe object
                resultRecipe = new Recipe
                {
                    Name = GetCleanString(username, record["recipeName"]?.As<string>() ?? string.Empty),
                    Description = record["description"]?.As<string>() ?? string.Empty,
                    servingSize = record["servingSize"]?.As<string>() ?? string.Empty,
                    numServings = record["numServings"]?.As<int>() ?? 0, // Default to 0 if null
                    Rating = record["rating"]?.As<int>() ?? 1,            // Default to 1 if null
                    Difficulty = record["difficulty"]?.As<int>() ?? 1,    // Default to 1 if null
                    PrepTime = record["prepTime"]?.As<int>() ?? 0,
                    CookTime = record["cookTime"]?.As<int>() ?? 0,
                    Equipment = new List<string>(),
                    Tags = new List<string>()
                };
                //I commented this out since I'm adding them via foreach. I'm also not passing names for them in.
                //We wouldn't need to look them up if we already have them.
                // Add tool and tag names to Equipment and Tags lists if they are present
                //var toolNameValue = record["toolName"]?.As<string>();
                //if (!string.IsNullOrEmpty(toolNameValue))
                //{
                //    resultRecipe.Equipment.Add(toolNameValue);
                //}

                //var tagNameValue = record["tagName"]?.As<string>();
                //if (!string.IsNullOrEmpty(tagNameValue))
                //{
                //    resultRecipe.Tags.Add(tagNameValue);
                //s}
            });

            List<Tag> tList = GetTagsByRecipe(recipe, username).Result;
            if (tList.Any())
            {
                foreach (Tag t in GetTagsByRecipe(recipe, username).Result)
                {
                    resultRecipe.Tags.Add(t.Name);
                }
            }

            List<string> iList = GetInstuctionsByRecipe(recipe, username).Result;
            if (iList.Any())
            {
                resultRecipe.Instructions = iList;
            }

            List<Ingredient> ingList = GetIngredientsByRecipe(username, recipe).Result;
            if (ingList.Any())
            {
                Console.WriteLine("Ingredient List is not empty");
                foreach (Ingredient ing in ingList)
                {
                    Console.WriteLine("Finding IngredientDetail for " + ing);
                    IngredientDetail ingD = GetIngredientDetail(username, ing.Name, recipe).Result;
                    Console.WriteLine("Retrieved IngredientDetail");
                    Console.WriteLine("ingD: Name: " + ingD.Name + ", Quantity: " + ingD.Quantity + ", Unit: " + ingD.Unit + ", Qualifier: " + ingD.Qualifier);
                    resultRecipe.Ingredients.Add(ingD);
                }
            }

            //resultRecipe.Instructions = GetInstuctionsByRecipe(recipeName, username).Result;

            //Add foreach for tools here once we implement it. We may leave it as a comma delimited string for time.
            List<string> toolList = GetToolsByRecipe(username, recipe).Result;
            if (toolList.Any())
            {
                resultRecipe.Equipment = toolList;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
        finally
        {
            await session.CloseAsync();
        }

        return resultRecipe;
    }

    //TESTING
    public static async Task<bool> DeleteRecipe(string recName, string username, string group = "")
    {
        using var driver = GraphDatabase.Driver(ServerSettings.neo4jURI, AuthTokens.Basic(ServerSettings.dbUser, ServerSettings.dbPassword));
        Console.WriteLine("Entering DeleteRecipe with recName: " + recName);
        string name = username + recName;
        string query = "Match (:Recipe {name: '" + name + "'})-[]->(s:Step)\n" +
            "DETACH DELETE s \n" +
            "WITH s \n" +
            "MATCH (rec:Recipe {name:'" + name + "'})\n " +
            "DETACH DELETE rec \n" +
            "WITH rec \n" +
            "MATCH (rec:Recipe {name:'" + name + "'})\n " +
            "return NOT(Count(rec) >0) ";
        try
        {
            var response = await driver.ExecutableQuery(query).WithConfig(qConf).ExecuteAsync();
            //response is EagerResult<IReadOnlyList<IRecord>>
            IReadOnlyList<IRecord> irol = response.Result; //The Deconstruct() method has several outbound parameters. Result is one of them, and it can be referenced like a property here. First time I've seen this kind of behavior.'
            var record = irol.First(); //This gets the first IRecord of the list. This should be the only one in this case.
            if (record[0] != null)
            {
                Console.WriteLine("Result: " + record[0].As<bool>());
                return record[0].As<bool>();
            }
            else
            {
                Console.WriteLine("Returned record was null");
                return false;
            }
        }
        catch
        {
            return false;
        }
        finally
        {
            await driver.DisposeAsync();
        }
    }

    //TESTING
    public static async Task<List<string>> GetCookbookRecipes(string cbName, string username, string group = "")
    {
        using var driver = GraphDatabase.Driver(ServerSettings.neo4jURI, AuthTokens.Basic(ServerSettings.dbUser, ServerSettings.dbPassword));
        string name = username + cbName;
        string query = "MATCH (u:User)-[:OWNS]->(cb:Cookbook)\n " +
            "WHERE cb.name = '" + name + "' AND u.username = '" + username + "'\n" +
            "MATCH (cb)-[]->(r:Recipe)\n" +
            "return r\n";
        try
        {
            var response = await driver.ExecutableQuery(query).WithConfig(qConf).ExecuteAsync();
            IReadOnlyList<IRecord> irol = response.Result;
            List<string> recipes = new List<string>();
            foreach (var record in irol)
            {
                var rNode = record["r"].As<INode>();

                recipes.Add(GetCleanString(username, rNode["name"].As<string>()));
            }
            return recipes;
        }
        catch
        {
            return new List<string>();
        }
        finally
        {
            await driver.DisposeAsync();
        }
    }

    public static async Task<Cookbook> GetCookbook(string cbName, string username)
    {
        using var driver = GraphDatabase.Driver(ServerSettings.neo4jURI, AuthTokens.Basic(ServerSettings.dbUser, ServerSettings.dbPassword));
        string name = username + cbName;
        string query = "MATCH (u:User)-[:OWNS]->(cb:Cookbook)\n " +
            "WHERE cb.name = '" + name + "' AND u.username = '" + username + "'\n" +
            "return cb\n";
        try
        {
            var response = await driver.ExecutableQuery(query).WithConfig(qConf).ExecuteAsync();
            IReadOnlyList<IRecord> irol = response.Result;
            Cookbook cb = new Cookbook();
            var cbNode = irol.First()["cb"].As<INode>();
            cb.Title = GetCleanString(username, cbNode["name"].As<string>());
            cb.Description = cbNode["description"].As<string>();
            cb.RecipeNames = GetCookbookRecipes(cbName, username).Result;
            cb.PrintAllStats();
            return cb;
        }
        catch
        {
            return new Cookbook();
        }
        finally
        {
            await driver.DisposeAsync();
        }
    }

    public static async Task<List<Cookbook>> GetCookbooks(string username, string group = "")
    {
        using var driver = GraphDatabase.Driver(ServerSettings.neo4jURI, AuthTokens.Basic(ServerSettings.dbUser, ServerSettings.dbPassword));
        string name = username;
        string startLabel = "User";
        try
        {
            List<Cookbook> cbks = new List<Cookbook>();
            string query = "MATCH (:" + startLabel + " {username:'" + name + "'})-[:OWNS]->(cb:Cookbook)\n " +
                                        "return cb\n";
            var response = await driver.ExecutableQuery(query).WithConfig(qConf).ExecuteAsync();
            IReadOnlyList<IRecord> irol = response.Result;
            foreach (var record in irol)
            {
                var cbNode = record["cb"].As<INode>();
                //We only add the name because there are no
                Cookbook cb = new Cookbook();
                cb.Title = GetCleanString(username, cbNode["name"].As<string>());
                cbks.Add(cb);
            }
            return cbks;
        }
        catch
        {
            return new List<Cookbook>();
        }
        finally
        {
            await driver.DisposeAsync();
        }
    }

    public static async Task<List<string>> GetCookbookNameList(string username)
    {

        using var driver = GraphDatabase.Driver(ServerSettings.neo4jURI, AuthTokens.Basic(ServerSettings.dbUser, ServerSettings.dbPassword));
        var query = @"
            MATCH (u:User {username: $username})-[]->(cookbook:Cookbook)
            RETURN cookbook
        ";


        var session = driver.AsyncSession();
        try
        {
            var response = await session.RunAsync(query, new { username });
            Console.WriteLine($"Cookbooks retrieved");

            // Pulls all responses from query
            IReadOnlyList<IRecord> records = await response.ToListAsync();
            List<string> cbNames = new List<string>();

            // Checks if there is a record && gets the first record which should be the bool response
            foreach (IRecord r in records)
            {
                var cNode = r["cookbook"].As<INode>();
                cbNames.Add(GetCleanString(username, cNode["name"].As<string>()));
            }
            return cbNames;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return new List<string>();
        }
        finally
        {
            await session.CloseAsync();
        }
    }

    public static async Task<bool> EditCookBook(string username, string name, string description)
    {
        Console.WriteLine($"Entering DBQueryModel.EditCookbook with parameters: username: {username}, name: {name}, description: {description}");
        using var driver = GraphDatabase.Driver(ServerSettings.neo4jURI, AuthTokens.Basic(ServerSettings.dbUser, ServerSettings.dbPassword));
        var query = @"
            MATCH (cookbook:Cookbook {name: $cookbookName})
            WHERE cookbook.description <> $description
            SET cookbook.description = $description
            RETURN COUNT(cookbook) > 0
        ";

        var cookbookName = username + name;

        var session = driver.AsyncSession();
        try
        {
            var response = await session.RunAsync(query, new { cookbookName, description });
            Console.WriteLine($"Cookbook node {cookbookName} edited!");

            // Pulls all responses from query
            IReadOnlyList<IRecord> records = await response.ToListAsync();

            // Checks if there is a record && gets the first record which should be the bool response
            bool cookbookCreated = records.Any() && records.First()[0].As<bool>();
            Console.WriteLine("Returning Result: " + cookbookCreated);
            return cookbookCreated;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return false;
        }
        finally
        {
            await session.CloseAsync();
        }
    }

    //IN PROGRESS
    public static async Task<bool> DeleteCookbook(string cbName, string username, string group = "")
    {
        using var driver = GraphDatabase.Driver(ServerSettings.neo4jURI, AuthTokens.Basic(ServerSettings.dbUser, ServerSettings.dbPassword));
        string name = username + cbName;
        string startLabel = "User";
        string query = "MATCH (:" + startLabel + " {username:'" + username + "'})-[:OWNS]->(cb:Cookbook {name:'" + name + "'})\n " +
                                    "WITH cb\n" +
                                    "DETACH DELETE cb\n" +
                                    "WITH cb\n" +
                        "MATCH (:" + startLabel + " {name:'" + username + "'})-[:OWNS]->(cbk:Cookbook {name:'" + name + "'})\n " +
                        "return NOT(Count(cbk) > 0)";
        try
        {
            var response = await driver.ExecutableQuery(query).WithConfig(qConf).ExecuteAsync();
            IReadOnlyList<IRecord> irol = response.Result;
            var record = irol.First<IRecord>();
            return record[0].As<bool>();
        }
        catch
        {
            return false;
        }
        finally
        {
            await driver.DisposeAsync();
        }
    }

    //WIP
    public static async Task<Ingredient> GetIngredient(string username, string ingName, string group = "")
    {
        using var driver = GraphDatabase.Driver(ServerSettings.neo4jURI, AuthTokens.Basic(ServerSettings.dbUser, ServerSettings.dbPassword));
        string name = username + ingName;
        string startLabel = "User";
        string query = "MATCH (u:" + startLabel + ")-[]->(t:Ingredient)\n" +
            "WHERE t.name = '" + name + "'\n" +
            "return t\n";

        Console.WriteLine("GetIngredient Query String: " + query);

        Ingredient ing = new Ingredient();

        try
        {
            var response = await driver.ExecutableQuery(query).WithConfig(qConf).ExecuteAsync();
            IReadOnlyList<IRecord> irol = response.Result;
            var record = irol.First<IRecord>();
            var tNode = record["t"].As<INode>();
            ing.Name = GetCleanString(username, tNode["name"].As<string>());
            //ing.Unit = GetCleanString(username, iNode["unit"].As<string>() ?? string.Empty);
            //ing.Description = GetCleanString(username, iNode["description"].As<string>() ?? string.Empty);
            return ing;
        }
        catch (Exception e)
        {
            Console.WriteLine("Error accessing GetIngredient query results. Exception: " + e);
            return new Ingredient() { Name = "ERROR" };
        }
        finally
        {
            await driver.DisposeAsync();
        }
    }

    public static async Task<IngredientDetail> GetIngredientDetail(string username, string ingName, string recipeName)
    {
        using var driver = GraphDatabase.Driver(ServerSettings.neo4jURI, AuthTokens.Basic(ServerSettings.dbUser, ServerSettings.dbPassword));
        Console.WriteLine("Entering GetIngredientDetail with " + username + ", " + ingName + ", " + recipeName);
        string rname = username + recipeName;
        string iname = username + ingName;
        string startLabel = "User";

        string query = "MATCH (:" + startLabel + ")-[:OWNS]->(rec:Recipe)-[r:MADE_WITH]->(i:Ingredient)\n " +
            "WHERE rec.name = '" + rname + "' AND " + "i.name = '" + iname + "'\n" +
            "return r\n";
        var response = await driver.ExecutableQuery(query).WithConfig(qConf).ExecuteAsync();
        IReadOnlyList<IRecord> irol = response.Result;
        var record = irol.First<IRecord>();
        var iRel = record["r"].As<IRelationship>(); //Relationships use IRelationship instead of INode.
        IngredientDetail ingD = new IngredientDetail();
        Console.WriteLine("Retrieving Ingredient " + ingName);
        ingD.Ingredient = new Ingredient() { Name = ingName };// GetIngredient(username, ingName).Result; GetIngredient was failing, but I realized we don't even need it here.  //
        Console.WriteLine("Retrieved Ingredient ");
        Console.WriteLine("ingD.Ingredient: Name: " + ingD.Ingredient.Name);
        ingD.Name = ingD.Ingredient.Name;


        try
        {
            ingD.Qualifier = iRel["qualifier"].As<string>() ?? string.Empty;
            //ing.Unit = GetCleanString(username, iNode["unit"].As<string>() ?? string.Empty);
            ingD.Quantity = Convert.ToDouble(iRel["quantity"].As<string>() ?? "0");
            ingD.Unit = iRel["unit"].As<string>() ?? string.Empty;
            return ingD;
        }
        catch (Exception e)
        {
            Console.WriteLine("Error accessing query results. Exception: " + e);
            ingD.Name = "ERROR";
            ingD.Ingredient.Name = "ERROR";
            return ingD;
        }
        finally
        {
            await driver.DisposeAsync();
        }
    }

    public static async Task<List<Ingredient>> GetIngredientsByRecipe(string username, string recipeName)
    {
        using var driver = GraphDatabase.Driver(ServerSettings.neo4jURI, AuthTokens.Basic(ServerSettings.dbUser, ServerSettings.dbPassword));
        Console.WriteLine("Entering GetIngredientsByRecipe with username: " + username + ", recipeName: " + recipeName);
        string name = username + recipeName;
        string startLabel = "User";
        string query = "MATCH (:" + startLabel + ")-[:OWNS]->(r:Recipe)-[]-(t:Ingredient)\n " +
            "WHERE r.name = '" + name + "'\n" +
            "return t\n";
        try
        {
            var response = await driver.ExecutableQuery(query).WithConfig(qConf).ExecuteAsync();
            IReadOnlyList<IRecord> irol = response.Result;
            List<Ingredient> ingList = new List<Ingredient>();
            foreach (var record in irol)
            {
                var iNode = record["t"].As<INode>();
                Ingredient ing = new Ingredient();

                try
                {
                    ing.Name = GetCleanString(username, iNode["name"].As<string>() ?? string.Empty);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error accessing query results. Exception: " + e);
                }
                ingList.Add(ing);
            }
            Console.WriteLine("List of Ingredients Found: ");
            foreach (Ingredient ing in ingList)
            {
                Console.WriteLine("     " + ing.Name);
            }
            Console.WriteLine("Exiting GetIngredientsByRecipe");
            return ingList;
        }
        catch
        {
            return new List<Ingredient>();
        }
        finally
        {
            await driver.DisposeAsync();
        }
    }

    //TESTING
    private static async Task<List<Tag>> GetTagsByRecipe(string recName, string username, string group = "")
    {
        using var driver = GraphDatabase.Driver(ServerSettings.neo4jURI, AuthTokens.Basic(ServerSettings.dbUser, ServerSettings.dbPassword));
        string name = username + recName;
        string startLabel = "User";
        string query = "MATCH (:" + startLabel + ")-[r]->(b:Recipe)-[]-(t:Tag)\n " +
            "WHERE b.name = '" + name + "'\n" +
            "return t\n";
        try
        {
            var response = await driver.ExecutableQuery(query).WithConfig(qConf).ExecuteAsync();
            IReadOnlyList<IRecord> irol = response.Result;
            List<Tag> lt = new List<Tag>();
            foreach (var record in irol)
            {
                Tag t = new Tag();
                var tNode = record["t"].As<INode>();
                try
                {
                    t.Name = GetCleanString(username, tNode["name"].As<string>());
                }
                catch
                {
                    t.Name = "";
                }
                lt.Add(t);
            }
            return lt;
        }
        catch
        {
            return new List<Tag>();
        }
        finally
        {
            await driver.DisposeAsync();
        }
    }

    //TESTING
    private static async Task<List<string>> GetInstuctionsByRecipe(string recName, string username)
    {
        using var driver = GraphDatabase.Driver(ServerSettings.neo4jURI, AuthTokens.Basic(ServerSettings.dbUser, ServerSettings.dbPassword));
        string name = username + recName;
        string startLabel = "User";
        string query = "MATCH (:" + startLabel + ")-[r]->(b:Recipe)-[]-(t:Step)\n " +
            "WHERE b.name = '" + name + "'\n" +
            "return t\n" +
            "ORDER BY t.order";
        try
        {
            var response = await driver.ExecutableQuery(query).WithConfig(qConf).ExecuteAsync();
            IReadOnlyList<IRecord> irol = response.Result;
            List<string> lt = new List<string>();
            foreach (var record in irol)
            {
                string t;
                var tNode = record["t"].As<INode>();
                try
                {
                    t = tNode["description"].As<string>();
                }
                catch
                {
                    t = "";
                }
                lt.Add(t);
            }
            lt.Sort();
            return lt;
        }
        catch
        {
            return new List<string>();
        }
        finally
        {
            await driver.DisposeAsync();
        }
    }

    //TESTING
    private static async Task<bool> DetachTagFromRecipe(string username, string tagName, string recName)
    {
        using var driver = GraphDatabase.Driver(ServerSettings.neo4jURI, AuthTokens.Basic(ServerSettings.dbUser, ServerSettings.dbPassword));
        string name;
        string startLabel;
        startLabel = "User";
        name = username + recName;

        string query = "MATCH (:" + startLabel + ")-[:OWNS]->(rec:Recipe)-[]->(t:Tag)\n " +
            "WHERE t.name = '" + tagName + "' AND rec.name = '" + name + "'\n" +
            "WITH t\n" +
            "DETACH t\n" +
            "WITH t\n" +
            "MATCH (:User )-[:OWNS]->(recc:Recipe)-[]->(tt:Tag)\n " +
            "WHERE tt.name = '" + tagName + "' AND recc.name = '" + name + "'\n" +
            "return NOT(COUNT(tt) > 0)\n";
        try
        {
            var response = await driver.ExecutableQuery(query).WithConfig(qConf).ExecuteAsync();
            IReadOnlyList<IRecord> irol = response.Result;
            var record = irol.First<IRecord>();
            return record[0].As<bool>();
        }
        catch
        {
            return false;
        }
        finally
        {
            await driver.DisposeAsync();
        }
    }

    public static async Task<bool> DeleteStepsFromRecipe(string username, string recipeName)
    {
        using var driver = GraphDatabase.Driver(ServerSettings.neo4jURI, AuthTokens.Basic(ServerSettings.dbUser, ServerSettings.dbPassword));
        string name;
        name = username + recipeName;

        string query = @$"MATCH (:User)-[:OWNS]->(rec:Recipe)-[]->(t:Step)
            WHERE rec.name = '{name}'
            DETACH DELETE t
            WITH t
            MATCH (:User )-[:OWNS]->(recc:Recipe)-[]->(tt:Step)
            WHERE recc.name = '{name}'
            return NOT(COUNT(tt) > 0)";
        try
        {
            var response = await driver.ExecutableQuery(query).WithConfig(qConf).ExecuteAsync();
            IReadOnlyList<IRecord> irol = response.Result;
            var record = irol.First<IRecord>();
            return record[0].As<bool>();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error removing steps from recipe {recipeName}. Exception " + e);
            return false;
        }
        finally
        {
            await driver.DisposeAsync();
        }
    }

    //FILL IN
    //The Inner Lists are the meal for that day.
    public static async Task<List<MPMeal>> GetMealPlanByDay(string username, string date)
    {
        Console.WriteLine("Received data string: " + date);

        using var driver = GraphDatabase.Driver(ServerSettings.neo4jURI, AuthTokens.Basic(ServerSettings.dbUser, ServerSettings.dbPassword));
        var query = @"
            MATCH (:User {name: $username})-[]->(rec:Recipe)-[x:SCHEDULED_FOR]->(:MealPlan)
            WHERE x.date = $date
            RETURN rec, x
        ";


        // Initialize the Neo4j session
        var session = driver.AsyncSession();
        List<MPMeal> mp = new List<MPMeal>();

        try
        {
            // Run the query and pass in parameters
            var result = await session.RunAsync(query, new { username, date });

            // Process each record in the result
            await result.ForEachAsync(record =>
            {
                string r = GetCleanString(username, record["rec"].As<INode>()["name"].As<string>());//new Recipe()
                //{
                //Name = GetCleanString(username, record["rec"].As<INode>()["name"].As<string>())
                //};
                Console.WriteLine("Recipe Name: " + r);
                // Initialize and populate a new Recipe object
                //MealSet ms = new MealSet
                //{
                //    Name = "",
                //};
                int order = record["x"].As<IRelationship>()["order"].As<int>();

                while (mp.Count <= order)
                {
                    mp.Add(new MPMeal());
                }
                mp[order].recipeNames.Add(r);
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return new List<MPMeal>();
        }
        finally
        {
            await session.CloseAsync();
        }
        return mp;
    }

    //FILL IN
    //Returns list of all meals.
    public static async Task<List<MealSet>> GetMeals(string username)
    {
        Console.WriteLine("Entering GetMeals");
        using var driver = GraphDatabase.Driver(ServerSettings.neo4jURI, AuthTokens.Basic(ServerSettings.dbUser, ServerSettings.dbPassword));
        var query = @"
            MATCH (:User {username: $username})-[:OWNS]->(m:Meal)
            RETURN m
        ";


        // Initialize the Neo4j session
        var session = driver.AsyncSession();
        List<MealSet> mp = new List<MealSet>();

        try
        {
            // Run the query and pass in parameters
            var result = await session.RunAsync(query, new { username });

            // Process each record in the result
            await result.ForEachAsync(record =>
            {
                MealSet m = GetMeal(username, GetCleanString(username, record["m"].As<INode>()["name"].As<string>())).Result;
                mp.Add(m);
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return new List<MealSet>();
        }
        finally
        {
            await session.CloseAsync();
        }
        return mp;
    }

    //FILL IN
    //Returns list of all meals.
    public static async Task<MealSet> GetMeal(string username, string mealName)
    {
        Console.WriteLine("Entering GetMeal");
        using var driver = GraphDatabase.Driver(ServerSettings.neo4jURI, AuthTokens.Basic(ServerSettings.dbUser, ServerSettings.dbPassword));
        string name = username + mealName;
        var query = @"
        MATCH (:User {username: $username})-[:OWNS]->(m:Meal {name: $name})
        MATCH (rec:Recipe)<-[:MADE_WITH]-(m)
        RETURN rec
        ";


        // Initialize the Neo4j session
        var session = driver.AsyncSession();
        MealSet meal = new MealSet();
        meal.Name = mealName;

        try
        {
            // Run the query and pass in parameters
            var result = await session.RunAsync(query, new { username, name });

            // Process each record in the result
            await result.ForEachAsync(record =>
            {
                Recipe r = GetRecipe(username, GetCleanString(username, record["rec"].As<INode>()["name"].As<string>())).Result;
                meal.Recipes.Add(r);
                //meal.RecipeNames.Add(r.Name);
                Console.WriteLine("Retrieved recipe " + r.Name + " from meal " + mealName);
            });
        }
        catch (Exception e)
        {
            Console.WriteLine("Error in DBQueryModel.GetMeal. Exception: " + e);
            return new MealSet();
        }
        finally
        {
            await session.CloseAsync();
        }
        return meal;
    }

    //Adds ingredient to pantry.
    //If the item is already there, it needs to add to the existing quantity of item.
    //Make sure Units are consistent
    public static async Task<bool> AddToPantry(string username, string pantry, string ingredient, string unit = "", string qualifier = "", double quantity = 0)
    {
        try
        {
            // Ensure ingredient node is created
            bool ingredientCreated = await DBQueryModel.CreateIngredientNode(username, ingredient);

            if (!ingredientCreated)
            {
                throw new Exception("Failed to create ingredient node.");
            }

            // Connect the ingredient node to the pantry, updating quantity if it exists
            bool ingredientConnected = await DBQueryModel.ConnectIngredientNode(username, "Pantry", ingredient, unit, qualifier, quantity);

            if (!ingredientConnected)
            {
                throw new Exception("Failed to connect ingredient node to pantry.");
            }

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Item could not be added to pantry. Exception: {ex.Message}");
            return false;
        }
    }


    //Basically AddToPantry with a negative number.
    public static async Task<bool> RemoveFromPantry(string username, string ingredient)
    {
        using var driver = GraphDatabase.Driver(ServerSettings.neo4jURI, AuthTokens.Basic(ServerSettings.dbUser, ServerSettings.dbPassword));
        var query = @"
            MATCH (pantry:Pantry {name: $pantryName})
            MATCH (ingredient:Ingredient {name: $ingredientName})
            MATCH (pantry)-[x:STORES]->(ingredient)
            DELETE (x)
            RETURN true
        ";
        var pantryName = username + "Pantry";
        var ingredientName = username + ingredient;

        var session = driver.AsyncSession();
        try
        {
            Console.WriteLine("Executing Query...");
            var response = await session.RunAsync(query, new { pantryName, ingredientName });
            IReadOnlyList<IRecord> records = await response.ToListAsync();

            // Checks if there is a record && gets the first record which should be the bool response
            bool removedFromPantry = records.Any() && records.First()[0].As<bool>();
            Console.WriteLine("Returning Result: " + removedFromPantry);
            return removedFromPantry;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return false;
        }
        finally
        {
            // Ensures the session is closed
            await session.CloseAsync();
        }
    }

    //Gets all pantry items a user has.
    //Ignore items where quantity <= 0. I think anyways.
    public static async Task<List<IngredientDetail>> GetPantry(string username)
    {
        using var driver = GraphDatabase.Driver(ServerSettings.neo4jURI, AuthTokens.Basic(ServerSettings.dbUser, ServerSettings.dbPassword));
        var query = @"
        MATCH (:User {username: $username})-[]->(:Pantry)-[:STORES]->(i:Ingredient)
        RETURN i
        ";

        // Initialize the Neo4j session
        var session = driver.AsyncSession();
        List<IngredientDetail> ingDList = new List<IngredientDetail>();

        try
        {
            // Run the query and pass in parameters
            var result = await session.RunAsync(query, new { username });

            // Process each record in the result
            await result.ForEachAsync(record =>
            {
                IngredientDetail ingD = GetPantryIngredient(username, GetCleanString(username, record["i"].As<INode>()["name"].As<string>())).Result;
                ingDList.Add(ingD);
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return new List<IngredientDetail>();
        }
        finally
        {
            // Ensures the session is closed
            await session.CloseAsync();
        }
        return ingDList;
    }

    public static async Task<IngredientDetail> GetPantryIngredient(string username, string ingName)
    {
        using var driver = GraphDatabase.Driver(ServerSettings.neo4jURI, AuthTokens.Basic(ServerSettings.dbUser, ServerSettings.dbPassword));
        var query = @"
        MATCH (:User {username: $username})-[]->(:Pantry)-[x:STORES]->(i:Ingredient)
        WHERE i.name = $ingredient
        RETURN x
        ";


        // Initialize the Neo4j session
        var session = driver.AsyncSession();
        string ingredient = username + ingName;
        IngredientDetail ingD = new IngredientDetail();
        ingD.Ingredient.Name = ingName;
        ingD.Name = ingName;

        try
        {
            // Run the query and pass in parameters
            var result = await session.RunAsync(query, new { username, ingredient });

            // Process each record in the result
            await result.ForEachAsync(record =>
            {
                var iRel = record["x"].As<IRelationship>();
                ingD.Quantity = iRel["quantity"].As<double>();
                ingD.Unit = iRel["unit"].As<string>();
                ingD.Qualifier = iRel["qualifier"].As<string>();
            });
        }
        catch (Exception e)
        {
            Console.WriteLine("Error in DBQueryModel.GetPantryIngredient. Exception: " + e);
            return new IngredientDetail();
        }
        finally
        {
            // Ensures the session is closed
            await session.CloseAsync();
        }
        return ingD;
    }

    //Connects recipe node to cookbook node
    public static async Task<bool> AddToCookbook(string username, string cookbookName, string recipeName)
    {
        using var driver = GraphDatabase.Driver(ServerSettings.neo4jURI, AuthTokens.Basic(ServerSettings.dbUser, ServerSettings.dbPassword));
        string cookbook = username + cookbookName;
        string recipe = username + recipeName;
        var query = @"
            MATCH (cookbook:Cookbook)
            WHERE cookbook.name = $cookbook
            MATCH (recipe:Recipe)
            WHERE recipe.name = $recipe
            MERGE (cookbook)-[x:CATALOGUES]->(recipe)
            RETURN COUNT(x) > 0
        ";

        var session = driver.AsyncSession();
        try
        {
            var response = await session.RunAsync(query, new { cookbook, recipe });
            // Pulls all responses from query
            IReadOnlyList<IRecord> records = await response.ToListAsync();
            // Checks if there is a record && gets the first record which should be the bool response
            return records.First()[0].As<bool>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return false;
        }
        finally
        {
            await session.CloseAsync();
        }
    }

    //Detaches recipe node from cookbook node
    public static async Task<bool> RemoveFromCookbook(string username, string cookbookName, string recipeName)
    {
        using var driver = GraphDatabase.Driver(ServerSettings.neo4jURI, AuthTokens.Basic(ServerSettings.dbUser, ServerSettings.dbPassword));
        string cookbook = username + cookbookName;
        string recipe = username + recipeName;
        var query = @"
            MATCH (cookbook:Cookbook)
            WHERE cookbook.name = $cookbook
            MATCH (recipe:Recipe)
            WHERE recipe.name = $recipe
            MATCH (cookbook)-[x:CATALOGUES]->(recipe)
            DELETE x
            RETURN COUNT(x) > 0
        ";

        var session = driver.AsyncSession();
        try
        {
            var response = await session.RunAsync(query, new { cookbook, recipe });
            // Pulls all responses from query
            IReadOnlyList<IRecord> records = await response.ToListAsync();
            // Checks if there is a record && gets the first record which should be the bool response
            return records.First()[0].As<bool>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return false;
        }
        finally
        {
            await session.CloseAsync();
        }
    }

    public static async Task<bool> DeleteMeal(string username, string meal)
    {
        Console.WriteLine($"Entering DBQueryModel.DeleteMeal with parameters: username: {username}, meal {meal}");
        using var driver = GraphDatabase.Driver(ServerSettings.neo4jURI, AuthTokens.Basic(ServerSettings.dbUser, ServerSettings.dbPassword));
        var deleteQuery = @"
            MATCH (meal:Meal {name: $mealName})
            DETACH DELETE meal
        ";
        var checkQuery = @"
            MATCH (meal:Meal {name: $mealName})
            RETURN COUNT(meal) = 0
        ";
        var mealName = username + meal;
        var session = driver.AsyncSession();
        try
        {
            // First, delete the meal node
            await session.RunAsync(deleteQuery, new { mealName });
            // Must check sperately if deleted since meal object won't get updated after deletion
            var response = await session.RunAsync(checkQuery, new { mealName });
            var record = await response.SingleAsync();
            bool mealDeleted = record.Any() && record[0].As<bool>();
            //bool mealDeleted = record["mealDeleted"].As<bool>();

            //What is this doing?
            //if (!mealDeleted)
            //{
            //    // Delete only the MADE_WITH relationship if the meal node still exists
            //    var relationshipResult = await session.RunAsync(deleteRelationshipOnly, new { mealName });
            //    var relationshipRecord = await relationshipResult.SingleAsync();
            //    mealDeleted = relationshipRecord["mealDeleted"].As<bool>();
            //}

            return mealDeleted;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return false;
        }
        finally
        {
            await session.CloseAsync();
        }
    }



    //Connects a recipe node to a meal node
    public static async Task<bool> AddToMeal(string username, string recipe, string meal)
    {
        using var driver = GraphDatabase.Driver(ServerSettings.neo4jURI, AuthTokens.Basic(ServerSettings.dbUser, ServerSettings.dbPassword));
        var query = @"
            MATCH (meal:Meal{name:$mealName})
            MATCH (recipe:Recipe{name:$recipeName})
            MERGE (meal)-[x:MADE_WITH]->(recipe)
            RETURN COUNT(x) > 0
        ";

        var mealName = username + meal;
        var recipeName = username + recipe;


        var session = driver.AsyncSession();

        try
        {
            var response = await session.RunAsync(query, new { mealName, recipeName });
            var record = await response.SingleAsync();

            bool connectionExists = record.Any() && record[0].As<bool>();
            return connectionExists;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return false;
        }
        finally
        {
            await session.CloseAsync();
        }
    }

    //Detaches a recipe node from a meal node
    public static async Task<bool> RemoveFromMeal(string username, string recipe, string meal)
    {
        using var driver = GraphDatabase.Driver(ServerSettings.neo4jURI, AuthTokens.Basic(ServerSettings.dbUser, ServerSettings.dbPassword));
        var deleteQuery = @"
            MATCH (meal:Meal{name:$mealName})
            MATCH (recipe:Recipe{name:$recipeName})
            MATCH (meal)-[x:MADE_WITH]->(recipe)
            DELETE (x)
        ";
        var checkQuery = @"
            MATCH (meal:Meal{name:$mealName})
            MATCH (recipe:Recipe{name:$recipeName})
            MATCH (meal)-[x:MADE_WITH]->(recipe)
            RETURN COUNT(x) = 0
        ";

        var mealName = username + meal;
        var recipeName = username + recipe;

        var session = driver.AsyncSession();

        try
        {
            // First, delete the meal node
            await session.RunAsync(deleteQuery, new { mealName });

            // Must check sperately if deleted since meal object won't get updated after deletion
            var response = await session.RunAsync(checkQuery, new { mealName, recipeName });
            var record = await response.SingleAsync();

            bool mealDisconnected = record.Any() && record[0].As<bool>();
            return mealDisconnected;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return false;
        }
        finally
        {
            await session.CloseAsync();
        }
    }

    //Connects a recipe node to the meal plan node with a date parameter
    // Simultaneously creates the meal if it didn't exist already
    public static async Task<bool> ScheduleRecipe(string username, string recipe, string date, int order)
    {
        Console.WriteLine($"Entering DBQueryModel.ScheduleRecipe with parameters: username: {username}, recipe {recipe}, date {date}, order {order}");

        using var driver = GraphDatabase.Driver(ServerSettings.neo4jURI, AuthTokens.Basic(ServerSettings.dbUser, ServerSettings.dbPassword));
        var query = @"
            MATCH (recipe:Recipe{name:$recipeName})
            MERGE (mealPlan:MealPlan{name:$mealName})
            CREATE (recipe)-[x:SCHEDULED_FOR]->(mealPlan)
            SET x.date = $date,
                x.order = $order
            RETURN COUNT(x) > 0
        ";
        var mealName = username + "MealPlan";
        var recipeName = username + recipe;


        var session = driver.AsyncSession();

        try
        {
            var response = await session.RunAsync(query, new { recipeName, mealName, date, order });
            var record = await response.SingleAsync();

            bool connectionExists = record.Any() && record[0].As<bool>();
            return connectionExists;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return false;
        }
        finally
        {
            await session.CloseAsync();
        }
    }

    //Detaches a recipe node from the schedule
    public static async Task<bool> UnScheduleRecipe(string username, string recipe, string date, string order)
    {
        using var driver = GraphDatabase.Driver(ServerSettings.neo4jURI, AuthTokens.Basic(ServerSettings.dbUser, ServerSettings.dbPassword));
        var deleteQuery = @"
            MATCH (recipe:Recipe{name:$recipeName})
            MATCH (recipe)-[x:SCHEDULED_FOR{date:$date, order:$order}]->()
            DELETE (x)
        ";
        var checkQuery = @"
            MATCH (recipe)-[x:SCHEDULED_FOR{date:$date, order:$order}]->()
            RETURN COUNT(x) = 0
        ";

        var recipeName = username + recipe;
        var session = driver.AsyncSession();

        try
        {
            // First, delete the meal node
            await session.RunAsync(deleteQuery, new { recipeName, date, order });

            // Must check sperately if deleted since meal object won't get updated after deletion
            var response = await session.RunAsync(checkQuery, new { recipeName });
            var record = await response.SingleAsync();

            bool recipeDisconnected = record.Any() && record[0].As<bool>();
            return recipeDisconnected;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return false;
        }
        finally
        {
            await session.CloseAsync();
        }
    }

    //Connects an Inredient node to the shoppin list with ingredientDetail parameters.
    //Creates it if it doesn't exist too
    public static async Task<bool> AddToShoppingList(string username, IngredientDetail ingD)
    {
        bool tmp;
        try
        {
            await CreateIngredientNode(username, ingD.Name);
        }
        catch
        {
            Console.WriteLine("Ingredient Already Exists (Probably)");
        }

        try
        {
            tmp = ConnectIngredientNode(username, "ShoppingList", ingD.Name, ingD.Unit, ingD.Qualifier, ingD.Quantity).Result;
        }
        catch
        {
            return false;
        }
        return tmp;
    }

    //Detaches an ingredient from the shopping list.
    public static async Task<bool> RemoveFromShoppingList(string username, string ingredient)
    {
        using var driver = GraphDatabase.Driver(ServerSettings.neo4jURI, AuthTokens.Basic(ServerSettings.dbUser, ServerSettings.dbPassword));
        var query = @"
            MATCH (ingredient:Ingredient {name: $ingredientName})
            MATCH (:ShoppingList)-[x:PLANS_TO_BUY]->(ingredient)
            DELETE (x)
            RETURN true
        ";
        var ingredientName = username + ingredient;

        var session = driver.AsyncSession();
        try
        {
            Console.WriteLine("Executing Query...");
            var response = await session.RunAsync(query, new { ingredientName });
            IReadOnlyList<IRecord> records = await response.ToListAsync();

            // Checks if there is a record && gets the first record which should be the bool response
            bool removedFromShopList = records.Any() && records.First()[0].As<bool>();
            Console.WriteLine("Returning Result: " + removedFromShopList);
            return removedFromShopList;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return false;
        }
        finally
        {
            // Ensures the session is closed
            await session.CloseAsync();
        }
    }

    // Essentially a edit function for the checked propery on the shoplist to ing relationship
    public static async Task<bool> CheckShoppingListItem(string username, string ingredient)
    {
        using var driver = GraphDatabase.Driver(ServerSettings.neo4jURI, AuthTokens.Basic(ServerSettings.dbUser, ServerSettings.dbPassword));
        var query = @"
            MATCH (ingredient:Ingredient {name: $ingredientName})
            MATCH (:ShoppingList)-[x:PLANS_TO_BUY]->(ingredient)
            SET x.checked = true
            RETURN true
            ";

        var ingredientName = username + ingredient;

        var session = driver.AsyncSession();
        try
        {
            var response = await session.RunAsync(query, new { ingredientName });

            IReadOnlyList<IRecord> records = await response.ToListAsync();

            // Checks if there is a record && gets the first record which should be the bool response
            bool shopListChecked = records.Any() && records.First()[0].As<bool>();
            Console.WriteLine("Returning Result: " + shopListChecked);
            return shopListChecked;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return false;
        }
        finally
        {
            await session.CloseAsync();
        }
    }

    public static async Task<bool> UncheckShoppingListItem(string username, string ingredient)
    {
        using var driver = GraphDatabase.Driver(ServerSettings.neo4jURI, AuthTokens.Basic(ServerSettings.dbUser, ServerSettings.dbPassword));
        var query = @"
            MATCH (ingredient:Ingredient {name: $ingredientName})
            MATCH (:ShoppingList)-[x:PLANS_TO_BUY]->(ingredient)
            SET x.checked = false
            RETURN false
            ";

        var ingredientName = username + ingredient;

        var session = driver.AsyncSession();
        try
        {
            var response = await session.RunAsync(query, new { ingredientName });

            IReadOnlyList<IRecord> records = await response.ToListAsync();

            // Checks if there is a record && gets the first record which should be the bool response
            bool shopListChecked = records.Any() && records.First()[0].As<bool>();
            Console.WriteLine("Returning Result: " + shopListChecked);
            return !shopListChecked;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return false;
        }
        finally
        {
            await session.CloseAsync();
        }
    }

    public static async Task<List<IngredientDetail>> GetShoppingList(string username)
    {
        using var driver = GraphDatabase.Driver(ServerSettings.neo4jURI, AuthTokens.Basic(ServerSettings.dbUser, ServerSettings.dbPassword));
        var query = @"
        MATCH (user:User {username: $username})
        MATCH (user)-[]->(n:Ingredient)-[r]-(n2:ShoppingList)
        RETURN n.name AS ingredient,
               r.unit AS unit,
               r.quantity AS quantity,
               r.qualifier AS qualifier,
               r.checked AS checked
    ";

        // Initialize the Neo4j session
        var session = driver.AsyncSession();
        List<IngredientDetail> ingDList = new List<IngredientDetail>();

        try
        {
            // Run the query and pass in parameters
            var result = await session.RunAsync(query, new { username });

            // Process each record in the result
            await result.ForEachAsync(record =>
            {
                // Map the result to IngredientDetail properties
                IngredientDetail ingD = new IngredientDetail
                {
                    Name = GetCleanString(username, record["ingredient"]?.As<string>() ?? string.Empty),
                    Unit = record["unit"]?.As<string>() ?? string.Empty,
                    Quantity = record["quantity"]?.As<double>() ?? 0.0,
                    Qualifier = record["qualifier"]?.As<string>() ?? string.Empty,
                    isChecked = record["checked"].As<bool>()
                };

                // Add each ingredient detail to the list
                ingDList.Add(ingD);
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return new List<IngredientDetail>();
        }
        finally
        {
            // Ensures the session is closed
            await session.CloseAsync();
        }

        return ingDList;
    }

    public static async Task<IngredientDetail> GetShoppingListIngredient(string username, string ingName)
    {
        using var driver = GraphDatabase.Driver(ServerSettings.neo4jURI, AuthTokens.Basic(ServerSettings.dbUser, ServerSettings.dbPassword));
        var query = @"
        MATCH (:User {username: $username})-[]->(:ShoppingList)-[x:PLANS_TO_BUY]->(i:Ingredient)
        WHERE i.name = $ingredient
        RETURN x
        ";


        // Initialize the Neo4j session
        var session = driver.AsyncSession();
        string ingredient = username + ingName;
        IngredientDetail ingD = new IngredientDetail();
        ingD.Ingredient.Name = ingName;
        ingD.Name = ingName;

        try
        {
            // Run the query and pass in parameters
            var result = await session.RunAsync(query, new { username, ingredient });

            // Process each record in the result
            await result.ForEachAsync(record =>
            {
                var iNode = record["x"].As<INode>();
                ingD.Quantity = iNode["quantity"].As<double>();
                ingD.Unit = iNode["unit"].As<string>();
                ingD.Qualifier = iNode["qualifier"].As<string>();
            });
        }
        catch (Exception e)
        {
            Console.WriteLine("Error in DBQueryModel.GetPantryIngredient. Exception: " + e);
            return new IngredientDetail();
        }
        finally
        {
            // Ensures the session is closed
            await session.CloseAsync();
        }
        return ingD;
    }



    //TESTING
    private static async Task<bool> ValidateGroupMembership(string group, AuthToken at)
    {
        using var driver = GraphDatabase.Driver(ServerSettings.neo4jURI, AuthTokens.Basic(ServerSettings.dbUser, ServerSettings.dbPassword));
        string query = "MATCH (u:User {name:'" + at.username + "'})-[r:MEMBER_OF]->(b:Group {name: " + group + "})\n " +
                                                "return Count(u) > 0\n";
        try
        {
            var response = await driver.ExecutableQuery(query).WithConfig(qConf).ExecuteAsync();
            IReadOnlyList<IRecord> irol = response.Result;
            var record = irol.First<IRecord>();
            return record[0].As<bool>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return false;
        }
        finally
        {
            // Ensures the session is closed
            await driver.DisposeAsync();
        }
    }

    /*
    private static string SanitizeQueryParameters(string s)
    {
        return s.Replace("'", "''");
    }
    */

    public static List<string> GetCleanList(string user, List<string> list)
    {
        List<string> cleanedList = new List<string>();

        foreach (var item in list)
        {
            if (item.StartsWith(user))
            {
                string itemName = item.Substring(user.Length);
                cleanedList.Add(itemName);
            }
        }
        return cleanedList;
    }

    // id is the part you want you remove (it's usually just username but it may be recipe)
    // ex. "jeff123BananaBreadStep1"
    public static string GetCleanString(string id, string item)
    {
        if (item.StartsWith(id))
        {
            return item.Substring(id.Length);
        }
        return item; // Return the original string if no match
    }

    public static async Task<bool> CheckUserExistence(string username)
    {
        using var driver = GraphDatabase.Driver(ServerSettings.neo4jURI, AuthTokens.Basic(ServerSettings.dbUser, ServerSettings.dbPassword));
        var query = @"
        MATCH (u:User {username: $username})
        RETURN Count(u) > 0 As answer;
        ";
        // Initialize the Neo4j session
        var session = driver.AsyncSession();
        try
        {
            // Run the query and pass in parameters
            var response = await session.RunAsync(query, new { username });

            // This should work because it's a single value return type.
            var result = await response.SingleAsync();
            return result["answer"].As<bool>();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error retrieving user. Exception: " + e);
            return false;
        }
        finally
        {
            // Ensures the session is closed
            await session.CloseAsync();
        }
    }

}
