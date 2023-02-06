using API.Models;
using API.ViewModels;
using Klien.Base;
using Klien.Repositories.Data;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace LeMaS.Controllers;

public class LoginController : BaseController<Employee, LoginRepository, int>
{
    private readonly LoginRepository repository;
    public LoginController(LoginRepository repository) : base(repository)
    {
        this.repository = repository;
    }

    [HttpPost]
    public async Task<IActionResult> Auth(LoginVM login)
    {
        var jwtToken = await repository.Auth(login);
        var token = jwtToken.data;

        if (token == null)
        {
            return RedirectToAction("index");
        }

        var claim = ExtractClaims(token);
        var role = claim.Where(claim => claim.Type == "role").Select(s => s.Value).Single();
        var nik = claim.Where(claim => claim.Type == "nik").Select(s => s.Value).Single();
        var email = claim.Where(claim => claim.Type == "email").Select(s => s.Value).Single();

        if (token == null)
        {
            return RedirectToAction("index");
        }

        HttpContext.Session.SetString("JWToken", token);
        HttpContext.Session.SetString("nik", nik);
        HttpContext.Session.SetString("email", email);



        if (role == "Trainer")
        {
            return RedirectToAction("index", "Trainer");
        }
        else if (role == "Admin")
        {
            return RedirectToAction("TablesEmployee", "Admin");
        }
        else
        {
            return RedirectToAction("index", "Peserta");
        }

        //HttpContext.Session.SetString("JWToken", token);
        //HttpContext.Session.SetString("Role", token);

        //return RedirectToAction("", "Trainer");
    }

    public IActionResult Index()
    {
        return View();
    }
    public IActionResult Register()
    {
        return View();
    }
    public ActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Login");
    }
    public IEnumerable<Claim> ExtractClaims(string jwtToken)
    {
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        JwtSecurityToken securityToken = (JwtSecurityToken)tokenHandler.ReadToken(jwtToken);
        IEnumerable<Claim> claims = securityToken.Claims;
        return claims;
    }
}