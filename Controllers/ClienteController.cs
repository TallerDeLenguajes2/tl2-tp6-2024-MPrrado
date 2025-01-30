using EspacioClientes;
using EspacioRepositorios;
using Microsoft.AspNetCore.Mvc;

public class ClienteController : Controller
{
    private ClienteRepository repositorioCliente;
    public ClienteController()
    {
        repositorioCliente = new ClienteRepository();
    }

     [HttpGet]
    public IActionResult Index()
    {
        return View(repositorioCliente.GetListaCliente());
    }

    [HttpGet]
    public IActionResult AltaCliente()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AltaCliente(Cliente cliente) 
    {
        repositorioCliente.AltaCliente(cliente);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult ModificarCliente(int idCliente)
    {
        if(repositorioCliente.GetListaCliente().Find(p => p.ClienteId == idCliente)!=null)
        {
            var cliente = repositorioCliente.GetListaCliente().Find(p => p.ClienteId == idCliente);
            return View(cliente);
        }else
        {
            throw new Exception("NO SE ENCONTRO EL CLIENTE BUSCADO");
        }
    }

    [HttpPost]
    public IActionResult ModificarCliente(int idCliente, Cliente clienteModifcado)
    {
        repositorioCliente.ModificarCliente(idCliente,clienteModifcado);
        return RedirectToAction("Index");
    }

}