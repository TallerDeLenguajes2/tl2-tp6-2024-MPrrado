using System.Net.Quic;
using EspacioClientes;
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
           
            try
            {
                using(SqliteConnection connection = new SqliteConnection(cadenaConexion))
                {
                    connection.Open();
                    string query = "INSERT INTO presupuesto_detalle (idPresupuesto, idProducto,Cantidad) VALUES(@idPresupuesto,@idProducto,@Cantidad)";
                    using(SqliteCommand command = new SqliteCommand(query,connection))
                    {
                        command.Parameters.Add(new SqliteParameter("@idPresupuesto",idPresupuesto));
                        command.Parameters.Add(new SqliteParameter("@idProducto",idProducto));
                        command.Parameters.Add(new SqliteParameter("@Cantidad",cantidad));
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                }
            }catch(Exception e)
            {
                Console.WriteLine("NO SE PUEDE REALIZAR LA OPERACION VERIFICAR ID DE PRESUPUESTOS O DEL PRODUCTO");
            }

        }

        public void AltaPresupuesto(Presupuesto presupuesto)
        {
            using(SqliteConnection connection = new SqliteConnection(cadenaConexion))
            {
                connection.Open();
                string query = @"INSERT INTO presupuesto (FechaCreacion,id_cliente) VALUES(@FechaCreacion, @id_cliente)";
                using(SqliteCommand command = new SqliteCommand(query,connection))
                {
                    command.Parameters.AddWithValue("@id_cliente", presupuesto.Cliente.ClienteId);
                    command.Parameters.AddWithValue("@FechaCreacion", DateTime.Now.ToString("yyyy-MM-dd"));
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public void EliminarPresupuesto(int idPresupuesto)
        {
            try
            {
                using(SqliteConnection connection = new SqliteConnection(cadenaConexion))
                {
                    connection.Open();
                    string query1 = @"DELETE FROM presupuesto_detalle
                                        WHERE idPresupuesto IN (
                                        SELECT idPresupuesto
                                        FROM presupuesto
                                        WHERE idPresupuesto = @idPresupuesto)";
                    using(SqliteCommand command1 = new SqliteCommand(query1,connection))
                    {
                        command1.Parameters.AddWithValue("@idPresupuesto", idPresupuesto);
                        command1.ExecuteNonQuery();
                    }
                    
                    string query2 = @"DELETE FROM presupuesto
                                    WHERE idPresupuesto = @idPresupuesto";
                    using(SqliteCommand command2 = new SqliteCommand(query2,connection))
                    {
                        command2.Parameters.AddWithValue("@idPresupuesto", idPresupuesto);
                        command2.ExecuteNonQuery();
                    }

                    connection.Close();
                }
            }catch(Exception e)
            {
                Console.WriteLine("NO SE PUEDE REALIZAR LA OPERACION VERIFICAR ID DEL PRESUPUESTO"+e.Message);
            }
        }

        public Presupuesto GetDetallePresupuesto(int idPresupuesto)
        {
            ProductoRepository productoRepositoryConsulta = new ProductoRepository();
            int idPresupuestoConsulta = 0;
            Cliente clienteConsulta;
            int id_cliente = 9999999;
            string nombreClienteConsulta="";
            string emailClienteConsulta="";
            string telefonoClienteConsulta="";

            List<PresupuestoDetalle> detalles = new List<PresupuestoDetalle>();
            using(SqliteConnection connection = new SqliteConnection(cadenaConexion))
            {
                connection.Open();
                string query1 = @"SELECT idPresupuesto,idProducto, Cantidad FROM presupuesto P
                        INNER JOIN presupuesto_detalle PD USING(idPresupuesto)
                        INNER JOIN cliente C USING(id_cliente)
                        WHERE P.idPresupuesto = @idPresupuesto;";
                string query2 = @"SELECT id_cliente,nombre,email,telefono FROM cliente 
                                INNER JOIN presupuesto USING (id_cliente)
                                WHERE idPresupuesto = @idPresupuesto";

                using(SqliteCommand command1 = new SqliteCommand(query1, connection))
                {
                    command1.Parameters.AddWithValue("@idPresupuesto",idPresupuesto);
                    using(SqliteDataReader reader = command1.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            idPresupuestoConsulta = reader.GetInt32(0);
                            var productoPresupuesto = productoRepositoryConsulta.GetProducto(reader.GetInt32(1));
                            int cantidadConsulta = reader.GetInt32(2);
                            detalles.Add(new(productoPresupuesto, cantidadConsulta));
                        }
                    }
                }

                using(SqliteCommand command2 = new SqliteCommand(query2, connection))
                {
                    using(SqliteDataReader reader = command2.ExecuteReader())
                    {
                        command2.Parameters.AddWithValue("@idPresupuesto",idPresupuesto);
                        if(reader.Read())
                        {
                            id_cliente = reader.GetInt32(0);
                            nombreClienteConsulta = reader.GetString(1);
                            emailClienteConsulta = reader.GetString(2);
                            telefonoClienteConsulta = reader.GetString(3);
                            clienteConsulta = new(id_cliente, nombreClienteConsulta, emailClienteConsulta,telefonoClienteConsulta);
                        }else //si no encontro cliente con presupuesto de tal id entonces crea un objeto cliente nulo para poder continuar con la ejecucion
                        {
                            clienteConsulta = null;
                        }
                    }
                }

                connection.Close();

            }
            Presupuesto presupuesto = new Presupuesto(idPresupuestoConsulta, clienteConsulta, detalles);
            return presupuesto;
        }

        public List<Presupuesto> GetListaPresupuesto()
        {
            List<Presupuesto> listaPresupuesto = new List<Presupuesto>();
            
            using(SqliteConnection connection = new SqliteConnection(cadenaConexion))
            {
                connection.Open();
                string query = @"SELECT idPresupuesto, FechaCreacion, id_cliente, nombre, email, telefono FROM presupuesto 
                                INNER JOIN cliente USING (id_cliente)";
                using(SqliteCommand command = new SqliteCommand(query,connection))
                {
                    using(SqliteDataReader reader1 = command.ExecuteReader())
                    {
                        while(reader1.Read())
                        {
                            int idPresupuesto = reader1.GetInt32(0);
                            string fechaCreacion = reader1.GetString(1);
                            int idCliente = reader1.GetInt32(2);
                            string nombreDestinatario = reader1.GetString(3);
                            string emailCliente = reader1.GetString(4);
                            string telefonoCliente = reader1.GetString(5);                            

                            //Todo lo necesario para obtener los productos de un PresupuestoDetalle dado un idPresupuesto
                            List<PresupuestoDetalle> detalles = new List<PresupuestoDetalle>();
                            string queryDetalles = @"SELECT idProducto,Descripcion,Precio,Cantidad FROM presupuesto_detalle
                            INNER JOIN producto USING(idProducto)
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
                            Cliente clienteConsulta = new(idCliente, nombreDestinatario, emailCliente, telefonoCliente);
                            listaPresupuesto.Add(new(idPresupuesto,clienteConsulta,detalles));
                        }

                    }
                }
                connection.Close();
            }
            return listaPresupuesto;
        }
    }
}