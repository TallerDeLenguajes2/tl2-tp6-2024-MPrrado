using EspacioProductos;
using EspacioRepositorios;
using Microsoft.AspNetCore.Mvc;

public class PresupuestosController : Controller
{
    private PresupuestoRepository presupuestoRepository;
    public PresupuestosController()
    {
        presupuestoRepository = new PresupuestoRepository();
    }

    
    [HttpGet]
    public IActionResult Index()
    {
        return View(presupuestoRepository.GetListaPresupuesto());
    }

    [HttpGet]
    public IActionResult AltaPresupuesto()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AltaPresupuesto(Presupuesto presupuesto)
    {
        presupuestoRepository.AltaPresupuesto(presupuesto);
        return RedirectToAction("ListarPresupuestos");
    }

    [HttpGet]
    public IActionResult AgregarProductoYCantidad()
    {
        return View(); 
    }
    [HttpPut]
    public IActionResult AgregarProductoYCantidad(int idPresupuesto, int idProducto, int cantidad)
    {
        ProductoRepository productoRepository = new ProductoRepository();
        if(presupuestoRepository.GetListaPresupuesto().Find(p => p.IdPresupuesto == idPresupuesto) != null && productoRepository.GetProducto(idProducto)!= null)
        {
            presupuestoRepository.AgregarProductoYCantidad(idPresupuesto, idProducto,cantidad);
            return RedirectToAction("ListarPresupuestos");
        }else
        {
            return RedirectToAction("Index");
        }
    }
}