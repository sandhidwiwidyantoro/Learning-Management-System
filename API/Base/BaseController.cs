using API.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Base;
[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class BaseController<Repository, Entity> : ControllerBase where Entity : class where Repository : IRepositories<Entity>
{
    private Repository _repositories;

    public BaseController(Repository repositories) : base()
    {
        _repositories = repositories;
    }
	[HttpGet]
	public ActionResult GetAll()
    {
        try
        {
            var result = _repositories.Get();
            return result == null
                ? Ok(new { statusCode = 200, message = "Data Tidak Ditemukan !!" })
                : Ok(new { statusCode = 200, message = "Data Berhasil Diterima", data = result });
        }
        catch (Exception ex)
        {
            return BadRequest(new { statusCode = 500, message = "Something Wrong I Can Fell It !!"+ex.Message});
        }
    }

    [HttpGet]
    [Route("id")]
    public ActionResult GetById(int id)
    {
        try
        {
            var result = _repositories.Get(id);
            return result == null
                ? Ok(new { statusCode = 200, message = $"Data Id = {id} Tidak Ditemukan !!" })
                : Ok(new { statusCode = 200, message = $"Data {id} Berhasil Diterima", data = result });
        }
        catch
        {
            return BadRequest(new { statusCode = 500, message = "Something Wrong I Can Fell It !!" });
        }
    }

    [HttpPost]
    public ActionResult Insert(Entity entity)
    {
        try
        {
            var result = _repositories.Insert(entity);
            return result == null
                ? Ok(new { message = "Data Gagal Ditambahkan!" })
                : Ok(new { message = "Data Berhasil Ditambahkan!" });
        }
        catch
        {
            return BadRequest(new { message = "Failed To Insert Check Out Yout Property" });
        }
    }

    [HttpPut]
    public ActionResult Update(Entity entity)
    {
        try
        {
            var result = _repositories.Update(entity);
            return result == 0
                ? Ok(new { message = $"Id Tidak Ditemukan" })
                : Ok(new { message = "Data Berhasil Diubah!" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = "Something Wrong Sorry" + ex });
        }
    }

    [HttpDelete]
    public ActionResult Delete(int id)
    {
        try
        {
            var result = _repositories.Delete(id);
            Convert.ToString(result);
            return result == 0
                ? Ok(new { message = $"Id {id} Tidak Ditemukan" })
                : Ok(new { message = "Data Berhasil Dihapus" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = "Something Wrong Sorry" + ex });
        }
    }
}