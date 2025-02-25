namespace EspacioRepositorios
{
    public enum RolUsuario
    {
        Administrador = 1,
        Cliente = 2,
        UsuarioNoLogueado = 3
    }
    public class Usuario
    {

        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public RolUsuario Rol { get; set; }

        public Usuario()
        {
        }
    }
}