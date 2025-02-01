using System.ComponentModel.DataAnnotations;

namespace EspacioViewModels
{
    public class ModifiacionPresupuestoSeleccionados
    {
        public int IdProducto {get;set;}
        [Required]
        [Range(0,int.MaxValue)]
        public int Cantidad {get;set;}
        public ModifiacionPresupuestoSeleccionados()
        {
        }
        public ModifiacionPresupuestoSeleccionados(int idProducto, int cantidad)
        {
            this.IdProducto = idProducto;
            this.Cantidad = cantidad;
        }
    }
}