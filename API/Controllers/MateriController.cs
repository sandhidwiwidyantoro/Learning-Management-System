using API.Base;
using API.Models;
using API.Repositories.Data;
using API.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class MateriController : BaseController<MateriRepositories, Materi>
{
    private MateriRepositories _repositories;
	private readonly IWebHostEnvironment _webHostEnvironment;
	public MateriController(MateriRepositories repositories, IWebHostEnvironment webHostEnvironment) : base(repositories) {
        _repositories = repositories;
		_webHostEnvironment = webHostEnvironment;
	}

    [HttpGet]
    [Route("tokenkelas")]
    public ActionResult GetMateriByToken(int tokenkelas)
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
    //  [HttpPost("UploadMateri")]
    //  public IActionResult UploadFile(string namaMateri, string judul, string descMateri, string namaFile ,int tokenkelas, IFormFile files)
    //  {
    //      if (files == null)
    //          return BadRequest();
    //      string directoryPath = Path.Combine(_webHostEnvironment.ContentRootPath, "../LeMaS/wwwroot/Materi");

    //      string filePath = Path.Combine(directoryPath, files.FileName);
    //      using(var stream = new FileStream(filePath, FileMode.Create))
    //      {
    //	files.CopyTo(stream);
    //      }
    //      var nameFile = files.FileName;

    //Materi materi = new Materi {
    //          Id = 0,
    //          NamaMateri = namaMateri,
    //          Judul = judul,
    //          NamaFile = nameFile,
    //          DescMateri = descMateri,
    //          TokenKelas = tokenkelas
    //};
    //      try
    //      {
    //	var result = _repositories.UploadMateri(materi);
    //	return result == null
    //		? Ok(new { statusCode = 200, message = $"Data Gagal DI Upload !!" })
    //		: Ok(new { statusCode = 200, message = $"Data Berhasil Diupload", data = nameFile });
    //}
    //      catch
    //      {
    //	return BadRequest(new { statusCode = 500, message = "Something Wrong I Can Fell It !!" });
    //}
    //  }
    [HttpPost("UploadMateri")]
    public IActionResult UploadFile(IFormFile files)
    {
        if (files == null)
            return BadRequest();
        string directoryPath = Path.Combine(_webHostEnvironment.ContentRootPath, "../LeMaS/wwwroot/Materi");

        string filePath = Path.Combine(directoryPath, files.FileName);
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            files.CopyTo(stream);
        }
        return Ok("Upload Successfull");
    }
}
