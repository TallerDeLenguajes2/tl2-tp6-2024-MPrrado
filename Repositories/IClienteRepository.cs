using EspacioClientes;

namespace EspacioRepositorios
{
    public interface IClienteRepository
    {
        public void AltaCliente(Cliente cliente);
        public List<Cliente> GetListaCliente();
        public void ModificarCliente(Cliente clienteModificado);
        public void EliminarCliente(int idCliente);
    }
}