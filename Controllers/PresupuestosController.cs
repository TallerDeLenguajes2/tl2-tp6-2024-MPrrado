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
        var idNuevoPresupuesto = presupuestoRepository.GetListaPresupuesto().Max(p=>p.IdPresupuesto)+1;
        Presupuesto presupuestoCreado = new (idNuevoPresupuesto,clienteAlta,null);
        presupuestoRepository.AltaPresupuesto(presupuestoCreado);
        return RedirectToAction("AgregarProducto", new{idPresupuesto = idNuevoPresupuesto});
    }

    [HttpGet]
    public IActionResult AgregarProducto(int idPresupuesto)
    {
        var model = new ProductoViewModel(productoRepository.GetListaProductos(),idPresupuesto);
        return View(model); 
    }

    [HttpPost]
    public IActionResult AgregarProductoYCantidad(List<ProductoSeleccionadoViewModel>listadoProductos, int IdPresupuesto)
    {
        if(listadoProductos == null || listadoProductos.Count()==0) //controlamos que no llegue vacia la eleccion de productos para el presupuesto
        {
            return RedirectToAction("AgregarProducto", new{idPresupuesto = IdPresupuesto});
        }else
        {
            foreach(var p in listadoProductos)
            {
                if(p.Seleccionado)
                {
                    presupuestoRepository.AgregarProductoYCantidad(IdPresupuesto, p.IdProducto, p.Cantidad);
                }
            }
            return RedirectToAction("Index");
        }
    }

    // [HttpPost]
    // public IActionResult AgregarProductoYCantidad(List<int>idProductos, List<int> cantidadProductos)
    // {

    //         return RedirectToAction("Index");
    // }

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


