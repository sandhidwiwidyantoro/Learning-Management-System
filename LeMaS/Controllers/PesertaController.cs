using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LeMaS.Controllers;
[Authorize(Roles = "Participant")]

public class PesertaController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult TugasBelum(int id)
    {
        ViewBag.title = "Tugas Belum Dikerjakan";
        ViewBag.id = id;
        return View();
    }
    public IActionResult Materi(int id)
    {
        ViewBag.title = "Materi";
        ViewBag.id = id;
        return View();
    }
    public IActionResult TugasKumpul(int id)
    {
        ViewBag.title = "Tugas Selesai Dikerjakan";
        ViewBag.id = id;
        return View();
    }
}
