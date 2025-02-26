using EspacioModelos;
using EspacioRepositorios;
using Microsoft.AspNetCore.Mvc;

public class ClienteController : AuthController
{
    private readonly IClienteRepository clienteRepository;

    public ClienteController(IClienteRepository clienteRepository)
    {
        this.clienteRepository = clienteRepository;
    }

     [HttpGet]
    public IActionResult Index()
    {
        if(!IsAuthenticated())return RedirectToAction("Index", "Login");
        if(GetRolUsuarioLogueado() != RolUsuario.Administrador) return RedirectToAction("Index", "Presupuestos");
        return View(clienteRepository.GetListaCliente());
    }

    [HttpGet]
    public IActionResult AltaCliente()
    {
        if(!IsAuthenticated())return RedirectToAction("Index", "Login");
        if(GetRolUsuarioLogueado() != RolUsuario.Administrador) return RedirectToAction("Index", "Presupuestos");
        return View();
    }

    [HttpPost]
    public IActionResult AltaCliente(Cliente cliente) 
    {
        if(!IsAuthenticated())return RedirectToAction("Index", "Login");
        if(GetRolUsuarioLogueado() != RolUsuario.Administrador) return RedirectToAction("Index", "Presupuestos");
        if(!ModelState.IsValid)
        {
            return RedirectToAction("AltaCliente");
        }else
        {
            clienteRepository.AltaCliente(cliente);
            return RedirectToAction("Index");
        }
    }

    [HttpGet]
    public IActionResult ModificarCliente(int idCliente)
    {
        if(!IsAuthenticated())return RedirectToAction("Index", "Login");
        if(GetRolUsuarioLogueado() != RolUsuario.Administrador) return RedirectToAction("Index", "Presupuestos");
        if(clienteRepository.GetListaCliente().Find(p => p.ClienteId == idCliente)!=null)
        {
            var cliente = clienteRepository.GetListaCliente().Find(p => p.ClienteId == idCliente);
            return View(cliente);
        }else
        {
            throw new Exception("NO SE ENCONTRO EL CLIENTE BUSCADO");
        }
    }

    [HttpPost]
    public IActionResult ModificarCliente(Cliente clienteModificado)
    {
        if(!IsAuthenticated())return RedirectToAction("Index", "Login");
        if(GetRolUsuarioLogueado() != RolUsuario.Administrador) return RedirectToAction("Index", "Presupuestos");
        clienteRepository.ModificarCliente(clienteModificado);
        return RedirectToAction("Index");
    }

    public IActionResult EliminarCliente(int idCliente)
    {
        if(!IsAuthenticated())return RedirectToAction("Index", "Login");
        if(GetRolUsuarioLogueado() != RolUsuario.Administrador) return RedirectToAction("Index", "Presupuestos");
        clienteRepository.EliminarCliente(idCliente);
        return RedirectToAction("Index");
    }

}