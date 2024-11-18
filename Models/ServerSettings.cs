using System;
using System.Linq;
using System.Xml.Linq;
namespace RecipeBuilder.Models;

public class ServerSettings
{
    //These fields will be fed from an ini file adjacent to the server binary.
    //For now, they're placed directly in code.
    public static string neo4jURI = "";
    public static string dbUser = "";
    public static string dbPassword = "";

    static ServerSettings()
    {
        // Load the XML file
        try
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "settings.xml");
            Console.WriteLine("File Path: " + filePath);

            XDocument doc = XDocument.Load(filePath);

            // Query elements using LINQ
            XElement serverSettings = doc.Element("ServerCredentials")!;
            Console.WriteLine(serverSettings.Element("URI")!.Value);
            neo4jURI = serverSettings.Element("URI")!.Value;
            dbUser = serverSettings.Element("dbUser")!.Value;
            dbPassword = serverSettings.Element("dbPassword")!.Value;
        }
        catch (Exception e)
        {
            Console.WriteLine("settings.xml file could not be opened. Exception: " + e);
            neo4jURI = string.Empty;
            dbUser = string.Empty;
            dbPassword = string.Empty;
        }
    }
}
