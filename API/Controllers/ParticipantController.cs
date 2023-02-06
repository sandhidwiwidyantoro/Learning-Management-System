using API.Base;
using API.Models;
using API.Repositories.Data;
using API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ParticipantController : BaseController<ParticipantRepositories, Participant>
{
    private ParticipantRepositories _repositories;
    public ParticipantController(ParticipantRepositories repositories) : base(repositories) { _repositories = repositories; }

	[HttpGet]
	[Route("nik")]
	public ActionResult GetKelasByNIK(int nik)
	{
		try
		{
			var result = _repositories.GetByNIK(nik);
			return result == null
				? Ok(new { statusCode = 200, message = $"Data Id = {nik} Tidak Ditemukan !!" })
				: Ok(new { statusCode = 200, message = $"Data {nik} Berhasil Diterima", data = result });
		}
		catch
		{
			return BadRequest(new { statusCode = 500, message = "Something Wrong I Can Fell It !!" });
		}
	}
}
