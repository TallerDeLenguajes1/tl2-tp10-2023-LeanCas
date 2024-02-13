using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace UsuarioRepository;
public class UsuarioRepositorio : IUsuarioRepositorio
{
    private readonly string cadenaConexion;

    public UsuarioRepositorio(string CadenaDeConexion)
    {
        cadenaConexion = CadenaDeConexion;
    }
    public void Create(Usuario usuario)
    {
        try
        {
            var queryString = @"INSERT INTO Usuario (nombre_de_usuario, rol, password) VALUES (@nombre_de_usuario, @rol, @password);";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(queryString, connection);

                string RolString = usuario.Rol.ToString();

                command.Parameters.Add(new SQLiteParameter("@nombre_de_usuario", usuario.NombreDeUsuario));
                command.Parameters.Add(new SQLiteParameter("@rol", RolString));
                command.Parameters.Add(new SQLiteParameter("@password", usuario.Password));
                command.ExecuteNonQuery();

                connection.Close();
            }
        }
        catch
        {
            throw;
        }
    }

    public void Delete(int id)
    {
        try
        {
            var queryString = @"DELETE FROM Usuario WHERE Usuario.id = (@id_usuario);";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(queryString, connection);

                command.Parameters.Add(new SQLiteParameter("@id_usuario", id));
                command.ExecuteNonQuery();

                connection.Close();
            }
        }
        catch
        {
            throw;
        }
    }

    public List<Usuario> GetAll()
    {
        try
        {
            List<Usuario> users = new List<Usuario>();
            var queryString = @"SELECT * FROM Usuario;";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();

                var command = new SQLiteCommand(queryString, connection);


                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var usuario = new Usuario();
                        usuario.Id = Convert.ToInt32(reader["id"]);
                        usuario.NombreDeUsuario = reader["nombre_de_usuario"].ToString();
                        if (!reader.IsDBNull(reader.GetOrdinal("rol")))
                        {
                            // Obtener el valor de la columna "rol"
                            string rolString = reader["rol"].ToString();

                            Rol rolUsuario;

                            // Verificar si la cadena es nula o vacía
                            if (!string.IsNullOrEmpty(rolString))
                            {
                                if (Enum.TryParse(rolString, out rolUsuario))
                                {
                                    usuario.Rol = rolUsuario;
                                }
                                else
                                {
                                    usuario.Rol = null;
                                }
                            }
                            else
                            {
                                usuario.Rol = null;
                            }
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("password")))
                        {
                            usuario.Password = reader["password"].ToString();
                        }
                        users.Add(usuario);
                    }

                    connection.Close();
                }
                return users;
            }

        }
        catch
        {
            throw;
        }
    }

    public Usuario GetUsuario(int? id)
    {
        try
        {
            List<Usuario> users = new List<Usuario>();
            var queryString = @"SELECT * FROM Usuario;";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();

                var command = new SQLiteCommand(queryString, connection);


                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var usuario = new Usuario();
                        usuario.Id = Convert.ToInt32(reader["id"]);
                        usuario.NombreDeUsuario = reader["nombre_de_usuario"].ToString();
                        string rolString = reader["rol"].ToString();
                        Rol rolUsuario;
                        if (Enum.TryParse(rolString, out rolUsuario))
                        {
                            usuario.Rol = rolUsuario;
                        }
                        usuario.Password = reader["password"].ToString();
                        users.Add(usuario);
                    }
                }

                connection.Close();
            }
            var usuarioBuscado = users.FirstOrDefault(U => U.Id == id);
            return usuarioBuscado;
        }
        catch
        {
            throw;
        }
    }


    public Usuario VerificarUsuario(string usuario, string password)
    {
        try
        {
            var usuarioVerificado = new Usuario();
            var queryString = @"SELECT * FROM Usuario WHERE (nombre_de_usuario = @nombreUsuario AND password = @password);";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();

                var command = new SQLiteCommand(queryString, connection);

                command.Parameters.Add(new SQLiteParameter("@nombreUsuario", usuario));
                command.Parameters.Add(new SQLiteParameter("@password", password));


                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        usuarioVerificado.Id = Convert.ToInt32(reader["id"]);
                        usuarioVerificado.NombreDeUsuario = reader["nombre_de_usuario"].ToString();
                        string rolString = reader["rol"].ToString();
                        Rol rolUsuario;
                        if (Enum.TryParse(rolString, out rolUsuario))
                        {
                            usuarioVerificado.Rol = rolUsuario;
                        }
                        usuarioVerificado.Password = reader["password"].ToString();
                    }
                }

                connection.Close();
            }
            return usuarioVerificado;
        }
        catch
        {
            throw;
        }
    }

    public void Set(int id, Usuario usuario)
    {
        try
        {
            var queryString = @"UPDATE Usuario SET nombre_de_usuario = @nombre_modificado WHERE id = @id_usuario;";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();

                var command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@nombre_modificado", usuario.NombreDeUsuario));
                command.Parameters.Add(new SQLiteParameter("@id_usuario", id));

                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        catch
        {
            throw;
        }
    }
}