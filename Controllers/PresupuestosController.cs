using EspacioProductos;
using EspacioRepositorios;
using Microsoft.AspNetCore.Mvc;

public class PresupuestosController : Controller
{
    private PresupuestoRepository presupuestoRepository;
    private ProductoRepository productoRepository;
    public PresupuestosController()
    {
        productoRepository = new ProductoRepository();
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
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult AgregarProducto(int idPresupuesto)
    {
        ViewBag.IdPresupuesto = idPresupuesto;
        var p = presupuestoRepository.GetListaPresupuesto().Find(p => p.IdPresupuesto == idPresupuesto);
        var listaDetalle = p.Detalle;
        var listaProductos = productoRepository.GetListaProductos();
        foreach(var detalle in listaDetalle)
        {
            if(listaProductos.Find(p => p.IdProducto == detalle.Producto.IdProducto) != null)
            {
                listaProductos.Remove(listaProductos.Find(p => p.IdProducto == detalle.Producto.IdProducto));
            }
        }
        return View(listaProductos); 
    }

    [HttpGet]
    public IActionResult AgregarCantidad(int idPresupuesto, int idProducto)
    {
        ViewBag.IdProducto = idProducto;
        ViewBag.IdPresupuesto = idPresupuesto;
        ViewBag.NombreProducto = productoRepository.GetListaProductos().Find(p => p.IdProducto == idProducto).Descripcion;
        return View(); 
    }

    [HttpPost]
    public IActionResult AgregarProductoYCantidad(int idPresupuesto, int idProducto, int cantidad)
    {
        if(presupuestoRepository.GetListaPresupuesto().Find(p => p.IdPresupuesto == idPresupuesto) != null && productoRepository.GetProducto(idProducto)!= null)
        {
            presupuestoRepository.AgregarProductoYCantidad(idPresupuesto, idProducto,cantidad);
            return RedirectToAction("Index");
        }else
        {
            return RedirectToAction("AgregarCantidad");
        }
    }

    [HttpGet]
    public IActionResult EliminarPresupuestoConfirmar(int idPresupuesto)
    {
        if(presupuestoRepository.GetListaPresupuesto().Find(p => p.IdPresupuesto == idPresupuesto) != null)
        {
            return View(presupuestoRepository.GetListaPresupuesto().Find(p => p.IdPresupuesto == idPresupuesto));
        }else
        {
            return View("Index");
        }
    }
    // [HttpDelete]
    public IActionResult EliminarPresupuestoDefinitivo(int idPresupuesto)
    {
        presupuestoRepository.EliminarPresupuesto(idPresupuesto);
        return RedirectToAction("Index");
    }
}


