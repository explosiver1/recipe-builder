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
        driver = GraphDatabase.Driver(ServerSettings.neo4jURI, AuthTokens.Basic(ServerSettings.dbUser, ServerSettings.dbPassword));
        qConf = new QueryConfig(database: "neo4j");
    }

    //Optionals can be added as "type name = 'value'" in the argument list. Not specifying a default value makes it required.

    // CreateUser()
    // TODO - test results of query and return success/fail boolean
    public async Task<bool> CreateUserNode(Dictionary<string, string> userData)
    {
        var query = @"
        CREATE (u:User {
            username: $username,
            name: $name,
            email: $email,
            phone: $phone
        })
        ";

        // Run the query with the parameters
        var session = driver.AsyncSession();  // Open a session for Neo4j
        try
        {

            await session.RunAsync(query, new
            {
                username = userData["username"],
                name = userData["name"],
                email = userData["email"],
                phone = userData["phone"]
            });
            //TODO - test query results
            //TODO - return bool for success/fail
            Console.WriteLine($"User {userData["username"]} created!");
        }
        finally
        {
            await session.CloseAsync();  // Ensure session is closed after query
        }
        return true;
    }

    // CreatePantry()
    // TODO - return success/fail boolean
    public async Task<bool> CreatePantryNode(string username)
    {
        var query = @"
        MATCH (user:User{username: $user})
        CREATE (pantry:Pantry {name: $pantryName})
        CREATE (user)-[:OWNS]->(pantry)
        ";
        var pantryName = username + "Pantry";

        var session = driver.AsyncSession();
        try
        {
            await session.RunAsync(query, new { pantryName, user = username });
            Console.WriteLine($"Pantry node {pantryName} created!");
        }
        finally
        {
            await session.CloseAsync();
        }

        return true;
    }

    // CreateCuisine()
    // TODO - Return success/fail
    public async Task<bool> CreateCuisineNode(string cuisine)
    {
        var query = @"CREATE (cuisine:Cuisine {name: $cuisineName})";

        var session = driver.AsyncSession();
        try
        {
            await session.RunAsync(query, new { cuisineName = cuisine });
            Console.WriteLine($"Cuisine node {cuisine} created!");
        }
        finally
        {
            await session.CloseAsync();
        }

        return true;
    }

    // CreateRecipe()
    // TODO - return success/fail
    public async Task<bool> CreateRecipeNode(string username, Dictionary<string, string> recipeData)
    {
        var query = @"
        MATCH (user:User {username: $user})
        CREATE (recipe:Recipe {
            name: $recipeName,
            title: $title,
            description: $description
        })
        CREATE (user)-[:OWNS]->(recipe)
        ";

        var recipeName = username + recipeData["name"];

        var session = driver.AsyncSession();
        try
        {
            await session.RunAsync(query, new
            {
                user = username,
                recipeName,
                title = recipeData["title"],
                description = recipeData["description"]
            });
            Console.WriteLine($"Recipe {recipeName} created!");
        }
        finally
        {
            await session.CloseAsync();
        }

        return true;
    }

    // GetRecipes()
    // All Recipes a User Owns
    // TODO - Add Group Parameter
    public async Task<List<string>> GetRecipeNodeNames(string username)
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
    public async Task<bool> CreateIngredientNode(string username, string ingredient)
    {
        var query = @"CREATE (ingredient:Ingredient {name: $ingredientName})";

        var ingredientName = username + ingredient;

        var session = driver.AsyncSession();
        try
        {
            await session.RunAsync(query, new { ingredientName });
            Console.WriteLine($"Ingredient node {ingredient} created!");
        }
        finally
        {
            await session.CloseAsync();
        }

        return true;
    }

    // MergeIngredient()
    // TODO - return success/fail
    // What's going on with the parent parameter?
    public async Task<bool> ConnectIngredientNode(string username, string parent, string ingredient)
    {
        string query;
        if (parent.Contains("Pantry"))
        {
            query = @"
            MATCH (parent:Pantry{name:$parentName})
            MATCH (ingredient:Ingredient{name:$ingredientName})
            CREATE (parent)-[:STORES]->(ingredient)
            ";
        }
        else if (parent.Contains("ShoppingList"))
        {
            query = @"
            MATCH (parent:ShoppingList{name:$parentName})
            MATCH (ingredient:Ingredient{name:$ingredientName})
            CREATE (parent)-[:PLANS_TO_BUY]->(ingredient)
            ";
        }
        else
        {
            query = @"
            MATCH (parent:Recipe{name:$parentName})
            MATCH (ingredient:Ingredient{name:$ingredientName})
            CREATE (parent)-[:MADE_WITH]->(ingredient)
            ";
        }
        var parentName = username + parent;
        var ingredientName = username + ingredient;

        var session = driver.AsyncSession();
        try
        {
            await session.RunAsync(query, new { parentName, ingredientName });
            Console.WriteLine($"Parent node {parentName} connected to {ingredient}!");
        }
        finally
        {
            await session.CloseAsync();
        }

        return true;
    }

    // CreateCookbookNode()
    // TODO - return success/fail
    // TODO - Add Group Parameter
    public async Task<bool> CreateCookbookNode(string username, string name)
    {
        var query = @"
            MATCH (user:User{username: $user})
            CREATE (cookbook:Cookbook {name: $cookbookName})
            CREATE (user)-[:OWNS]->(cookbook)
        ";

        var cookbookName = username + name;

        var session = driver.AsyncSession();
        try
        {
            await session.RunAsync(query, new { cookbookName });
            Console.WriteLine($"Cookbook node {cookbookName} created!");
        }
        finally
        {
            await session.CloseAsync();
        }

        return true;
    }

    // CreateTool()
    // TODO - return success/fail
    // TODO - Match and Connect to User Node
    public async Task<bool> CreateToolNode(string toolName)
    {
        var query = @"CREATE (tool:Tool {name: $toolName})";

        var session = driver.AsyncSession();
        try
        {
            await session.RunAsync(query, new { toolName });
            Console.WriteLine($"Tool {toolName} created!");
        }
        finally
        {
            await session.CloseAsync();
        }

        return true;
    }

    // CreateGroup()
    // TODO - return success/fail
    // TODO - Take in Username and Connect as Owner
    public async Task<bool> CreateGroupNode(string group)
    {
        var query = @"CREATE (group:Group {name: $groupName})";

        var session = driver.AsyncSession();
        try
        {
            await session.RunAsync(query, new { groupName = group });
            Console.WriteLine($"Group node {group} created!");
        }
        finally
        {
            await session.CloseAsync();
        }
        return true;
    }

    // CreateShopListNode()
    // TODO - return success/fail
    // TODO - Match and Connect to User Node
    public async Task<bool> CreateShoppingListNode(string username)
    {
        var query = @"CREATE (shoppinglist:ShoppingList {name: $shoppinglistName})";
        var shoppingListName = username + "ShoppingList";

        var session = driver.AsyncSession();
        try
        {
            await session.RunAsync(query, new { shoppinglistName = shoppingListName });
            Console.WriteLine($"Shopping List node {shoppingListName} created!");
        }
        finally
        {
            await session.CloseAsync();
        }

        return true;
    }

    // CreateMealNode()
    // TODO - return success/fail
    // TODO - Match to User/Group First
    public async Task<bool> CreateMealNode(string username, string meal)
    {
        var query = @"
        CREATE (meal:Meal {
            name: $mealName,
            title: $meal
        })
        ";
        var mealName = username + meal;

        var session = driver.AsyncSession();
        try
        {
            await session.RunAsync(query, new { mealName, meal });
            Console.WriteLine($"Meal node {mealName} created!");
        }
        finally
        {
            await session.CloseAsync();
        }

        return true;
    }

    // MergeMealNode()
    // TODO - return success/fail
    // TODO - Match to User/Group First
    public async Task<bool> ConnectMealNode(string username, string meal, string recipe)
    {
        var query = @"
        MATCH (meal:Meal{name:$mealName})
        MATCH (recipe:Recipe{name:$recipeName})
        CREATE (meal)-[:MADE_WITH]->(recipe)
        ";
        var mealName = username + meal;
        var recipeName = username + recipe;

        var session = driver.AsyncSession();
        try
        {
            await session.RunAsync(query, new { mealName, recipeName });
            Console.WriteLine($"Meal node {mealName} connected to {recipe}!");
        }
        finally
        {
            await session.CloseAsync();
        }

        return true;
    }

    // CreateStepNode()
    // TODO - success/fail
    // TODO - Match to User/Group First
    public async Task<bool> CreateStepNode(string username, Dictionary<string, object> stepData, string recipe)
    {
        var query = @"
        MATCH (recipe:Recipe {name: $recipeName})
        CREATE (step:Step {
            description: $description,
            step_number: $stepNumber
        })
        CREATE (recipe)-[:HAS_STEP]->(step)
        ";

        var recipeName = username + recipe;

        var session = driver.AsyncSession();
        try
        {
            await session.RunAsync(query, new
            {
                description = stepData["description"],
                stepNumber = stepData["step_number"],
                recipeName
            });
            Console.WriteLine($"Step {stepData["step_number"]} created for {recipeName}!");
        }
        finally
        {
            await session.CloseAsync();
        }
        return true;
    }

    // CreateTagNode()
    // TODO - return success/fail
    // TODO - Match Recipe to User/Group First
    public async Task<bool> CreateTagNode(string tag, string recipe, string username)
    {
        var query = @"
        MATCH (recipe:Recipe {name: $recipeName})
        CREATE (tag:Tag {name: $tagName})
        CREATE (recipe)-[:TAGGED_WITH]->(tag)
        ";

        var recipeName = username + recipe;

        var session = driver.AsyncSession();
        try
        {
            await session.RunAsync(query, new { tagName = tag, recipeName });
            Console.WriteLine($"Tag {tag} created and connected to {recipeName}!");
        }
        finally
        {
            await session.CloseAsync();
        }

        return true;
    }


    //DONE
    public async static Task<bool> Authenticate(string username, string password)
    {
        string query = "MATCH (u:User {name: '" + username + "', password: '" + password + "'}) \n"
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
            return false;
        }
    }

    //TESTING
    public async Task<Recipe> GetRecipe(string recName, AuthToken at, string group = "")
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
                                    //"MATCH (rec)<-[tr]-(c)\n" +
                                    "return rec, r, b \n"; //tr, c\n";
        var response = await driver.ExecutableQuery(query).WithConfig(qConf).ExecuteAsync();
        IReadOnlyList<IRecord> irol = response.Result;
        //var record = irol.First();
        //var rNode = record["rec"].As<INode>();

        Recipe r = new Recipe();

        //The recipe node is on each record, so we're getting it's info on the first pass only.
        //The other passes are for getting data from relationships to the recipe.
        bool firstPass = true;
        foreach (var record in irol)
        {
            var recNode = record["rec"].As<INode>();
            var rNode = record["r"].As<INode>();
            var bNode = record["b"].As<INode>();
            if (firstPass)
            {
                //Try-Catching all of them independently since many fields are optional.
                try
                {
                    r.Name = recNode["name"].As<string>();
                }
                catch
                {
                    r.Name = "Error";
                }

                try
                {
                    r.CookTime = recNode["cooktime"].As<int>();
                }
                catch
                {
                    r.CookTime = 0;
                }

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
    public async Task<bool> DeleteRecipe(string recName, AuthToken at, string group = "")
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
    public async Task<Cookbook> GetCookbook(string cbName, AuthToken at, string group = "")
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

    //IN PROGRESS
    public async Task<bool> DeleteCookbook(string cbName, AuthToken at, string group = "")
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
        string query = "MATCH (:" + startLabel + " {name:'" + at.username + "'})-[:OWNS]->(cb:Cookbook {name:'" + name + "'})\n " +
                                    "DELETE cb\n" +
                        "MATCH (:" + startLabel + " {name:'" + at.username + "'})-[:OWNS]->(cbk:Cookbook {name:'" + name + "'})\n " +
                        "return NOT(Count(cbk) > 0)";
        var response = await driver.ExecutableQuery(query).WithConfig(qConf).ExecuteAsync();
        IReadOnlyList<IRecord> irol = response.Result;
        var record = irol.First<IRecord>();
        return record[0].As<bool>();
    }

    //WIP
    public async Task<Ingredient> GetIngredient(string ingName, AuthToken at, string group = "")
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
    private async Task<List<Tag>> GetTags(string recName, AuthToken at, string group = "")
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
    private async Task<bool> DeleteTag(string tagName, string recName, AuthToken at, string group = "")
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
    private async Task<bool> ValidateGroupMembership(string group, AuthToken at)
    {
        string query = "MATCH (u:User {name:'" + at.username + "'})-[r:MEMBER_OF]->(b:Group {name: " + group + "})\n " +
                                                "return Count(u) > 0\n";
        var response = await driver.ExecutableQuery(query).WithConfig(qConf).ExecuteAsync();
        IReadOnlyList<IRecord> irol = response.Result;
        var record = irol.First<IRecord>();
        return record[0].As<bool>();
    }
}
