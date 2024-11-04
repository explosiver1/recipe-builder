namespace RecipeBuilder.Models;
using Neo4j.Driver;

public class DBQueryModel
{

    private static IDriver driver;
    private static QueryConfig qConf;

    //Taken from the neo4j dotnet driver documentation
    //private static IDriver CreateDriverWithBasicAuth(string uri, string user, string password)
    //{
    //    return GraphDatabase.Driver(uri, AuthTokens.Basic(user, password));
    //}

    //static constructor for initialization.
    static DBQueryModel()
    {
        Console.WriteLine("Creating Driver...");
        driver = GraphDatabase.Driver(ServerSettings.neo4jURI, AuthTokens.Basic(ServerSettings.dbUser, ServerSettings.dbPassword));
        qConf = new QueryConfig(database: "neo4j");
        Console.WriteLine("Driver Created.");
    }

    //Optionals can be added as "type name = 'value'" in the argument list. Not specifying a default value makes it required.

    // CreateUser()
    public static async Task<bool> CreateUserNode(string username, string name, string email, string phone, string password)
    {
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
            return userCreated;
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

    // This is covered by the tags node
    // public static async Task<bool> CreateCuisineNode(string cuisine)
    // {
    //     var query = @"
    //         CREATE (cuisine:Cuisine {name: $cuisineName})
    //         ";

    //     var session = driver.AsyncSession();
    //     try
    //     {
    //         await session.RunAsync(query, new { cuisineName = cuisine });
    //         Console.WriteLine($"Cuisine node {cuisine} created!");
    //     }
    //     finally
    //     {
    //         await session.CloseAsync();
    //     }

    //     return true;
    // }

    // CreateRecipe()
    // TODO - Test results
    public static async Task<bool> CreateRecipeNode(string username, string recipe, string description = "", string rating = "", string difficulty = "", string servings = "", string servingsize = "")
    {

        Console.WriteLine("Entering CreateRecipeNode with parameters:\n" +
            "username: " + username + "\n" +
            "recipe: " + recipe + "\n" +
            "description: " + description + "\n" +
            "rating: " + rating + "\n" +
            "difficulty: " + difficulty + "\n" +
            "servings: " + servings + "\n" +
            "servingize: " + servingsize + "\n");

        var query = @"
            MATCH (user:User {username: $username})
            MERGE (recipe:Recipe {name: $recipeName})
            ON CREATE SET
                recipe.description = $description,
                recipe.rating = $rating,
                recipe.difficulty = $difficulty,
                recipe.servings = $servings,
                recipe.servingSize = $servingsize
            MERGE (user)-[x:OWNS]->(recipe)
            RETURN COUNT(x) > 0
        ";

        var recipeName = username + recipe;

        var session = driver.AsyncSession();
        try
        {
            var response = await session.RunAsync(query, new { username, recipeName, description, rating, difficulty, servings, servingsize });

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


    // GetRecipes()
    // Gets a list of all the recipe names a user owns
    // TODO - Add Group Parameter
    public static async Task<List<string>> GetRecipeNodeNames(string username)
    {
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
                recipeNames.Add(record["recipeName"].As<string>());
            });

            Console.WriteLine($"Found {recipeNames.Count} recipes for user {username}!");
        }
        finally
        {
            await session.CloseAsync();
        }

        return recipeNames;
    }

    // CreateIngredient()
    // TODO - return success/fail
    public static async Task<bool> CreateIngredientNode(string username, string ingredient)
    {
        var query = @"
            MATCH (user:User {name: $user})
            MERGE (ingredient:Ingredient {name: $ingredientName})
            MERGE (user)-(x:OWNS)->(ingredient)
            RETURN COUNT(x) > 0
            ";

        var ingredientName = username + ingredient;

        var session = driver.AsyncSession();
        try
        {
            var response = await session.RunAsync(query, new { ingredientName });
            Console.WriteLine($"Ingredient node {ingredient} created!");

            // Pulls all responses from query
            IReadOnlyList<IRecord> records = await response.ToListAsync();

            // Checks if there is a record && gets the first record which should be the bool response
            bool ingredientCreated = records.Any() && records.First()[0].As<bool>();
            Console.WriteLine("Returning Result: " + ingredientCreated);
            return ingredientCreated;
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

    //This was easier than making the other method do more because of the parameter injection on the query string.
    public static async Task<List<string>> GetRecipeNodeNamesByIngredient(string username, string ingredient)
    {
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

        return recipeNames;
    }


    // MergeIngredient()
    // TODO - Test results
    // Used when adding ingredients to Recipe, Shopping List, or Pantry (must create the nodes first)
    // The parent is the name of the node you are adding ingredients too
    public static async Task<bool> ConnectIngredientNode(string username, string parent, string ingredient)
    {
        string query;
        if (parent.Contains("Pantry"))
        {
            query = @"
            MATCH (parent:Pantry{name:$parentName})
            MATCH (ingredient:Ingredient{name:$ingredientName})
            MERGE (parent)-[x:STORES]->(ingredient)
            RETURN COUNT(x) > 0
            ";
        }
        else if (parent.Contains("ShoppingList"))
        {
            query = @"
            MATCH (parent:ShoppingList{name:$parentName})
            MATCH (ingredient:Ingredient{name:$ingredientName})
            MERGE (parent)-[x:PLANS_TO_BUY]->(ingredient)
            RETURN COUNT(x) > 0
            ";
        }
        else
        {
            query = @"
            MATCH (parent:Recipe{name:$parentName})
            MATCH (ingredient:Ingredient{name:$ingredientName})
            MERGE (parent)-[x:MADE_WITH]->(ingredient)
            RETURN COUNT(x) > 0
            ";
        }
        var parentName = username + parent;
        var ingredientName = username + ingredient;

        var session = driver.AsyncSession();
        try
        {
            var response = await session.RunAsync(query, new { parentName, ingredientName });
            Console.WriteLine($"Parent node {parentName} connected to {ingredient}!");

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

    // GetIngredients()
    // Gets a list of all the names of ingredients a user owns
    // Unsure if works with no ingredients
    public static async Task<List<string>> GetIngredientNodeNames(string username)
    {
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

        return ingredientNames;
    }

    // CreateCookbookNode()
    // TODO - Test no description input
    public static async Task<bool> CreateCookbookNode(string username, string name, string description = "")
    {
        var query = @"
            MATCH (user:User{username: $username})
            MERGE (cookbook:Cookbook {name: $cookbookName})
            ON CREATE SET
                cookbook.description = $description
            MERGE (user)-[x:OWNS]->(cookbook)
            RETURN COUNT(x) > 0
        ";

        var cookbookName = name;

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
    public static async Task<bool> CreateToolNode(string toolName)
    {
        var query = @"
            MERGE (tool:Tool {name: $toolName})
            RETURN COUNT(tool) > 0
        ";

        var session = driver.AsyncSession();
        try
        {
            var response = await session.RunAsync(query, new { toolName });
            Console.WriteLine($"Tool {toolName} created!");

            // Pulls all responses from query
            IReadOnlyList<IRecord> records = await response.ToListAsync();

            // Checks if there is a record && gets the first record which should be the bool response
            bool toolCreated = records.Any() && records.First()[0].As<bool>();
            Console.WriteLine("Returning Result: " + toolCreated);
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
    public static async Task<bool> CreateMealNode(string username, string meal, string date)
    {
        var query = @"
            MERGE (meal:Meal { name: $mealName})
            ON CREATE SET
                date: $date
            RETURN COUNT(meal) > 0
        ";

        var mealName = username + meal;

        var session = driver.AsyncSession();
        try
        {
            var response = await session.RunAsync(query, new { mealName, date });
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
        var query = @"
        MATCH (recipe:Recipe {name: $recipeName})
        MERGE (step:Step {name: $stepName})
        ON CREATE SET
            step.order = order: $order
            step.description = description: $description
        MERGE (recipe)-[x:HAS_STEP]->(step)
        RETURN COUNT(x) > 0
        ";

        var recipeName = username + recipe;
        var stepName = recipeName + "Step" + order;

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


    // CreateTagNode()
    // TODO - return success/fail
    // TODO - Match Recipe to User/Group First
    public static async Task<bool> CreateTagNode(string tag, string recipe, string username)
    {
        var query = @"
        MATCH (recipe:Recipe {name: $recipeName})
        MERGE (tag:Tag {name: $tag})
        MERGE (recipe)-[x:TAGGED_WITH]->(tag)
        RETURN COUNT(x) > 0
        ";

        var recipeName = username + recipe;

        var session = driver.AsyncSession();
        try
        {
            var response = await session.RunAsync(query, new { tag, recipeName });
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
        Console.WriteLine("Authenticating " + username + " with " + password);
        string query = "MATCH (u:User {username: '" + username + "', password: '" + password + "'}) \n"
                                + "WITH COUNT(u) > 0 as exists \n"
                                + "RETURN exists";
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

    // Playing around with new GetRecipe
    public static async Task<Recipe> GetRecipe(string username, string recipe, string tool = "", string tag = "")
    {
        var query = @"
            MATCH (recipe:Recipe {name: $recipeName})
            OPTIONAL MATCH (tool:Tool {name: $toolName})
            OPTIONAL MATCH (tag:Tag {name: $tagName})
            RETURN recipe.name AS recipeName,
                recipe.description AS description,
                recipe.servingSize AS servingSize,
                recipe.servings AS numServings,
                recipe.rating AS rating,
                recipe.difficulty AS difficulty,
                tool.name AS toolName,
                tag.name AS tagName
        ";

        var recipeName = username + recipe;
        var toolName = !string.IsNullOrEmpty(tool) ? username + tool : null;
        var tagName = !string.IsNullOrEmpty(tag) ? username + tag : null;

        // Initialize the Neo4j session
        var session = driver.AsyncSession();
        Recipe resultRecipe = null;

        try
        {
            // Run the query and pass in parameters
            var result = await session.RunAsync(query, new { recipeName, toolName, tagName });
            
            // Process each record in the result
            await result.ForEachAsync(record =>
            {
                // Initialize and populate a new Recipe object
                resultRecipe = new Recipe
                {
                    Name = record["recipeName"]?.As<string>() ?? string.Empty,
                    Description = record["description"]?.As<string>() ?? string.Empty,
                    servingSize = record["servingSize"]?.As<string>() ?? string.Empty,
                    numServings = record["numServings"]?.As<int>() ?? 0, // Default to 0 if null
                    Rating = record["rating"]?.As<int>() ?? 1,            // Default to 1 if null
                    Difficulty = record["difficulty"]?.As<int>() ?? 1,    // Default to 1 if null
                    Equipment = new List<string>(),
                    Tags = new List<string>()
                };

                // Add tool and tag names to Equipment and Tags lists if they are present
                var toolNameValue = record["toolName"]?.As<string>();
                if (!string.IsNullOrEmpty(toolNameValue))
                {
                    resultRecipe.Equipment.Add(toolNameValue);
                }

                var tagNameValue = record["tagName"]?.As<string>();
                if (!string.IsNullOrEmpty(tagNameValue))
                {
                    resultRecipe.Tags.Add(tagNameValue);
                }
            });
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
    public static async Task<Recipe> GetRecipe(string recName, AuthToken at, string ingredient = "", string group = "")
    {
        string name;
        string startLabel;
        if (group != "")
        {
            bool gTest = await ValidateGroupMembership(group, at);
            if (gTest)
            {
                name = group;
                startLabel = "Group";
            }
            else
            {
                Recipe r2 = new Recipe();
                r2.Name = "ERROR";
                return r2;
            }
        }
        else
        {
            name = recName;
            startLabel = "User";
        }
        string query = "MATCH  (:" + startLabel + " {name:'" + at.username + "'})-[:OWNS]->()-[:CATALOGUES]->(rec:Recipe {name:'" + name + "'})-[r]->(b)\n " +
                                    // "MATCH (rec)<-[tr]-(c)\n" +
                                    "return rec, r, b \n"; //tr, c\n";
        var response = await driver.ExecutableQuery(query).WithConfig(qConf).ExecuteAsync();
        IReadOnlyList<IRecord> irol = response.Result;
        // var record = irol.First();
        // var rNode = record["rec"].As<INode>();

        Recipe r = new Recipe();

        // The recipe node is on each record, so we're getting it's info on the first pass only.
        // The other passes are for getting data from relationships to the recipe.
        bool firstPass = true;
        foreach (var record in irol)
        {
            var recNode = record["rec"].As<INode>();
            var rNode = record["r"].As<INode>();
            var bNode = record["b"].As<INode>();
            if (firstPass)
            {
                // Try-Catching all of them independently since many fields are optional.
                try
                {
                    r.Name = recNode["name"].As<string>();
                }
                catch
                {
                    r.Name = "Error";
                }

                // try
                // {
                //     r.CookTime = recNode["cooktime"].As<int>();
                // }
                // catch
                // {
                //     r.CookTime = 0;
                // }

                try
                {
                    r.Difficulty = recNode["difficulty"].As<int>();
                }
                catch
                {
                    r.Difficulty = 0;
                }

                try
                {
                    r.Rating = recNode["rating"].As<int>();
                }
                catch
                {
                    r.Rating = 0;
                }
                try
                {
                    r.RecipeId = recNode["identity"].As<int>();
                }
                catch
                {
                    r.RecipeId = 0;
                }

                firstPass = false;
            }
            if (rNode != null)
            {
                switch (rNode["type"].As<string>())
                {
                    case "MADE_WITH":
                        IngredientDetail ingD;
                        try
                        {
                            Ingredient ing = new Ingredient();
                            ingD = new IngredientDetail();
                            ing.Name = bNode.Properties["name"].As<string>();
                            ingD.Unit = rNode.Properties["unit"].As<string>();
                            ingD.Quantity = rNode.Properties["quantity"].As<float>();
                            ingD.Ingredient = ing;
                        }
                        catch
                        {
                            Ingredient ing = new Ingredient();
                            ingD = new IngredientDetail();
                            ingD.Ingredient = ing;
                        }
                        r.AddIngredient(ingD);
                        break;
                    case "USES":
                        try
                        {
                            //r.AddTool(bNode.Properties["name"]);
                        }
                        catch
                        {
                            //Add a blank tool.
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        List<Tag> tags = await GetTags(name, at);
        List<string> tagNames = new List<string>();
        foreach (Tag tag in tags)
        {
            tagNames.Add(tag.Name);
        }
        r.Tags = tagNames;
        return r;
    }


    //TESTING
    public static async Task<bool> DeleteRecipe(string recName, AuthToken at, string group = "")
    {
        string name;
        string startLabel;
        if (group != "")
        {
            bool gTest = await ValidateGroupMembership(group, at);
            if (gTest)
            {
                name = group;
                startLabel = "Group";
            }
            else
            {
                return false;
            }
        }
        else
        {
            name = recName;
            startLabel = "User";
        }
        string query = "MATCH (:" + startLabel + " {name:'" + at.username + "'})-[:OWNS]->()-[:CATALOGUES]->(rec:Recipe {name:'" + name + "'})\n " +
            "DELETE rec \n" +
            "MATCH (:" + startLabel + " {name:'" + at.username + "'})-[:OWNS]->()-[:CATALOGUES]->(n:Recipe {name:'" + name + "'})\n " +
            "return NOT(Count(n) >0) ";
        var response = await driver.ExecutableQuery(query).WithConfig(qConf).ExecuteAsync();
        //response is EagerResult<IReadOnlyList<IRecord>>
        IReadOnlyList<IRecord> irol = response.Result; //The Deconstruct() method has several outbound parameters. Result is one of them, and it can be referenced like a property here. First time I've seen this kind of behavior.'
        var record = irol.First(); //This gets the first IRecord of the list. This should be the only one in this case.
        if (record[0] != null)
        {
            return record[0].As<bool>();
        }
        else
        {
            return false;
        }
    }

    //TESTING
    public static async Task<Cookbook> GetCookbook(string cbName, AuthToken at, string group = "")
    {
        string name;
        string startLabel;
        if (group != "")
        {
            bool gTest = await ValidateGroupMembership(group, at);
            if (gTest)
            {
                name = group;
                startLabel = "Group";
            }
            else
            {
                Cookbook cbk = new Cookbook();
                cbk.Title = "ERROR";
                return cbk;
            }
        }
        else
        {
            name = cbName;
            startLabel = "User";
        }
        string query = "MATCH (:" + startLabel + " {name:'" + at.username + "'})-[:OWNS]->(cb:Cookbook {name:'" + name + "'})-[r]->(b)\n " +
                                    "return cb, r, b\n";
        var response = await driver.ExecutableQuery(query).WithConfig(qConf).ExecuteAsync();
        IReadOnlyList<IRecord> irol = response.Result;
        Cookbook cb = new Cookbook();

        //The recipe node is on each record, so we're getting it's info on the first pass only.
        //The other passes are for getting data from relationships to the recipe.
        bool firstPass = true;
        foreach (var record in irol)
        {
            var cbNode = record["cb"].As<INode>();
            var rNode = record["r"].As<INode>();
            var bNode = record["b"].As<INode>();
            if (firstPass)
            {
                //Try-Catching all of them independently since many fields are optional.
                try
                {
                    cb.Title = cbNode["name"].As<string>();
                }
                catch
                {
                    cb.Title = "Error";
                }
                firstPass = false;
            }
            if (rNode != null)
            {
                switch (rNode["type"].As<string>())
                {
                    case "CATALOGUES":
                        Recipe r = new Recipe();
                        try
                        {
                            r.Name = rNode["name"].As<string>();
                        }
                        catch
                        {
                            r.Name = "ERROR";
                        }
                        cb.AddRecipe(r);
                        break;
                }
            }
        }


        return cb;
    }

    public static async Task<List<Cookbook>> GetCookbooks(AuthToken at, string group = "")
    {
        string name;
        string startLabel;
        List<Cookbook> cbks = new List<Cookbook>();
        if (group != "")
        {
            bool gTest = await ValidateGroupMembership(group, at);
            if (gTest)
            {
                name = group;
                startLabel = "Group";
            }
            else
            {
                //List<Cookbook> cbks = new List<Cookbook>();
                return cbks;
            }
        }
        else
        {
            name = at.username;
            startLabel = "User";
        }
        string query = "MATCH (:" + startLabel + " {username:'" + name + "'})-[:OWNS]->(cb:Cookbook)\n " +
                                    "return cb\n";
        var response = await driver.ExecutableQuery(query).WithConfig(qConf).ExecuteAsync();
        IReadOnlyList<IRecord> irol = response.Result;
        foreach (var record in irol)
        {
            var cbNode = record["cb"].As<INode>();
            //We only add the name because there are no
            Cookbook cb = new Cookbook();
            cb.Title = cbNode["name"].As<string>();
            cbks.Add(cb);
        }
        return cbks;
    }

    // Doesn't return bool in query response yet
    public static async Task<bool> EditCookBook(string username, string name, string description)
    {
        var query = @"
            MATCH (cookbook:Cookbook {name: $cookbookName})
            SET cookbook.description = $description

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
    public static async Task<bool> DeleteCookbook(string cbName, AuthToken at, string group = "")
    {
        string name;
        string startLabel;
        if (group != "")
        {
            bool gTest = await ValidateGroupMembership(group, at);
            if (gTest)
            {
                name = group;
                startLabel = "Group";
            }
            else
            {
                return false;
            }
        }
        else
        {
            name = cbName;
            startLabel = "User";
        }
        string query = "MATCH (:" + startLabel + " {username:'" + at.username + "'})-[:OWNS]->(cb:Cookbook {name:'" + name + "'})\n " +
                                    "WITH cb\n" +
                                    "DETACH DELETE cb\n" +
                                    "WITH cb\n" +
                        "MATCH (:" + startLabel + " {name:'" + at.username + "'})-[:OWNS]->(cbk:Cookbook {name:'" + name + "'})\n " +
                        "return NOT(Count(cbk) > 0)";
        var response = await driver.ExecutableQuery(query).WithConfig(qConf).ExecuteAsync();
        IReadOnlyList<IRecord> irol = response.Result;
        var record = irol.First<IRecord>();
        return record[0].As<bool>();
    }

    //WIP
    public static async Task<Ingredient> GetIngredient(string ingName, AuthToken at, string group = "")
    {
        string name;
        string startLabel;
        if (group != "")
        {
            bool gTest = await ValidateGroupMembership(group, at);
            if (gTest)
            {
                name = group;
                startLabel = "Group";
            }
            else
            {
                Ingredient ingerr = new Ingredient();
                return ingerr;
            }
        }
        else
        {
            name = ingName;
            startLabel = "User";
        }
        string query = "MATCH (:" + startLabel + ")-[r]->(:Ingredient {name: " + ingName + "})\n " +
                                        "return t\n";
        var response = await driver.ExecutableQuery(query).WithConfig(qConf).ExecuteAsync();
        IReadOnlyList<IRecord> irol = response.Result;
        var record = irol.First<IRecord>();
        Ingredient ing = new Ingredient();
        return ing;
    }

    //TESTING
    private static async Task<List<Tag>> GetTags(string recName, AuthToken at, string group = "")
    {
        string name;
        string startLabel;
        if (group != "")
        {
            bool gTest = await ValidateGroupMembership(group, at);
            if (gTest)
            {
                name = group;
                startLabel = "Group";
            }
            else
            {
                List<Tag> ltg = new List<Tag>();
                return ltg;
            }
        }
        else
        {
            name = recName;
            startLabel = "User";
        }
        string query = "MATCH (:" + startLabel + ")-[r]->(b:Recipe {name: " + recName + "})\n " +
                                        "return t\n";
        var response = await driver.ExecutableQuery(query).WithConfig(qConf).ExecuteAsync();
        IReadOnlyList<IRecord> irol = response.Result;
        List<Tag> lt = new List<Tag>();
        foreach (var record in irol)
        {
            Tag t = new Tag();
            var tNode = record["t"].As<INode>();
            try
            {
                t.Name = tNode["name"].As<string>();
            }
            catch
            {
                t.Name = "";
            }
            lt.Add(t);
        }
        return lt;
    }

    //TESTING
    private static async Task<bool> DeleteTag(string tagName, string recName, AuthToken at, string group = "")
    {
        string name;
        string startLabel;
        if (group != "")
        {
            bool gTest = await ValidateGroupMembership(group, at);
            if (gTest)
            {
                name = group;
                startLabel = "Group";
            }
            else
            {
                return false;
            }
        }
        else
        {
            name = tagName;
            startLabel = "User";
        }
        string query = "MATCH (:" + startLabel + ")-[:OWNS]->(:Cookbook)-[:CATALOGUES]->(:Recipe {name: " + recName + "})<-[:DESCRIBES]-(t:Tag)\n " +
                                            "DELETE t\n" +
                                            "MATCH (:User )-[:OWNS]->(:Cookbook)-[:CATALOGUES]->(:Recipe {name: " + recName + "})<-[:DESCRIBES]-(tt:Tag)\n " +
                                                                                "return NOT(COUNT(tt) > 0)\n";
        var response = await driver.ExecutableQuery(query).WithConfig(qConf).ExecuteAsync();
        IReadOnlyList<IRecord> irol = response.Result;
        var record = irol.First<IRecord>();
        return record[0].As<bool>();
    }

    //TESTING
    private static async Task<bool> ValidateGroupMembership(string group, AuthToken at)
    {
        string query = "MATCH (u:User {name:'" + at.username + "'})-[r:MEMBER_OF]->(b:Group {name: " + group + "})\n " +
                                                "return Count(u) > 0\n";
        var response = await driver.ExecutableQuery(query).WithConfig(qConf).ExecuteAsync();
        IReadOnlyList<IRecord> irol = response.Result;
        var record = irol.First<IRecord>();
        return record[0].As<bool>();
    }
}
