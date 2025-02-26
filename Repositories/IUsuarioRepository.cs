using EspacioModelos;

namespace EspacioRepositorios
{
    public interface IUsuarioRepository
    {
        public void AltaUsuario(Usuario usuario);
        public void ModificarUsuario(int idUsuario, Usuario usuarioModificado);
        public Usuario GetUsuario(int idUsuario);
        public Usuario GetUsuarioPorNombreUsuario(string nombreUsuario);
        public List<Usuario> GetListadoUsuarios();
        public void EliminarUsuario(int idUsuario);
        public string EncriptarPassword(string password);
        public bool ComprobarPassword(string passwordIngresada, string passwordDb);
    }
}