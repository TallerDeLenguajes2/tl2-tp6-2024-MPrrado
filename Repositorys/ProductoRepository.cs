using EspacioProductos;
using Microsoft.Data.Sqlite;

namespace EspacioRepositorios
{
    internal interface IProductoRepository
    {
        public void AltaProducto(Producto producto);
        public void ModificarProducto(int idProducto, Producto producto);
        public List<Producto> GetListaProductos();
        public Producto GetProducto(int idProducto);
        public void EliminarProducto(int idProducto);
    }
    public class ProductoRepository : IProductoRepository
    {
        private static string cadenaDeConexion = "Data Source=db/Tienda.db;Cache=Shared";
        public void AltaProducto(Producto producto)
        {
            if (producto != null)
            {
                string query = @"INSERT INTO Productos (Descripcion, Precio) VALUES (@Descripcion, @Precio)";
                using (SqliteConnection connection = new SqliteConnection(cadenaDeConexion))
                {
                    connection.Open();
                    using (var command = new SqliteCommand(query, connection))
                    {
                        command.Parameters.Add(new SqliteParameter("@Descripcion", producto.Descripcion));
                        command.Parameters.Add(new SqliteParameter("@Precio", producto.Precio));
                        int a = command.ExecuteNonQuery();
                    }
                    connection.Close();
                }
            }
        }

        public void EliminarProducto(int idProducto)
        {
            string query = @"DELETE FROM Productos WHERE idProducto = @idProducto";
            using(SqliteConnection connection = new SqliteConnection(cadenaDeConexion))
            {
                using(SqliteCommand command = new SqliteCommand(query,connection))
                {
                    command.Parameters.Add(new SqliteParameter("@idProducto", idProducto));
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public Producto GetProducto(int idProducto)
        {
           string query = @"SELECT * FROM Productos WHERE idProducto = @idProducto";
           Producto producto = new Producto();
           using(SqliteConnection connection = new SqliteConnection(cadenaDeConexion))
           {
            var command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@idProducto", idProducto));
            connection.Open();
            using(SqliteDataReader reader = command.ExecuteReader()) //esto debe ir siempre dentro del using ? por qué?
            {
                if(reader.Read())
                {
                    producto.Descripcion = reader["Descripcion"].ToString();
                    producto.Precio = Convert.ToInt32(reader["Precio"]);
                }else
                {
                    producto = null;
                }
               

            }
            connection.Close();
           }
           return producto;
        }

        public List<Producto> GetListaProductos()
        {
            List<Producto> listaProductos = new List<Producto>();
            string query = @"SELECT idProducto, Descripcion, Precio FROM Productos";
            using (SqliteConnection connection = new SqliteConnection(cadenaDeConexion))
            {
                connection.Open();
                var command = new SqliteCommand(query, connection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Producto productoDB = new Producto(Convert.ToInt32(reader["idProducto"]),reader["Descripcion"].ToString(), Convert.ToInt32(reader["Precio"]));
                        listaProductos.Add(productoDB);
                        // productoDB.Descripcion = reader["Descripcion"].ToString();
                    }
                }
                connection.Close();
            }
            return listaProductos;
        }

        public void ModificarProducto(int idProducto, Producto productoModificado)
        {
            string query = @"UPDATE Productos SET Descripcion = @Descripcion, Precio = @Precio WHERE idProducto = @idProducto";
            using (SqliteConnection connection = new SqliteConnection(cadenaDeConexion))
                {
                    connection.Open();
                    using (var command = new SqliteCommand(query, connection))
                    {
                        if(productoModificado.Descripcion == null )
                        {
                            command.Parameters.Add(new SqliteParameter("@Descripcion", "SIN DESCRIPCION"));
                        }else
                        {
                            command.Parameters.Add(new SqliteParameter("@Descripcion", productoModificado.Descripcion));
                        }

                        if(productoModificado.Precio == 0)
                        {
                            command.Parameters.Add(new SqliteParameter("@Precio", -1));
                        }else
                        {
                            command.Parameters.Add(new SqliteParameter("@Precio", productoModificado.Precio));
                        }
                        command.Parameters.Add(new SqliteParameter("@idProducto", idProducto));

                        int a = command.ExecuteNonQuery();
                    }
                    connection.Close();
                }
        }
    }
}