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

    //The Merge methods must accept every property that may be held in the respective node and the necessary relationship properties as parameters.
    //Optionals can be added as "type name = 'value'" in the argument list. Not specifying a default value makes it required.

    //DONE
    public async static Task<AuthToken> Authenticate(string username, string password)
    {
        string query = "MATCH (u:User {name: '" + username + "', password: '" + password + "'}) \n"
                                + "WITH COUNT(u) > 0 as exists \n"
                                + "RETURN exists";
        var response = await driver.ExecutableQuery(query).WithConfig(qConf).ExecuteAsync();
        //response is EagerResult<IReadOnlyList<IRecord>>
        IReadOnlyList<IRecord> irol = response.Result; //The Deconstruct() method has several outbound parameters. Result is one of them, and it can be referenced like a property here. First time I've seen this kind of behavior.'
        var record = irol.First(); //This gets the first IRecord of the list. This should be the only one in this case.
        if (record[0] != null && record[0].As<bool>())
        {
            Console.WriteLine(Convert.ToString(record[0].As<bool>()));
            return new AuthToken(username);
        }
        else
        {
            return new AuthToken("ERROR");
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
