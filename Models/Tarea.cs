
public enum Estado
{
    Ideas,
    Todo,
    Doing,
    Review,
    Done
}
public class Tarea
{
    private int _id;
    private string _nombre;
    private string _descripcion;
    private string _color;
    private Estado _estado;
    private int? idUsuarioAsignado;

    public int Id { get => _id; set => _id = value; }
    public string Nombre { get => _nombre; set => _nombre = value; }
    public string Descripcion { get => _descripcion; set => _descripcion = value; }
    public string Color { get => _color; set => _color = value; }
    public int? IdUsuarioAsignado { get => idUsuarioAsignado; set => idUsuarioAsignado = value; }
    internal Estado Estado { get => _estado; set => _estado = value; }
}