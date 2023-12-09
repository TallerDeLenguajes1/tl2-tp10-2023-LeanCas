
namespace TableroRepository;
using System.Data.SQLite;

public class TableroRepositorio : ITableroRepositorio
{
    private readonly string cadenaConexion;

    public TableroRepositorio(string CadenaDeConexion)
    {
        cadenaConexion = CadenaDeConexion;
    }
    public void Create(Tablero tablero)
    {
        try
        {
            var queryString = @"INSERT INTO Tablero (id_usuario_propietario,nombre,descripcion) VALUES (@id_u_propietario, @nombre, @descripcion);";

            using (var connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@id_u_propietario", tablero.IdUsuarioPropietario));
                command.Parameters.Add(new SQLiteParameter("@nombre", tablero.Nombre));
                command.Parameters.Add(new SQLiteParameter("@descripcion", tablero.Descripcion));

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
            var queryString = @"DELETE FROM Tablero WHERE Tablero.id = (@id_tablero);";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(queryString, connection);

                command.Parameters.Add(new SQLiteParameter("@id_tablero", id));
                command.ExecuteNonQuery();

                connection.Close();
            }
        }
        catch
        {
            throw;
        }
    }

    public List<Tablero> GetAll()
    {
        try
        {
            List<Tablero> tableros = new List<Tablero>();
            var queryString = @"SELECT * FROM Tablero;";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();

                var command = new SQLiteCommand(queryString, connection);


                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tablero = new Tablero();
                        tablero.Id = Convert.ToInt32(reader["id"]);
                        tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                        tablero.Nombre = reader["nombre"].ToString();
                        tablero.Descripcion = reader["descripcion"].ToString();
                        tableros.Add(tablero);
                    }
                }

                connection.Close();
            }
            return tableros;
        }
        catch
        {
            throw;
        }
    }

    public Tablero GetTablero(int id)
    {
        try
        {
            var queryString = @"SELECT * FROM Tablero WHERE Tablero.id = @id_tablero ;";
            var tablero = new Tablero();
            using (var connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();

                var command = new SQLiteCommand(queryString, connection);

                command.Parameters.Add(new SQLiteParameter("@id_tablero", id));

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tablero.Id = Convert.ToInt32(reader["id"]);
                        tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                        tablero.Nombre = reader["nombre"].ToString();
                        tablero.Descripcion = reader["descripcion"].ToString();
                    }
                }

                connection.Close();
            }
            return tablero;
        }
        catch
        {
            throw;
        }
    }

    public List<Tablero> GetTableroUsuario(int idUsuario)
    {
        try
        {
            List<Tablero> tableros = new List<Tablero>();
            var queryString = @"SELECT * FROM Tablero WHERE Tablero.id_usuario_propietario = @idUsuario ;";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();

                var command = new SQLiteCommand(queryString, connection);

                command.Parameters.Add(new SQLiteParameter("@idUsuario", idUsuario));


                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tablero = new Tablero();
                        tablero.Id = Convert.ToInt32(reader["id"]);
                        tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                        tablero.Nombre = reader["nombre"].ToString();
                        tablero.Descripcion = reader["descripcion"].ToString();
                        tableros.Add(tablero);
                    }
                }

                connection.Close();
            }
            return tableros;
        }
        catch
        {
            throw;
        }
    }

    public void Set(int id, Tablero tablero)
    {
        try
        {
            var queryString = @"UPDATE Tablero SET (id,id_usuario_propietario,nombre,descripcion) = (@id, @id_u_propietario, @nombre, @descripcion) WHERE id = @id_buscar;";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();

                var command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@id_buscar", id));
                command.Parameters.Add(new SQLiteParameter("@id", tablero.Id));
                command.Parameters.Add(new SQLiteParameter("@id_u_propietario", tablero.IdUsuarioPropietario));
                command.Parameters.Add(new SQLiteParameter("@nombre", tablero.Nombre));
                command.Parameters.Add(new SQLiteParameter("@descripcion", tablero.Descripcion));

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