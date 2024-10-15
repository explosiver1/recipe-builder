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

    public async static Task<AuthToken> Authenticate(string username, string password)
    {
        /*
            using var session = driver.Session();
            return session.ExecuteRead(
                tx =>
                {
                    var result = tx.Run("MATCH (u:User {name: '$username', password: '$password'}) \n"
                        + "WITH COUNT(u) > 0 as exists \n"
                        + "RETURN exists", username, password);
                    if (result.Single()[0].As<bool>()) {
                        return AuthToken(username);
                    }
                }
            )
            */
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

    public async Task<Recipe> GetRecipe(string name, string cbName, AuthToken at, string group = "")
    {
        string query = "MATCH (rec:" + at.username + "Recipe {name:'" + name + "'})-[r]->(b)\n " +
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
            //var trNode = record["tr"].As<INode>();
            //var cNode = record["c"].As<INode>();
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
                            Ingredient ing = new Ingredient(
                                                bNode["identity"].As<int>(),
                                                bNode.Properties["name"].As<string>(),
                                                "",
                                                rNode.Properties["unit"].As<string>()
                                                );
                            rNode.Properties["quantity"].As<float>();
                            ingD = new IngredientDetail(ing, rNode.Properties["quantity"].As<float>(), ing.Unit);

                        }
                        catch
                        {
                            Ingredient ing = new Ingredient(0, "", "", "");
                            ingD = new IngredientDetail(ing, 0.0f, "None");
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
            //if (trNode != null)
            //{
            //   switch (trNode["type"].As<string>())
            //   {
            //
            //    }
            //}
        }

        //Create and run query on Tags. Or Call GetTags method.
        //Load names into List<string> on Recipe object.
        return r;
    }

    public async Task<bool> DeleteRecipe(string name, AuthToken at)
    {
        string query = "MATCH (rec:" + at.username + "Recipe {name:'" + name + "'})\n " +
            "DELETE rec \n" +
            "MATCH (n:" + at.username + "Recipe {name:'" + name + "'})\n " +
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

    public async Task<Cookbook> GetCookbook(string name, AuthToken at)
    {
        string query = "MATCH (cb:" + at.username + "Cookbook {name:'" + name + "'})-[r]->(b)\n " +
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

                try
                {
                    cb.CookbookId = cbNode["identity"].As<int>();
                }
                catch
                {
                    cb.CookbookId = 0;
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

        //Create and run query on Tags. Or Call GetTags method.
        //Load names into List<string> on Recipe object.
        return cb;
    }

    public async Task<List<Tag>> GetTags(string recipeName, AuthToken at)
    {
        string query = "MATCH (t:Tag)-[r]->(b:" + at.username + "Recipe {name: " + recipeName + "})\n " +
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

    public async Task<bool> DeleteCookbook(string name, AuthToken at)
    {
        return true;
    }
}
