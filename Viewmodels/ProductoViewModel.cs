using EspacioProductos;

namespace EspacioViewModels
{
    public class ProductoViewModel
    {
        private List<Producto> listadoProducto;
        private int idPresupuesto;

        public ProductoViewModel()
        {
        }

        public ProductoViewModel(List<Producto>listadoProducto, int idPresupuesto)
        {
            this.listadoProducto = listadoProducto;
            this.idPresupuesto = idPresupuesto;
        }

        public List<Producto> ListadoProducto { get => listadoProducto; set => listadoProducto = value; }
        public int IdPresupuesto { get => idPresupuesto; set => idPresupuesto = value; }
    }
}