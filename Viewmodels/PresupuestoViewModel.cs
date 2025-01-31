using EspacioClientes;
using EspacioRepositorios;

namespace EspacioViewModels
{
    public class PresupuestoViewModel
    {
        private List<Cliente> listadoCliente;

        public PresupuestoViewModel()
        {
        }

        public PresupuestoViewModel(List<Cliente> listadoCliente)
        {
            this.listadoCliente = listadoCliente;
        }
        

        public List<Cliente> ListadoCliente { get => listadoCliente; set => listadoCliente = value; }
    }
}