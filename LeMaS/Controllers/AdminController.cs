using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LeMaS.Controllers;
[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    public IActionResult TablesEmployee()
    {
        return View();
    }
    public IActionResult TablesBatchClass()
    {
        return View();
    }
}