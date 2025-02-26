namespace EspacioModelos
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
        public Usuario(int IdUsuario, string Nombre, string UserName, string Password, RolUsuario Rol)
        {
            this.IdUsuario = IdUsuario;
            this.Nombre = Nombre;
            this.UserName = UserName;
            this.Password = Password;
            this.Rol = Rol;
        }
    }
}