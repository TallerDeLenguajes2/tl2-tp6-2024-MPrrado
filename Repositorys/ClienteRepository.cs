using EspacioClientes;
using Microsoft.Data.Sqlite;
using SQLitePCL;

namespace EspacioRepositorios
{
    internal interface IClienteRepository
    {
        public void AltaCliente(Cliente cliente);
        public List<Cliente> GetListaCliente();
        public void ModificarCliente(int idCliente, Cliente clienteModificado);
        public void EliminarCliente(int idCliente);
    }
    public class ClienteRepository : IClienteRepository
    {
        private static string cadenaConexion = "Data Source=db/Tienda.db;Cache=Shared";
        public void AltaCliente(Cliente cliente)
        {
            if(cliente != null)
            {
                using(SqliteConnection connection = new SqliteConnection(cadenaConexion))
                {
                    connection.Open();
                    string query = @"INSERT INTO cliente (nombre, email, telefono) VALUES(@nombre, @email, @telefono)";
                    using(SqliteCommand command = new SqliteCommand(query,connection))
                    {
                        command.Parameters.AddWithValue("@nombre", cliente.Nombre);
                        command.Parameters.AddWithValue("@email", cliente.Email);
                        command.Parameters.AddWithValue("@telefono", cliente.Telefono);
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                }
            }else
            {
                throw new Exception("ERROR CON LOS DATOS DEL CLIENTE");
            }
        }
        public List<Cliente> GetListaCliente()
        {
            List<Cliente> listaCliente = new List<Cliente>();
            string query = @"SELECT id_cliente, nombre, email, telefono FROM cliente";
            using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SqliteCommand(query, connection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Cliente cliente = new Cliente(Convert.ToInt32(reader["id_cliente"]),reader["nombre"].ToString(), Convert.ToString(reader["email"]), Convert.ToString(reader["telefono"]));
                        listaCliente.Add(cliente);
                    }
                }
                connection.Close();
            }
            if(listaCliente != null)
            {
                return listaCliente;
            }else
            {
                throw new Exception("ERROR NO HAY CLIENTES");
            }
        }

        public void EliminarCliente(int idCliente)
        {
            try
            {
                string query1 = @"DELETE FROM presupuesto
                                WHERE id_cliente = @id_cliente";
                string query2 = @"DELETE FROM cliente
                                WHERE  id_cliente = @id_cliente";

                using(SqliteConnection connection = new SqliteConnection(cadenaConexion))
                {
                    connection.Open();
                    using(SqliteCommand command1 = new SqliteCommand(query1,connection))
                    {
                        command1.Parameters.Add(new SqliteParameter("@idProducto", idCliente));
                        command1.ExecuteNonQuery();
                    }
                    
                    using(SqliteCommand command2 = new SqliteCommand(query2,connection))
                    {
                        command2.Parameters.Add(new SqliteParameter("@idProducto", idCliente));
                        command2.ExecuteNonQuery();
                    }
                    connection.Close();
                }
            }catch(Exception e)
            {
                throw new ("ERROR NO SE PUDO ELIMINAR");
            }
        }


        public void ModificarCliente(int idCliente, Cliente clienteModificado)
        {
            try
            {
                string query = @"UPDATE cliente SET nombre = @nombre, email = @email, telefono = @telefono
                                WHERE id_cliente = @id_cliente";
                using(SqliteConnection connection = new SqliteConnection(cadenaConexion))
                {
                    connection.Open();
                    using(SqliteCommand command = new SqliteCommand(query, connection))
                    {
                        command.Parameters.Add(new SqliteParameter("@id_cliente", idCliente));
                        command.Parameters.Add(new SqliteParameter("@nombre", clienteModificado.Nombre));
                        command.Parameters.Add(new SqliteParameter("@email", clienteModificado.Email));
                        command.Parameters.Add(new SqliteParameter("@telefono", clienteModificado.Telefono));
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                }
            }catch(Exception e)
            {
                throw new Exception("ERROR NO SE PUDO MODIFICAR LOS DATOS DEL CLIENTE");
            }
        }
    }
}