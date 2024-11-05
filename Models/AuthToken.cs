namespace RecipeBuilder.Models;
public class AuthToken
{

    public string username;
    public DateTime creation;
    public DateTime expiration;


    //This is only for the DBQueryModel to use. The DBQueryModel will return and validate AuthTokens for you.
    public AuthToken(string username)
    {
        this.username = username;
        creation = DateTime.UtcNow;
        expiration = creation.AddHours(0.5f); //30 minutes til token expires.
    }

    public bool Validate()
    {
        //Check if token is expired here.
        int test = DateTime.Compare(DateTime.Now, expiration);

        //Negative is earlier than, equal is same, greater is later than. Relationship of first parameter to second parameter.
        if (test >= 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

}
