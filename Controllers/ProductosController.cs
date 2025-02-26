using EspacioModelos;
using EspacioRepositorios;
using Microsoft.AspNetCore.Mvc;
public class ProductosController : AuthController
{
    private readonly IProductoRepository productoRepository;

    public ProductosController(IProductoRepository productoRepository)
    {
        this.productoRepository = productoRepository;
    }

    [HttpGet]
    public IActionResult Index()
    {
        if(!IsAuthenticated())return RedirectToAction("Index", "Login");
        if(GetRolUsuarioLogueado() != RolUsuario.Administrador) return RedirectToAction("Index", "Presupuestos");
        return View(productoRepository.GetListaProductos());
    }

    [HttpGet]
    public IActionResult AltaProducto()
    {
        if(!IsAuthenticated())return RedirectToAction("Index", "Login");
        if(GetRolUsuarioLogueado() != RolUsuario.Administrador) return RedirectToAction("Index", "Presupuestos");
        return View();
    }

    [HttpPost]
    public IActionResult AltaProducto(Producto producto)
    {
        if(!IsAuthenticated())return RedirectToAction("Index", "Login");
        if(GetRolUsuarioLogueado() != RolUsuario.Administrador) return RedirectToAction("Index", "Presupuestos");
        if(!ModelState.IsValid)
        {
            return View();
        }else
        {
            productoRepository.AltaProducto(producto);
            return RedirectToAction("Index");
        }
    }

    [HttpGet]
    public IActionResult ModificarProducto(int idProducto)
    {
        if(!IsAuthenticated())return RedirectToAction("Index", "Login");
        if(GetRolUsuarioLogueado() != RolUsuario.Administrador) return RedirectToAction("Index", "Presupuestos");
        return View(productoRepository.GetProducto(idProducto));
    }

    [HttpPost]
    public IActionResult ModificarProducto(Producto producto)
    {
        if(!IsAuthenticated())return RedirectToAction("Index", "Login");
        if(GetRolUsuarioLogueado() != RolUsuario.Administrador) return RedirectToAction("Index", "Presupuestos");
        productoRepository.ModificarProducto(producto.IdProducto, producto);
        return RedirectToAction("Index");
    }

    [HttpGet] // porque no puedo dejar este atributo, si lo pongo me sale error  405
    public IActionResult EliminarProducto(int idProducto)
    {
        if(!IsAuthenticated())return RedirectToAction("Index", "Login");
        if(GetRolUsuarioLogueado() != RolUsuario.Administrador) return RedirectToAction("Index", "Presupuestos");
        productoRepository.EliminarProducto(idProducto);
        return RedirectToAction("Index");
    }

}
