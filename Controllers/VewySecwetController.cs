using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using recipe_builder.Models;

namespace recipe_builder.Controllers;

public class VewySecwetController : Controller
{
    public async Task<IActionResult> Index()
    {
        VewySecwetModel vsm = new VewySecwetModel();
        try
        {
            AuthToken at = await DBQueryModel.Authenticate("test_user", "test_password");
            if (at != null)
            {
                vsm.result = at.username;
            }
            else
            {
                vsm.result = "Auth Failure";
            }
        }
        catch (Exception ex)
        {
            vsm.result = "An error occurred during authentication: " + ex;
        }
        return View(vsm);
    }
}
