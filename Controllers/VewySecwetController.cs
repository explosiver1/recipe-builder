// using System.Diagnostics;
// using Microsoft.AspNetCore.Mvc;
// using RecipeBuilder.Models;
// using Newtonsoft.Json;

// namespace RecipeBuilder.Controllers;

// public class VewySecwetController : Controller
// {
//     public async Task<IActionResult> Index()
//     {

//         VewySecwetModel vsm = new VewySecwetModel();
//         try
//         {
//             AuthToken at = JsonConvert.DeserializeObject<AuthToken>(HttpContext.Session.GetString("auth"));
//             if (at != null)
//             {
//                 if (at.Validate())
//                 {

//                 }
//                 else
//                 {
//                     vsm.r.Name = "Auth Invalid";
//                 }
//             }
//             else
//             {
//                 vsm.r.Name = "Auth Token Not Found";
//             }

//         }
//         catch (Exception ex)
//         {
//             vsm.r.Name = "An error occurred during authentication: " + ex;
//         }
//         return View(vsm);
//     }
// }
