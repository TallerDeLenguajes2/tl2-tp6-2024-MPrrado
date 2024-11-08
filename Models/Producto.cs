using Microsoft.AspNetCore.Diagnostics;

namespace EspacioProductos
{
    public class Producto
    {
        private int idProducto;
        private string descripcion;
        private int precio;

        public string Descripcion { get => descripcion; set => descripcion = value; }
        public int Precio { get => precio; set => precio = value; }

        public Producto()
        {
        }
        public Producto(int idProducto, string descripcion, int precio)
        {
            this.idProducto = idProducto;
            this.descripcion = descripcion;
            this.precio = precio;
        }

        public void SetIdProducto(int idProducto)
        {
            this.idProducto = idProducto;
        }

        public int GetIdProducto()
        {
            return idProducto;
        }
    }
}