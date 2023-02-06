using Microsoft.AspNetCore.Mvc;
using API.Base;
using API.Repositories.Data;
using API.Models;
using API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers;
public class BatchClassController : BaseController<BatchClassRepositories, BatchClass>
{
    private BatchClassRepositories _repositories;
    public BatchClassController(BatchClassRepositories repositories) : base(repositories) { _repositories = repositories; }

    [HttpGet]
    [Route("pic")]
    public ActionResult GetKelasByPIC(int nik)
    {
        try
        {
            var result = _repositories.GetByPIC(nik);
            return result == null
                ? Ok(new { statusCode = 200, message = $"Data Id = {nik} Tidak Ditemukan !!" })
                : Ok(new { statusCode = 200, message = $"Data {nik} Berhasil Diterima", data = result });
        }
        catch
        {
            return BadRequest(new { statusCode = 500, message = "Something Wrong I Can Fell It !!" });
        }
    }

    [HttpGet]
    [Route("participant")]
    public ActionResult GetParticipantPerKelas(int tokenkelas)
    {
        try
        {
            var result = _repositories.GetPesertaKelas(tokenkelas);
            return result == null
                ? Ok(new { statusCode = 200, message = $"Data Id = {tokenkelas} Tidak Ditemukan !!" })
                : Ok(new { statusCode = 200, message = $"Data {tokenkelas} Berhasil Diterima", data = result });
        }
        catch
        {
            return BadRequest(new { statusCode = 500, message = "Something Wrong I Can Fell It !!" });
        }
    }
}
