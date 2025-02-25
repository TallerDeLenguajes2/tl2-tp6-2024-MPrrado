namespace EspacioRepositorios
{
    public enum RolUSuario
    {
        Administrador = 1,
        Cliente = 2,
        UsuarioNoLogueado = 3
    }
    public class Usuario
    {

        public int idUsuario { get; set; }
        public string Nombre { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public RolUSuario Rol { get; set; }
        
        public Usuario()
        {
        }
    }
}