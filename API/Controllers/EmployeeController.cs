using API.Models;
using API.Base;
using API.Repositories.Data;
using API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Controllers;

public class EmployeeController : BaseController<EmployeeRepositories, Employee>
{
    private EmployeeRepositories _repositories;
    private IConfiguration _con;
    public EmployeeController(EmployeeRepositories repositories, IConfiguration con) : base(repositories)
    {
        _repositories = repositories;
        _con = con;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("Login")]
    public ActionResult Login(LoginVM entity)
    {
        try
        {
            var result = _repositories.Login(entity);
            switch (result)
            {
                case 0:
                    return Ok(new { message = $"Akun Dengan Email : {entity.Email} Tidak Ditemukan" });
                case 1:
                    return BadRequest(new { message = "Password Yang Anda Masukkan Salah" });
                default:
                    var roles = _repositories.UserRoles(entity.Email);
                    var nik = _repositories.GetNIK(entity.Email);
                    var niks = Int32.Parse(nik);
                    var tokenkelas = 0;
                    var claims = new List<Claim>()
                    {
                        new Claim("email", entity.Email),
                        new Claim("role", roles),
                        new Claim("nik", nik)
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_con["JWT:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _con["JWT:Issuer"],
                        _con["JWT:Audience"],
                        claims,
                        expires: DateTime.Now.AddMinutes(5),
                        signingCredentials: signIn
                        );
                    var generateToken = new JwtSecurityTokenHandler().WriteToken(token);
                    claims.Add(new Claim("Token Security", generateToken.ToString()));

                    return Ok(new { statusCode = 200, message = "Login Success!", data = generateToken });
            }
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = "Something Wrong Sorry" + ex.Message });
        }
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("RegistrasiPeserta")]
    public ActionResult Regist(RegistrasiVM regis)
    {
        try
        {
            var result = _repositories.Registrasi(regis);
            if (result == 0)
            {
                return Ok(new { message = $"Email {regis.Email} Sudah Terdaftar ! Silahkan gunakan email lain" });
            }
            else if (result == 1)
            {
                return Ok(new { message = "Token Kelas Tidak Ditemukan" });
            }
            else if (result == 2)
            {
                return Ok(new { message = "Anda Berhasil Registrasi" });
            }
            return Ok(new { message = "Akun Tidak Bisa Diregistrasi" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = "Something Wrong Sorry" + ex.Message });
        }
    }

    [HttpGet]
    [Route("GetIdByRole")]
    public ActionResult DataByRole(Roles role)
    {
        try
        {
            var result = _repositories.GetByRole(role);
            return result == null
                ? Ok(new { statusCode = 200, message = "Samlekom" })
                : Ok(new { statusCode = 200, message = "", data = result });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = "Something Wrong Sorry" + ex.Message });
        }
    }
}
