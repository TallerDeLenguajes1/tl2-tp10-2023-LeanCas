using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace tp10_tl2.Models;

public class CrearTareaViewModel
{
    private string? _nombre;

    private string? _descripcion;

    private string? _color;

    private Estado _estado;

    private int? _idUsuarioAsignado;
    private int _idTablero;

    private List<Usuario> listaUsuario = new List<Usuario>();
    private List<Tablero> listaTablero = new List<Tablero>();

    public CrearTareaViewModel() { }
    public CrearTareaViewModel(List<Usuario> usuarioList, List<Tablero> tableroList)
    {
        this.listaUsuario = usuarioList;
        this.listaTablero = tableroList;
    }

    public CrearTareaViewModel(Tarea tarea)
    {
        _nombre = tarea.Nombre;
        _descripcion = tarea.Descripcion;
        _color = tarea.Color;
        Estado = tarea.Estado;
        _idUsuarioAsignado = tarea.IdUsuarioAsignado;
        _idTablero = tarea.IdTablero;
    }

    [Required(ErrorMessage = "Este campo es requerido")]
    public string? Nombre { get => _nombre; set => _nombre = value; }

    [Required(ErrorMessage = "Este campo es requerido")]
    public string? Descripcion { get => _descripcion; set => _descripcion = value; }

    [Required(ErrorMessage = "Este campo es requerido")]
    public string? Color { get => _color; set => _color = value; }

    [Required(ErrorMessage = "Este campo es requerido")]
    public Estado Estado { get => _estado; set => _estado = value; }

    public int? IdUsuarioAsignado { get => _idUsuarioAsignado; set => _idUsuarioAsignado = value; }

    [Required(ErrorMessage = "Este campo es requerido")]
    public int IdTablero { get => _idTablero; set => _idTablero = value; }
    public List<Usuario> ListaUsuario { get => listaUsuario; set => listaUsuario = value; }
    public List<Tablero> ListaTablero { get => listaTablero; set => listaTablero = value; }
}