namespace EspacioRepositorios
{
    public interface IUsuarioRepository
    {
        public void AltaUsuario(Usuario usuario);
        public void ModificarUsuario(int idUsuario, Usuario usuarioModificado);
        public Usuario GetUsuario(int idUsuario);
        public List<Usuario> GetListadoUsuarios();
        public void EliminarUsuario(int idUsuario);
    }
}