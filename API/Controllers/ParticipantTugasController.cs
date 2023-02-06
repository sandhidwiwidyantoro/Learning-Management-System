using API.Base;
using API.Models;
using API.Repositories.Data;
using API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace API.Controllers;

public class ParticipantTugasController : BaseController<ParticipantTugasRepositories, ParticipantTugas>
{
    private ParticipantTugasRepositories _repositories;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public ParticipantTugasController(ParticipantTugasRepositories repositories, IWebHostEnvironment webHostEnvironment) : base(repositories)
    {
        _repositories = repositories;
        _webHostEnvironment = webHostEnvironment;
    }

    [HttpPost("UploadKumpulTugas")]
    public IActionResult UploadFile(IFormFile files)
    {
        if (files == null)
            return BadRequest();
        string directoryPath = Path.Combine(_webHostEnvironment.ContentRootPath, "../LeMaS/wwwroot/ParticipantTugas");

        string filePath = Path.Combine(directoryPath, files.FileName);
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            files.CopyTo(stream);
        }

        return Ok("Upload Successfull");
    }

    [HttpGet]
    [Route("TugasBelum")]
    public ActionResult TugasBelum(int idpeserta, int tokenkelas)
    {
        try
        {
            var result = _repositories.TugasBelum(idpeserta, tokenkelas);

            return result == null
                ? Ok(new { statusCode = 200, message = "Samlekom" })
                : Ok(new { statusCode = 200, message = "", data = result });
        }
        catch (Exception ex)
        {
            return BadRequest(new { statusCode = 500, message = "Something Wrong I Can Fell It !!"+ex.Message});
        }

    }

    [HttpGet]
    [Route("TugasBeres")]
    public ActionResult TugasBeres(int idpeserta, int tokenkelas)
    {
        try
        {
            var result = _repositories.TugasBeres(idpeserta, tokenkelas);

            return result == null
                ? Ok(new { statusCode = 200, message = "Samlekom" })
                : Ok(new { statusCode = 200, message = "", data = result });
        }
        catch (Exception ex)
        {
            return BadRequest(new { statusCode = 500, message = "Something Wrong I Can Fell It !!" + ex.Message });
        }

    }

    [HttpGet]
    [Route("TugasByIdTugas")]
    public ActionResult GetTugasById(int idTugas)
    {
        try
        {
            var result = _repositories.GetByIdTugas(idTugas);

            return result == null
                ? Ok(new { statusCode = 200, message = "Samlekom" })
                : Ok(new { statusCode = 200, message = "", data = result });
        }
        catch (Exception ex)
        {
            return BadRequest(new { statusCode = 500, message = "Something Wrong I Can Fell It !!" + ex.Message });
        }

    }
}