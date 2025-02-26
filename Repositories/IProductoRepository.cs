using EspacioModelos;

namespace EspacioRepositorios
{
    public interface IProductoRepository
    {
        public void AltaProducto(Producto producto);
        public void ModificarProducto(int idProducto, Producto producto);
        public List<Producto> GetListaProductos();
        public Producto GetProducto(int idProducto);
        public void EliminarProducto(int idProducto);
    }
}