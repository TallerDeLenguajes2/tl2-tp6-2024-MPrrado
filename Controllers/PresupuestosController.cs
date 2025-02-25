using System.Net.Mail;
using EspacioClientes;
using EspacioProductos;
using EspacioRepositorios;
using EspacioViewModels;
using Microsoft.AspNetCore.Mvc;

public class PresupuestosController : Controller
{
    private readonly IClienteRepository clienteRepository;
    private readonly IPresupuestoRespository presupuestoRespository;
    private readonly IProductoRepository productoRepository;

    public PresupuestosController(IClienteRepository clienteRepository, IPresupuestoRespository presupuestoRespository, IProductoRepository productoRepository)
    {
        this.clienteRepository = clienteRepository;
        this.presupuestoRespository = presupuestoRespository;
        this.productoRepository = productoRepository;
    }

    
    [HttpGet]
    public IActionResult Index()
    {
        return View(presupuestoRespository.GetListaPresupuesto());
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
        var xdd = presupuestoRespository.GetListaPresupuesto();
        var idNuevoPresupuesto = presupuestoRespository.GetListaPresupuesto().Max(p=>p.IdPresupuesto)+1;
        Presupuesto presupuestoCreado = new (idNuevoPresupuesto,clienteAlta,null);
        presupuestoRespository.AltaPresupuesto(presupuestoCreado);
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
                    presupuestoRespository.AgregarProductoYCantidad(IdPresupuesto, p.IdProducto, p.Cantidad);
                }
            }
            return RedirectToAction("Index");
        }
    }

    [HttpGet]
    public IActionResult EliminarPresupuestoConfirmar(int idPresupuesto)
    {
        if(presupuestoRespository.GetListaPresupuesto().Find(p => p.IdPresupuesto == idPresupuesto) != null)
        {
            return View(presupuestoRespository.GetListaPresupuesto().Find(p => p.IdPresupuesto == idPresupuesto));
        }else
        {
            return View("Index");
        }
    }

    // [HttpDelete]
    public IActionResult EliminarPresupuestoDefinitivo(int idPresupuesto)
    {
        presupuestoRespository.EliminarPresupuesto(idPresupuesto);
        return RedirectToAction("Index");
    }

    [HttpGet]

    public IActionResult ModificarCargadosPresupuesto(int idPresupuesto)
    {
        var presupuesto = presupuestoRespository.GetDetallePresupuesto(idPresupuesto);
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
                presupuestoRespository.ModificarProductosYaCargados(IdPresupuesto, x.IdProducto, x.Cantidad);
            }
            return RedirectToAction("AgregarProductoModificar",new{idPresupuesto = IdPresupuesto}); 
        }
    }

    [HttpGet]
     public IActionResult AgregarProductoModificar(int idPresupuesto)
    {
        var productosTotales = productoRepository.GetListaProductos();
        var presupuesto = presupuestoRespository.GetDetallePresupuesto(idPresupuesto);
        var productosNoSeleccionados = productosTotales.Where(p => !presupuesto.Detalle.Any(q => q.Producto.IdProducto == p.IdProducto)).ToList();
        var model = new ProductoViewModel(productosNoSeleccionados,idPresupuesto);
        return View(model); 
    }

    
}


