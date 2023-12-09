using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace tp10_tl2.Models;

public class ModificarTareaViewModel
{
    private int _id;

    private int _idTablero;
    private string? _nombre;

    private string? _descripcion;

    private string? _color;

    private Estado _estado;

    private int? _idUsuarioAsignado;

    private List<Usuario> listaUsuario = new List<Usuario>();
    private List<Tablero> listaTablero = new List<Tablero>();
    public ModificarTareaViewModel() { }

    public ModificarTareaViewModel(List<Usuario> usuarioList, List<Tablero> tableroList)
    {
        this.listaUsuario = usuarioList;
        this.listaTablero = tableroList;
    }

    public ModificarTareaViewModel(Tarea tarea)
    {
        _id = tarea.Id;
        _nombre = tarea.Nombre;
        _descripcion = tarea.Descripcion;
        _color = tarea.Color;
        Estado = tarea.Estado;
        _idUsuarioAsignado = tarea.IdUsuarioAsignado;
    }

    public ModificarTareaViewModel(Tarea tarea, List<Usuario> usuarioList, List<Tablero> tableroList)
    {
        _id = tarea.Id;
        _nombre = tarea.Nombre;
        _descripcion = tarea.Descripcion;
        _color = tarea.Color;
        Estado = tarea.Estado;
        _idUsuarioAsignado = tarea.IdUsuarioAsignado;
        this.listaUsuario = usuarioList;
        this.listaTablero = tableroList;
    }



    public int Id { get => _id; set => _id = value; }

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

    public Tarea convertirTarea(ModificarTareaViewModel tareaViewModel)
    {
        var tarea = new Tarea(tareaViewModel.Nombre, tareaViewModel.Descripcion, tareaViewModel.Color, tareaViewModel.Estado, tareaViewModel.IdUsuarioAsignado, tareaViewModel.IdTablero);
        return tarea;
    }

}