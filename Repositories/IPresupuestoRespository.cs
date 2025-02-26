using EspacioModelos;

namespace EspacioRepositorios
{
    public interface IPresupuestoRespository
    {
        public void AltaPresupuesto(Presupuesto presupuesto);
        public List<Presupuesto> GetListaPresupuesto();
        public Presupuesto GetDetallePresupuesto(int idPresupuesto);
        public void AgregarProductoYCantidad(int idPresupuesto, int idProducto, int cantidad);
        public void EliminarPresupuesto(int idPresupuesto);
        public void ModificarProductosYaCargados(int idPresupuesto, int idProducto, int cantidadNueva);
    }
}