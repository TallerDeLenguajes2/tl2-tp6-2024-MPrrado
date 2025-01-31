using EspacioProductos;

namespace EspacioViewModels
{
    public class ProductoViewModel
    {
        private List<Producto> listadoProducto;

        public ProductoViewModel()
        {
        }

        public ProductoViewModel(List<Producto>listadoProducto)
        {
            this.listadoProducto = listadoProducto;
        }

        public List<Producto> ListadoProducto { get => listadoProducto; set => listadoProducto = value; }
    }
}