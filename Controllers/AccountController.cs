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
    public IActionResult Login(string msg = "")
    {
        //msg is status message. Needs to be added to ViewModel before rendering View.
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
        Console.WriteLine("Creating Account with parameters: \n" +
         "username: " + cam.username + "\n" +
         "password: " + cam.password + "\n" +
         "email: " + cam.email + "\n" +
         "phoneNumber: " + cam.phoneNumber + "\n" +
         "name: " + cam.name);
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
            creationSuccess = await DBQueryModel.CreateUserNode(username: userData["username"], name: userData["name"], email: userData["email"], phone: userData["phone"], password: userData["password"]);
            if (creationSuccess)
            {
                return RedirectToAction("Login");
            }
            else
            {
                return View();
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
