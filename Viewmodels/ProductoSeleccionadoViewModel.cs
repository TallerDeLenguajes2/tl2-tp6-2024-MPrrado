using System.ComponentModel.DataAnnotations;

namespace EspacioViewModels
{
    public class ProductoSeleccionadoViewModel
    {

        public int IdProducto { get; set; }
        [Required]
        [Range(1,9999)]
        public int Cantidad { get; set; }
        public bool Seleccionado { get; set; } 
        public ProductoSeleccionadoViewModel()
        {
        }

        public ProductoSeleccionadoViewModel(int idProducto, int cantidad)
        {
            this.IdProducto = idProducto;
            this.Cantidad = cantidad;
        }
    }
        

}