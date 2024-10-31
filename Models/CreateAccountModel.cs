namespace RecipeBuilder.Models;

public class CreateAccountModel()
{
    public string username { get; set; }
    public string password { get; set; }
    public string email { get; set; }
    public string phoneNumber { get; set; }
    public string name { get; set; }

    public string msg {get; set;}
}
