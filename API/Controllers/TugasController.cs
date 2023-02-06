using API.Base;
using API.Models;
using API.Repositories.Data;
using API.Repositories.Interface;
using API.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class TugasController : BaseController<TugasRepositories, Tugas>
{
    private TugasRepositories _repositories;
	private readonly IWebHostEnvironment _webHostEnvironment;
	public TugasController(TugasRepositories repositories, IWebHostEnvironment webHostEnvironment) : base(repositories) { 
		_repositories = repositories;
		_webHostEnvironment = webHostEnvironment;
	}

    [HttpGet]
    [Route("tokenkelas")]
    public ActionResult GetTugasByToken(int tokenkelas)
    {
        try
        {
            var result = _repositories.GetByToken(tokenkelas);
            return result == null
                ? Ok(new { statusCode = 200, message = $"Data Id = {tokenkelas} Tidak Ditemukan !!" })
                : Ok(new { statusCode = 200, message = $"Data {tokenkelas} Berhasil Diterima", data = result });
        }
        catch
        {
            return BadRequest(new { statusCode = 500, message = "Something Wrong I Can Fell It !!" });
        }
    }

	[HttpPost("UploadTugas")]
	public IActionResult UploadFile(IFormFile files)
	{
		if (files == null)
			return BadRequest();
		string directoryPath = Path.Combine(_webHostEnvironment.ContentRootPath, "../LeMaS/wwwroot/Tugas");

		string filePath = Path.Combine(directoryPath, files.FileName);
		using (var stream = new FileStream(filePath, FileMode.Create))
		{
			files.CopyTo(stream);
		}
        return Ok("Upload Successfull");
    }
}
