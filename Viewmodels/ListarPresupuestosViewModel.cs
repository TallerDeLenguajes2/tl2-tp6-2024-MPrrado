using EspacioModelos;

namespace EspacioViewModels
{
    public class ListarPresupuestosViewModel
    {
        public List<Presupuesto> ListadoPresupuestos {get;set;}
        public RolUsuario RolUsuarioLogueado {get;set;}
        public ListarPresupuestosViewModel()
        {
        }
        public ListarPresupuestosViewModel(List<Presupuesto> ListadoPresupuestos, RolUsuario RolUsuarioLogueado)
        {
            this.ListadoPresupuestos = ListadoPresupuestos;
            this.RolUsuarioLogueado = RolUsuarioLogueado;
        }
    }
}