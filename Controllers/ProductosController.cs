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
    public IActionResult ModificarProducto(int idProducto)
    {
        return View(productoRepository.GetProducto(idProducto));
    }
}