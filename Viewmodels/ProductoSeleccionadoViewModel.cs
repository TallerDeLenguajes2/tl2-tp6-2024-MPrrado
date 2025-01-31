namespace EspacioViewModels
{
    public class ProductoSeleccionadoViewModel
    {

        public int IdProducto { get; set; }
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