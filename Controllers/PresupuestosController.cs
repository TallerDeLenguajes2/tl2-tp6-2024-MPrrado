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
        var xdd = presupuestoRepository.GetListaPresupuesto();
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

    [HttpGet]

    public IActionResult ModificarCargadosPresupuesto(int idPresupuesto)
    {
        var presupuesto = presupuestoRepository.GetDetallePresupuesto(idPresupuesto);
        return View(presupuesto);
    }

    [HttpPost]
    public IActionResult ModificarCargadosPresupuesto(List<ModifiacionPresupuestoSeleccionados>listadoProductosSeleccionados, int IdPresupuesto)
    {
        if(listadoProductosSeleccionados == null || listadoProductosSeleccionados.Count()==0)
        {
            return RedirectToAction("AgregarProducto", new{idPresupuesto = IdPresupuesto});
        }else
        {
            foreach(var x in listadoProductosSeleccionados)
            {
                presupuestoRepository.ModificarProductosYaCargados(IdPresupuesto, x.IdProducto, x.Cantidad);
            }
            return RedirectToAction("AgregarProductoModificar",new{idPresupuesto = IdPresupuesto}); 
        }
    }

    [HttpGet]
     public IActionResult AgregarProductoModificar(int idPresupuesto)
    {
        var productosTotales = productoRepository.GetListaProductos();
        var presupuesto = presupuestoRepository.GetDetallePresupuesto(idPresupuesto);
        var productosNoSeleccionados = productosTotales.Where(p => !presupuesto.Detalle.Any(q => q.Producto.IdProducto == p.IdProducto)).ToList();
        var model = new ProductoViewModel(productosNoSeleccionados,idPresupuesto);
        return View(model); 
    }

    
}


