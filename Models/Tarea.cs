
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
    private int _idTablero;
    private string _nombre;
    private string _descripcion;
    private string _color;
    private Estado _estado;
    private int? _idUsuarioAsignado;

    public int Id { get => _id; set => _id = value; }
    public string Nombre { get => _nombre; set => _nombre = value; }
    public string Descripcion { get => _descripcion; set => _descripcion = value; }
    public string Color { get => _color; set => _color = value; }
    public int? IdUsuarioAsignado { get => _idUsuarioAsignado; set => _idUsuarioAsignado = value; }
    internal Estado Estado { get => _estado; set => _estado = value; }
    public int IdTablero { get => _idTablero; set => _idTablero = value; }

    public Tarea() { }

    public Tarea(string nombre, string descripcion, string color, Estado estado, int? idUsuarioAsignado, int IdTablero)
    {
        this._nombre = nombre;
        this._descripcion = descripcion;
        this._color = color;
        this._estado = estado;
        this._idUsuarioAsignado = idUsuarioAsignado;
        this._idTablero = IdTablero;
    }
}