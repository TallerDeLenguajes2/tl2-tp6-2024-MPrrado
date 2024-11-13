using EspacioRepositorios;
using Microsoft.AspNetCore.Mvc;

public class PresupuestosController : Controller
{
    private PresupuestoRepository presupuestoRepository;
    public PresupuestosController()
    {
        presupuestoRepository = new PresupuestoRepository();
    }

    public IActionResult Index()
    {
        return View();
    }
    
    [HttpGet]
    public IActionResult ListarPresupuestos()
    {
        return View(presupuestoRepository.GetListaPresupuesto());
    }
}