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
    public CrearTareaViewModel() { }

    public CrearTareaViewModel(Tarea tarea)
    {
        _nombre = tarea.Nombre;
        _descripcion = tarea.Descripcion;
        _color = tarea.Color;
        Estado = tarea.Estado;
        _idUsuarioAsignado = tarea.IdUsuarioAsignado;
    }

    [Required(ErrorMessage = "Este campo es requerido")]
    public string? Nombre { get => _nombre; set => _nombre = value; }

    [Required(ErrorMessage = "Este campo es requerido")]
    public string? Descripcion { get => _descripcion; set => _descripcion = value; }

    [Required(ErrorMessage = "Este campo es requerido")]
    public string? Color { get => _color; set => _color = value; }

    [Required(ErrorMessage = "Este campo es requerido")]
    public Estado Estado { get => _estado; set => _estado = value; }

    [Required(ErrorMessage = "Este campo es requerido")]
    public int? IdUsuarioAsignado { get => _idUsuarioAsignado; set => _idUsuarioAsignado = value; }
}