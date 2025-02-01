using System.ComponentModel.DataAnnotations;

namespace EspacioClientes
{
    public class Cliente
    {
        private int clienteId;
        private string nombre;
        private string email;
        private string telefono;
     
        public int ClienteId { get => clienteId; set => clienteId = value; }
        [Required(ErrorMessage ="Debe ingresar su nombre")]
        public string Nombre { get => nombre; set => nombre = value; }
        [EmailAddress(ErrorMessage ="Ingrese un correo electrónico válido")]
        public string Email { get => email; set => email = value; }
        [Phone(ErrorMessage ="El número de teléfono no es válido")]
        public string Telefono { get => telefono; set => telefono = value; }
        public Cliente(){

        }
        public Cliente (int clienteId, string nombre, string email, string telefono)
        {
            this.clienteId = clienteId;
            this.nombre = nombre;
            this.email = email;
            this.telefono = telefono;
        }

    }
}
