namespace TareaRepository;
using System.Data.SqlClient;
using System.Data.SQLite;

public class TareaRepositorio : ITareaRepositorio
{
    private readonly string cadenaConexion;

    public TareaRepositorio(string CadenaDeConexion)
    {
        cadenaConexion = CadenaDeConexion;
    }
    public void Asignar(int idUsuario, int idTarea)
    {
        try
        {
            var queryString = @"UPDATE Tarea SET id_usuario_asignado = (@id_usuario) WHERE id = (@id_tarea);";
            using (var connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();

                var command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@id_usuario", idUsuario));
                command.Parameters.Add(new SQLiteParameter("@id_tarea", idTarea));
                command.ExecuteNonQuery();

                connection.Close();
            }
        }
        catch
        {
            throw;
        }
    }

    public Tarea Create(int idTablero, Tarea tarea)
    {
        try
        {
            var queryString = @"INSERT INTO Tarea (id_tablero,nombre,estado,descripcion,color,id_usuario_asignado) VALUES (@id_tablero, @nombre, @estado, @descripcion, @color, @id_u_asignado);";
            using (var connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@id_tablero", idTablero));
                command.Parameters.Add(new SQLiteParameter("@nombre", tarea.Nombre));
                command.Parameters.Add(new SQLiteParameter("@estado", tarea.Estado));
                command.Parameters.Add(new SQLiteParameter("@descripcion", tarea.Descripcion));
                command.Parameters.Add(new SQLiteParameter("@color", tarea.Color));
                command.Parameters.Add(new SQLiteParameter("@id_u_asignado", tarea.IdUsuarioAsignado));

                command.ExecuteNonQuery();
                connection.Close();
            }
            return null;
        }
        catch
        {
            throw;
        }
    }

    public void Delete(int idTarea)
    {
        try
        {
            var queryString = @"DELETE FROM Tarea WHERE id = (@id_tarea);";
            using (var connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();

                var command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@id_tarea", idTarea));
                command.ExecuteNonQuery();

                connection.Close();
            }
        }
        catch
        {
            throw;
        }
    }

    public List<Tarea> GetAll()
    {
        try
        {
            var queryString = @"SELECT * FROM Tarea ORDER BY id_usuario_asignado;";
            List<Tarea> tareas = new List<Tarea>();
            using (var connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();

                var command = new SQLiteCommand(queryString, connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tarea = new Tarea();
                        tarea.Id = Convert.ToInt32(reader["id"]);
                        tarea.Nombre = reader["nombre"].ToString();
                        string estadoLeido = reader["estado"].ToString(); // Supongamos que lees el valor como una cadena
                        Estado estadoEnum;
                        if (Enum.TryParse(estadoLeido, out estadoEnum))
                        {
                            tarea.Estado = estadoEnum;
                        }
                        tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                        tarea.Descripcion = reader["descripcion"].ToString();
                        tarea.Color = reader["color"].ToString();
                        tarea.IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"]);
                        tareas.Add(tarea);
                    }
                }

                connection.Close();
            }
            return tareas;
        }
        catch
        {
            throw;
        }
    }

    public Tarea GetTarea(int id)
    {
        try
        {
            var queryString = @"SELECT * FROM Tarea WHERE Tarea.id = @id_tarea ;";
            var tarea = new Tarea();
            using (var connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();

                var command = new SQLiteCommand(queryString, connection);

                command.Parameters.Add(new SQLiteParameter("@id_tarea", id));

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tarea.Id = Convert.ToInt32(reader["id"]);
                        tarea.Nombre = reader["nombre"].ToString();
                        string estadoLeido = reader["estado"].ToString(); // Supongamos que lees el valor como una cadena
                        Estado estadoEnum;
                        if (Enum.TryParse(estadoLeido, out estadoEnum))
                        {
                            tarea.Estado = estadoEnum;
                        }
                        tarea.Descripcion = reader["descripcion"].ToString();
                        tarea.Color = reader["color"].ToString();
                        tarea.IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"]);
                    }
                }

                connection.Close();
            }
            return tarea;
        }
        catch
        {
            throw;
        }
    }

    public List<Tarea> GetTareaTablero(int idTablero)
    {
        try
        {
            var queryString = @"SELECT * FROM Tarea WHERE Tarea.id_tablero = @id_tablero ;";
            List<Tarea> tareas = new List<Tarea>();
            using (var connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();

                var command = new SQLiteCommand(queryString, connection);

                command.Parameters.Add(new SQLiteParameter("id_tablero", idTablero));

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tarea = new Tarea();
                        tarea.Id = Convert.ToInt32(reader["id"]);
                        tarea.Nombre = reader["nombre"].ToString();
                        string estadoLeido = reader["estado"].ToString(); // Supongamos que lees el valor como una cadena
                        Estado estadoEnum;
                        if (Enum.TryParse(estadoLeido, out estadoEnum))
                        {
                            tarea.Estado = estadoEnum;
                        }
                        tarea.Descripcion = reader["descripcion"].ToString();
                        tarea.Color = reader["color"].ToString();
                        tarea.IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"]);
                        tareas.Add(tarea);
                    }
                }

                connection.Close();
            }

            return tareas;
        }
        catch
        {
            throw;
        }
    }

    public void Set(int id, Tarea tarea)
    {
        try
        {
            var queryString = @"UPDATE Tarea SET (nombre,estado,descripcion,color,id_usuario_asignado, id_tablero) = (@nombre, @estado, @descripcion, @color, @id_u_asignado, @id_tablero) WHERE id = @id_buscar;";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();

                var command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@id_buscar", id));
                command.Parameters.Add(new SQLiteParameter("@nombre", tarea.Nombre));
                command.Parameters.Add(new SQLiteParameter("@estado", tarea.Estado));
                command.Parameters.Add(new SQLiteParameter("@descripcion", tarea.Descripcion));
                command.Parameters.Add(new SQLiteParameter("@color", tarea.Color));
                command.Parameters.Add(new SQLiteParameter("@id_u_asignado", tarea.IdUsuarioAsignado));
                command.Parameters.Add(new SQLiteParameter("@id_tablero", tarea.IdTablero));

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