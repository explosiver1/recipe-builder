using System;
using System.Linq;
using System.Xml.Linq;
namespace RecipeBuilder.Models;

public class ServerSettings
{
    //These fields will be fed from an ini file adjacent to the server binary.
    //For now, they're placed directly in code.
    public static string neo4jURI = "bolt+s://recipes.zelpa.net:7687";  //"bolt://localhost:7687";
    public static string dbUser = "neo4j";
    public static string dbPassword = "55QvQu95HG";

    static ServerSettings()
    {

        //XML is super WIP. Idk how to use it well yet.

        // Load the XML file
        XDocument doc = XDocument.Load("settings.xml");

        // Query elements using LINQ
        var elements = from el in doc.Descendants("Credentials")
                       select el;

        // Loop through the elements
        foreach (var el in elements)
        {
            Console.WriteLine("Element: " + el.Name);
            Console.WriteLine("Value: " + el.Value);
        }
    }
}
