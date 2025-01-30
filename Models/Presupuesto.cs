using EspacioClientes;

namespace EspacioProductos
{
    public class Presupuesto
    {
        private int idPresupuesto;
        private Cliente cliente;
        private List<PresupuestoDetalle> detalle;
        public List<PresupuestoDetalle> Detalle { get => detalle;}
        public int IdPresupuesto { get => idPresupuesto;}
        public Cliente Cliente { get => cliente;}

        public Presupuesto()
        {
            detalle = new List<PresupuestoDetalle>();
        }
        public Presupuesto(int idPresupuesto,  Cliente cliente, List<PresupuestoDetalle> detalles)
        {
            this.idPresupuesto = idPresupuesto;
            this.cliente = cliente;
            detalle = detalles;
        }


        public double MontoPresupuesto()
        {   
            double montoTotal = 0;
            foreach(var presupuesto in detalle)
            {
                montoTotal += (double)(presupuesto.Producto.Precio * presupuesto.Cantidad);
            }
            return montoTotal;
        }

        public double MontoPresupuestoConIVA()
        {
            return MontoPresupuesto() * 1.21;
        }

        public int CantidadProductos()
        {
            return detalle.Count();
        }
    }
}