using EspacioProductos;
using EspacioRepositorios;
using Microsoft.AspNetCore.Mvc;
public class ProductosController : Controller
{
    private ProductoRepository productoRepository;
    public ProductosController()
    {
        productoRepository = new ProductoRepository();
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(productoRepository.GetListaProductos());
    }

    [HttpGet]
    public IActionResult AltaProducto()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AltaProducto(Producto producto)
    {
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
        return View(productoRepository.GetProducto(idProducto));
    }

    [HttpPost]
    public IActionResult ModificarProducto(Producto producto)
    {
        productoRepository.ModificarProducto(producto.IdProducto, producto);
        return RedirectToAction("Index");
    }

    [HttpGet] // porque no puedo dejar este atributo, si lo pongo me sale error  405
    public IActionResult EliminarProducto(int idProducto)
    {
        productoRepository.EliminarProducto(idProducto);
        return RedirectToAction("Index");
    }

}
