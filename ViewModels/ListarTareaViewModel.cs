using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace tp10_tl2.Models;

public class ListarTareaViewModel
{
    private int _id;

    private string? _Tablero;
    private string? _nombre;

    private string? _descripcion;

    private string? _color;

    private Estado _estado;

    private string? _usuarioAsignado;
    public ListarTareaViewModel() { }

    public ListarTareaViewModel(Tarea tarea, string? usuario, string? tablero)
    {
        _id = tarea.Id;
        _nombre = tarea.Nombre;
        _descripcion = tarea.Descripcion;
        _color = tarea.Color;
        Estado = tarea.Estado;
        _usuarioAsignado = usuario;
        _Tablero = tablero;
    }

    public int Id { get => _id; set => _id = value; }
    public string? Nombre { get => _nombre; set => _nombre = value; }
    public string? Descripcion { get => _descripcion; set => _descripcion = value; }
    public string? Color { get => _color; set => _color = value; }
    public Estado Estado { get => _estado; set => _estado = value; }
    public string? Tablero { get => _Tablero; set => _Tablero = value; }
    public string? UsuarioAsignado { get => _usuarioAsignado; set => _usuarioAsignado = value; }

    public ListarTareaViewModel convertirVM(Tarea tarea, string? usuario, string? tablero)
    {
        var tareaVM = new ListarTareaViewModel(tarea, usuario, tablero);
        return tareaVM;
    }
}