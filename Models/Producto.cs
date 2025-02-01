using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Options;

namespace EspacioProductos
{
    public class Producto
    {
        private int idProducto;

        private string? descripcion;
        private int precio;
        
        [StringLength(250)]
        public string Descripcion { get => descripcion; set => descripcion = value; }
        
        [Required(ErrorMessage ="Debe ingresar el precio del producto.")]
        [Range(1,int.MaxValue, ErrorMessage ="El precio debe ser un valor positivo.")]
        public int Precio { get => precio; set => precio = value; }
        public int IdProducto { get => idProducto; set => idProducto = value;}

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