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

}
