using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RecipeBuilder.Models;
using RecipeBuilder.ViewModels;
using Newtonsoft.Json;


namespace RecipeBuilder.Controllers;

public class AccountController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Login()
    {
        //Authenticate(string username, string password)
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] string username, [FromBody] string password)
    {
        Console.WriteLine("Logging in with " + username + ", " + password);
        //TODO - Sanitize UI
        bool isAuthTokenValid = await DBQueryModel.Authenticate(username, password);
        if (isAuthTokenValid)
        {
            AuthToken at = new AuthToken(username);
            HttpContext.Session.SetString("authToken", JsonConvert.SerializeObject(at));
            return Ok(new { message = "Login Successful!" });
        }
        else
        {
            Console.WriteLine("Login Failed");
            return BadRequest(new { message = "Invalid username or password." });
            //HttpContext.Session.SetString("user", "ERROR");
            //return View(new AccountLoginVM { authValid = false });
        }
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(string username, string password, string name, string email, string phone)
    {
        //TODO - Sanitize UI
        Dictionary<string, string> userData = new Dictionary<string, string>();
        userData["username"] = username;
        userData["password"] = password;
        userData["name"] = name;
        userData["email"] = email;
        userData["phone"] = phone;
        bool creationSuccess = await DBQueryModel.CreateUserNode(userData);

        if (creationSuccess)
        {
            return Ok();
        }
        else
        {
            return Unauthorized();
        }
    }

    public IActionResult Recovery()
    {
        return View();
    }

}
