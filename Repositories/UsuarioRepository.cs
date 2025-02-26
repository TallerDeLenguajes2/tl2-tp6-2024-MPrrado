
using EspacioModelos;
using Microsoft.Data.Sqlite;

namespace EspacioRepositorios
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private static string cadenaConexion = "Data Source=db/Tienda.db;Cache=Shared";

        //metodo para encriptar las constraseñas y comprobar contraseñas 
        public string EncriptarPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        public bool ComprobarPassword(string passwordIngresad, string passwordDb)
        {
            return BCrypt.Net.BCrypt.Verify(passwordIngresad, passwordDb);
        }

        public void AltaUsuario(Usuario usuario)
        {
            string query = @"INSERT INTO usuario(nombre, usuario, password, id_rol) VALUES(@nombre, @password, @id_rol)";
            using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
            {
                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@nombre", usuario.Nombre);
                    command.Parameters.AddWithValue("@usuario", usuario.UserName);
                    var passwordHashed = EncriptarPassword(usuario.Password);
                    command.Parameters.AddWithValue("@password", passwordHashed);
                    command.Parameters.AddWithValue("@id_rol", usuario.Rol);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void ModificarUsuario(int idUsuario, Usuario usuarioModificado)
        {
            string query = @"UPDATE usuario SET nombre = @nombre, usuario = @usuario, password = @password 
                                WHERE id_usuario = @id_usuario";
                using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
                {
                    using (SqliteCommand command = new SqliteCommand(query, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@id_usuario", idUsuario);
                        command.Parameters.AddWithValue("@nombre", usuarioModificado.Nombre);
                        command.Parameters.AddWithValue("@nombre", usuarioModificado.UserName);
                        command.Parameters.AddWithValue("@password", EncriptarPassword(usuarioModificado.Password));
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
        }

        public Usuario GetUsuario(int idUsuario)
        {
            Usuario usuario = new();
            string query = @"SELECT id_usuario, nombre, usuario, password, id_rol FROM usuario 
                            WHERE id_usuario = @id_usuario";
            using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
            {
                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@id_usuario", idUsuario);
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            usuario.IdUsuario = reader.GetInt32(0);
                            usuario.Nombre = reader.GetString(1);
                            usuario.UserName = reader.GetString(2);
                            usuario.Password = reader.GetString(3);
                            usuario.Rol = (RolUsuario)reader.GetInt32(4);
                        }
                        connection.Close();
                    }
                }
            }
            return usuario;
        }

        public Usuario GetUsuarioPorNombreUsuario(string nombreUsuario)
        {
            Usuario usuario = new();
            string query = @"SELECT id_usuario, nombre, usuario, password, id_rol FROM usuario 
                            WHERE usuario = @usuario";
            using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
            {
                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@usuario", nombreUsuario);
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            usuario.IdUsuario = reader.GetInt32(0);
                            usuario.Nombre = reader.GetString(1);
                            usuario.UserName = reader.GetString(2);
                            usuario.Password = reader.GetString(3);
                            usuario.Rol = (RolUsuario)reader.GetInt32(4);
                        }
                        connection.Close();
                    }
                }
            }
            return usuario;
        }

        public List<Usuario> GetListadoUsuarios()
        {
            List<Usuario> listadoUsuarios = new List<Usuario>();
                string query = @"SELECT id_usuario, nombre, usuario, password, id_rol FROM usuario";
                using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
                {
                    using (SqliteCommand command = new SqliteCommand(query, connection))
                    {
                        connection.Open();
                        using (SqliteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int idUsuarioConsulta = reader.GetInt32(0);
                                string nombreConsulta = reader.GetString(1);
                                string usuarioConsulta = reader.GetString(2);
                                string password = reader.GetString(3);
                                int idRol = reader.GetInt32(4);
                                listadoUsuarios.Add(new(idUsuarioConsulta,nombreConsulta, usuarioConsulta, password, (RolUsuario)idRol));
                            }
                            connection.Close();
                        }
                    }
                }
                return listadoUsuarios;
        }

        public void EliminarUsuario(int idUsuario)
        {
            string query = @"DELETE FROM usuario WHERE id_usuario = @id_usuario";
            using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
            {
                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@id_usuario", idUsuario);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
    }
}