using EspacioModelos;
using EspacioRepositorios;
using Microsoft.AspNetCore.Mvc;

public class AuthController : Controller
    {

        public AuthController()
        {
        }
        protected bool IsAuthenticated()
        {
            string isAuthenticatedString = HttpContext.Session.GetString("IsAuthenticated"); //misma linea de LoginController que nos garantiza el valor correcto para  HttpContext.Session.GetString("IsAuthenticated")
            return bool.TryParse(isAuthenticatedString, out bool result) ? result : false;
        }

        protected int GetIdUsuarioLogueado()
        {
            return HttpContext.Session.GetInt32("IdUsuario").Value;
        }
        protected RolUsuario GetRolUsuarioLogueado()
        {
            return ConvertirARolUsuario();
        }
        protected string GetNombreUsuarioLogueado()
        {
            return HttpContext.Session.GetString("UserName") ?? "No name"; //operador "??" es operador de fusion de nulos, si el valor de la izq no es nulo lo devuelve sino devuelve el de la derecha
        }


        private RolUsuario ConvertirARolUsuario() => (RolUsuario)Enum.Parse(typeof(RolUsuario), HttpContext.Session.GetString("Rol"));
    }