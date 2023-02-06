using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LeMaS.Controllers;
[Authorize(Roles = "Trainer")]

public class TrainerController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Tugas(int id)
    {
        ViewBag.title = "Tugas";
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
        ViewBag.title = "Kumpul Tugas";
        ViewBag.id = id;
        return View();
    }

    public IActionResult ListTugasKumpul(int id)
    {
        ViewBag.title = "Kumpul Tugas";
        ViewBag.id = id;
        return View();
    }

    public IActionResult Peserta(int id)
    {
        ViewBag.title = "Peserta";
        ViewBag.id = id;
        return View();
    }
}