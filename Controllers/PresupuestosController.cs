using System.Net.Mail;
using EspacioClientes;
using EspacioProductos;
using EspacioRepositorios;
using EspacioViewModels;
using Microsoft.AspNetCore.Mvc;

public class PresupuestosController : Controller
{
    private PresupuestoRepository presupuestoRepository;
    private ProductoRepository productoRepository;
    private ClienteRepository clienteRepository;
    public PresupuestosController()
    {
        clienteRepository = new ClienteRepository();     
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
        var model = new PresupuestoViewModel(clienteRepository.GetListaCliente());
        return View(model);
    }

    [HttpPost]
    public IActionResult AltaPresupuesto(int idCliente)
    {
        Cliente clienteAlta = clienteRepository.GetListaCliente().Find(c => c.ClienteId == idCliente);
        return RedirectToAction("AgregarProducto", clienteAlta);
    }

    [HttpGet]
    public IActionResult AgregarProducto(Cliente Cliente)
    {

        return View(); 
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


