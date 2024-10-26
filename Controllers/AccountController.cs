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
    public async Task<IActionResult> Login(LoginModel lm)
    {
        string username = lm.Username;
        string password = lm.Password;
        Console.WriteLine("Logging in with " + username + ", " + password);
        //TODO - Sanitize UI
        bool isAuthTokenValid = await DBQueryModel.Authenticate(username, password);
        if (isAuthTokenValid)
        {
            AuthToken at = new AuthToken(username);
            HttpContext.Session.SetString("authToken", JsonConvert.SerializeObject(at));
            return RedirectToAction("Private", "Home");
        }
        else
        {
            Console.WriteLine("Login Failed");
            //lm.Response = "Error, login failed.";
            return View();
            //HttpContext.Session.SetString("user", "ERROR");
            //return View(new AccountLoginVM { authValid = false });
        }
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateAccountModel cam)
    {
        //TODO - Sanitize UI
        Dictionary<string, string> userData = new Dictionary<string, string>();
        userData["username"] = cam.username;
        userData["password"] = cam.password;
        userData["name"] = cam.name;
        userData["email"] = cam.email;
        userData["phone"] = cam.phoneNumber;
        bool creationSuccess;
        try
        {
            creationSuccess = await DBQueryModel.CreateUserNode(userData);
            if (creationSuccess)
            {
                return Ok(new { message = "Account creation successful" });
            }
            else
            {
                return BadRequest(new { message = "Account creation failed" });
            }
        }
        catch
        {
            return BadRequest(new { message = "Database access failure" });
        }

    }

    public IActionResult Recovery()
    {
        return View();
    }


}
