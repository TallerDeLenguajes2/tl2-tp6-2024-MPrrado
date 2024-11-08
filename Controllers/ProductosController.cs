using EspacioRepositorios;
using Microsoft.AspNetCore.Mvc;

public class ProductosController : Controller
{
    private ProductoRepository productoRepository;
    public ProductosController()
    {
        productoRepository = new ProductoRepository();
    }

    public IActionResult Index()
    {
        return View(productoRepository.GetListaProductos());
    }

    public IActionResult ModificarProducto(int idProducto)
    {
        return View(productoRepository.GetProducto(idProducto));
    }
}