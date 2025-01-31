namespace EspacioViewModels
{
    public class ModifiacionPresupuestoSeleccionados
    {
        public int IdProducto {get;set;}
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