using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RecipeBuilder.Models;
using RecipeBuilder.ViewModels;
using Newtonsoft.Json;
using System.Text.RegularExpressions;


namespace RecipeBuilder.Controllers;

public class AccountController : Controller
{
    // public IActionResult Index()
    // {
    //     return View();
    // }

    [HttpGet]
    public IActionResult Login(string msg = "")
    {
        //msg is status message. Needs to be added to ViewModel before rendering View.
        //Authenticate(string username, string password)
        if (msg != "")
        {
            LoginModel lm = new LoginModel();
            lm.msg = msg;
            return View(lm);
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginModel lm)
    {
        string username = lm.Username;
        string password = lm.Password;
        //Console.WriteLine("Logging in with " + username + ", " + password);
        //Sanitize UI
        if (username.Any(ch => !char.IsLetterOrDigit(ch)))
        {
            return Login("Error, username uses invalid characters.");
        }
        else if (password.Any(ch => !char.IsLetterOrDigit(ch)))
        {
            return Login("Error, password uses invalid characters.");
        }

        bool isAuthTokenValid = await DBQueryModel.Authenticate(username, password);
        if (isAuthTokenValid)
        {
            AuthToken at = new AuthToken(username);
            HttpContext.Session.SetString("authToken", JsonConvert.SerializeObject(at));
            return RedirectToAction("Index", "User");
        }
        else
        {
            Console.WriteLine("Login Failed");
            //lm.Response = "Error, login failed.";
            return Login("Login Failed");
            //HttpContext.Session.SetString("user", "ERROR");
            //return View(new AccountLoginVM { authValid = false });
        }
    }


    public IActionResult Create(string msg = "")
    {
        if (msg != "")
        {
            CreateAccountModel cam = new CreateAccountModel();
            cam.msg = msg;
            return View(cam);
        }
        else
        {
            return View();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateAccountModel cam)
    {
        if (DBQueryModel.CheckUserExistence(cam.username).Result)
        {
            return Create("Error, that username is already taken.");
        }
        Console.WriteLine("Creating Account with parameters: \n" +
         "username: " + cam.username + "\n" +
         "password: " + cam.password + "\n" +
         "email: " + cam.email + "\n" +
         "phoneNumber: " + cam.phoneNumber + "\n" +
         "name: " + cam.name);

        string userRegex = @"\w*@\w*\.\w{2,4}"; //@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

        //UI Sanitization
        if (cam.username.Any(ch => !char.IsLetterOrDigit(ch)))
        {
            return Create("Error, username uses invalid characters.");
        }
        else if (cam.username.Length < 3)
        {
            return Create("Error, username must be at least 3 characters.");
        }
        else if (cam.password.Any(ch => !char.IsLetterOrDigit(ch)))
        {
            return Create("Error, password uses invalid characters.");
        }
        else if (cam.password.Length < 8)
        {
            return Create("Error, password must be at least 8 characters");
        }
        else if (!Regex.Match(cam.email, userRegex).Success)
        {
            return Create("Error, email is invalid pattern");
        }
        else if (!Regex.Match(cam.phoneNumber, @"\d{10,11}").Success)
        {
            return Create("Error, phone number is invalid pattern. Must be 10 numbers with optional 1 digit region code.");
        }


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
                return Create("Account Creation Error");
            }
        }
        catch
        {
            return Create("Account Creation Error");
        }

    }

    // public IActionResult Recovery()
    // {
    //     return View();
    // }


}
