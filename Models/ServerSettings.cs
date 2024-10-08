namespace RecipeBuilder.Models;

public class ServerSettings
{
    //These fields will be fed from an ini file adjacent to the server binary.
    //For now, they're placed directly in code.
    public static string neo4jURI = "bolt://localhost:7687";  //"bolt://localhost:7687";
    public static string dbUser = "neo4j";
    public static string dbPassword = "55QvQu95HG";
}
