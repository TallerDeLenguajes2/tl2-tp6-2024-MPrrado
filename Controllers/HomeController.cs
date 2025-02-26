using System.Diagnostics;
using EspacioModelos;
using Microsoft.AspNetCore.Mvc;
using tl2_tp6_2024_MPrrado.Models;

namespace tl2_tp6_2024_MPrrado.Controllers;

public class HomeController : AuthController
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        if(!IsAuthenticated())return RedirectToAction("Index", "Login");
        if(GetRolUsuarioLogueado() != RolUsuario.Administrador) return RedirectToAction("Index", "Presupuestos");
        return View();
    }

    public IActionResult Privacy()
    {
        if(!IsAuthenticated())return RedirectToAction("Index", "Login");
        if(GetRolUsuarioLogueado() != RolUsuario.Administrador) return RedirectToAction("Index", "Presupuestos");
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
