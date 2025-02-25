
namespace EspacioRepositorios
{
    public class UsuarioRepository : IUsuarioRepository
    {

        //metodo para encriptar las constraseñas y comprobar contraseñas 
        public string EncriptarPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        public bool ComprobarPassword(string passwordIngresad, string passwordDb)
        {
            return BCrypt.Net.BCrypt.Verify(passwordIngresad, passwordDb);
        }

        public void AltaUsuario(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public void EliminarUsuario(int idUsuario)
        {
            throw new NotImplementedException();
        }

        public List<Usuario> GetListadoUsuarios()
        {
            throw new NotImplementedException();
        }

        public Usuario GetUsuario(int idUsuario)
        {
            throw new NotImplementedException();
        }

        public Usuario GetUsuarioPorNombreUsuario(string nombreUsuario)
        {
            throw new NotImplementedException();
        }

        public void ModificarUsuario(int idUsuario, Usuario usuarioModificado)
        {
            throw new NotImplementedException();
        }
    }
}