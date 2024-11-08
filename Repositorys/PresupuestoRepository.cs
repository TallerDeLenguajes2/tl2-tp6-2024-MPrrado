using System.Net.Quic;
using EspacioProductos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using SQLitePCL;

namespace EspacioRepositorios
{
    internal interface IPresupuestoRespository
    {
        public void AltaPresupuesto(Presupuesto presupuesto);
        public List<Presupuesto> GetListaPresupuesto();
        public Presupuesto GetDetallePresupuesto(int idPresupuesto);
        public void AgregarProductoYCantidad(int idPresupuesto, int idProducto, int cantidad);
        public void EliminarPresupuesto(int idPresupuesto);
    }

    public class PresupuestoRepository : IPresupuestoRespository
    {
        private string cadenaConexion = "Data Source=db/Tienda.db;Cache=Shared";
        public void AgregarProductoYCantidad(int idPresupuesto, int idProducto, int cantidad)
        {
           
            using(SqliteConnection connection = new SqliteConnection(cadenaConexion))
            {
                connection.Open();
                string query = "INSERT INTO PresupuestosDetalle (idPresupuesto, idProducto,Cantidad) VALUES(@idPresupuesto,@idProducto,@Cantidad)";
                using(SqliteCommand command = new SqliteCommand(query,connection))
                {
                    command.Parameters.Add(new SqliteParameter("@idPresupuesto",idPresupuesto));
                    command.Parameters.Add(new SqliteParameter("@idProducto",idProducto));
                    command.Parameters.Add(new SqliteParameter("@Cantidad",cantidad));
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public void AltaPresupuesto(Presupuesto presupuesto)
        {
            using(SqliteConnection connection = new SqliteConnection(cadenaConexion))
            {
                connection.Open();
                string query = @"INSERT INTO Presupuestos (NombreDestinatario, FechaCreacion) VALUES(@NombreDestinatario, @FechaCreacion)";
                using(SqliteCommand command = new SqliteCommand(query,connection))
                {
                    command.Parameters.AddWithValue("@NombreDestinatario", presupuesto.NombreDestinatario);
                    command.Parameters.AddWithValue("@FechaCreacion", DateTime.Now.ToString("yyyy-MM-dd"));
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public void EliminarPresupuesto(int idPresupuesto)
        {
            throw new NotImplementedException();
        }

        public Presupuesto GetDetallePresupuesto(int idPresupuesto)
        {
            ProductoRepository productoRepositoryConsulta = new ProductoRepository();
            int idPresupuestoConsulta = 0;
            string nombreDestinatarioConsulta = "";

            List<PresupuestoDetalle> detalles = new List<PresupuestoDetalle>();
            using(SqliteConnection connection = new SqliteConnection(cadenaConexion))
            {
                connection.Open();
                string query = @"SELECT idPresupuesto, NombreDestinatario, idProducto, Cantidad FROM Presupuestos P
                        INNER JOIN PresupuestosDetalle PD USING(idPresupuesto)
                        WHERE P.idPresupuesto = @idPresupuesto;";
                using(SqliteCommand command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idPresupuesto",idPresupuesto);
                    using(SqliteDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            idPresupuestoConsulta = reader.GetInt32(0);
                            nombreDestinatarioConsulta = reader.GetString(1);
                            var productoPresupuesto = productoRepositoryConsulta.GetProducto(reader.GetInt32(2));
                            int cantidadConsulta = reader.GetInt32(3);
                            detalles.Add(new(productoPresupuesto, cantidadConsulta));
                        }
                    }
                }
                connection.Close();

            }
            Presupuesto presupuesto = new Presupuesto(idPresupuestoConsulta, nombreDestinatarioConsulta, detalles);
            return presupuesto;
        }

        public List<Presupuesto> GetListaPresupuesto()
        {
            List<Presupuesto> listaPresupuesto = new List<Presupuesto>();
            
            using(SqliteConnection connection = new SqliteConnection(cadenaConexion))
            {
                connection.Open();
                string query = "SELECT idPresupuesto, NombreDestinatario FROM Presupuestos";
                using(SqliteCommand command = new SqliteCommand(query,connection))
                {
                    using(SqliteDataReader reader1 = command.ExecuteReader())
                    {
                        while(reader1.Read())
                        {
                            int idPresupuesto = reader1.GetInt32(0);
                            string nombreDestinatario = reader1.GetString(1);
                            List<PresupuestoDetalle> detalles = new List<PresupuestoDetalle>();
                            //Todo lo necesario para obtener los productos de un PresupuestoDetalle dado un idPresupuesto
                            string queryDetalles = @"SELECT idProducto,Descripcion,Precio,Cantidad FROM PresupuestosDetalle
                            INNER JOIN Productos USING(idProducto)
                            WHERE idPresupuesto=@idPresupuesto";
                            var commandDetalles = new SqliteCommand(queryDetalles,connection);
                            commandDetalles.Parameters.AddWithValue("@idPresupuesto",idPresupuesto);
                            using(SqliteDataReader reader2 = commandDetalles.ExecuteReader())
                            {
                                while(reader2.Read())
                                {
                                    Producto productoDetalle = new Producto(reader2.GetInt32(0),reader2.GetString(1), reader2.GetInt32(2));
                                    detalles.Add(new(productoDetalle,reader2.GetInt32(3)));
                                }   
                            }
                            listaPresupuesto.Add(new(idPresupuesto,nombreDestinatario,detalles));
                        }

                    }
                }
                connection.Close();
            }
            return listaPresupuesto;
        }
    }
}