namespace EspacioClientes
{
    public class Cliente
    {
        private int clienteId;
        private string nombre;
        private string email;
        private string telefono;
        public Cliente (int clienteId, string nombre, string email, string telefono)
        {
            this.clienteId = clienteId;
            this.nombre = nombre;
            this.email = email;
            this.telefono = telefono;
        }

        public int ClienteId { get => clienteId;}
        public string Nombre { get => nombre;}
        public string Email { get => email;}
        public string Telefono { get => telefono;}
    }
}
