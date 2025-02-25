using EspacioRepositorios;
using EspacioViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EspacioController
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepository usuarioRepository;

        public LoginController(IUsuarioRepository usuarioRepository)
        {
            this.usuarioRepository = usuarioRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            
            if (HttpContext.Session.GetString("IsLoged") != "true")
            {
                string isAuthenticatedString = HttpContext.Session.GetString("IsAuthenticated");

                LoginViewModel modelo = new LoginViewModel
                {
                    IsAuthenticated = bool.TryParse(isAuthenticatedString, out bool result) ? result : false //esta linea nos garantiza que si HttpContext.Session.GetString("IsAuthenticated") es NULL no se podra parsear el valor y obtendremos false de todos modos!
                };

                return View(modelo);
            }
            else
            {
                return RedirectToAction("Index", "Tablero");
            }
            }
        [HttpPost]
        public IActionResult Login(LoginViewModel modelo)
        {
            
            if (string.IsNullOrEmpty(modelo.NombreUsuario) || string.IsNullOrEmpty(modelo.Password))
            {
                modelo.MensajeError = "Por favor ingrese su nombre de usuario y contraseña.";
                return View("Index", modelo);
            }

            Usuario usuario = usuarioRepository.GetUsuarioPorNombreUsuario(modelo.NombreUsuario);
            if (usuario.Nombre == null)
            {
                modelo.MensajeError = "ERROR: Usuario o contraseña incorrectos";
                return View("Index", modelo);
            }
            else
            {
                if (!usuarioRepository.(modelo.Password, usuario.Password))
                {
                    modelo.MensajeError = "ERROR: Usuario o contraseña incorrectos";
                    return View("Index", modelo);
                }
            }
            HttpContext.Session.SetString("IsAuthenticated", "true");
            HttpContext.Session.SetInt32("IdUsuario", usuario.IdUsuario);
            HttpContext.Session.SetString("Rol", usuario.Rol.ToString());
            HttpContext.Session.SetString("IsLoged", "true");
            HttpContext.Session.SetString("UserName", usuario.Nombre);
            return RedirectToAction("Index", "Tablero");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            
            HttpContext.Session.Clear();
            if (Request.Cookies.ContainsKey(".AspNetCore.Session"))
            {
                Response.Cookies.Delete(".AspNetCore.Session");
            }
            return RedirectToAction("Index");
        }
    }
}