using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace tp10_tl2.Models;

public class ListarTareaViewModel
{
    private int _id;
    private string? _nombre;

    private string? _descripcion;

    private string? _color;

    private Estado _estado;

    private int? _idUsuarioAsignado;
    public ListarTareaViewModel() { }

    public ListarTareaViewModel(Tarea tarea)
    {
        _id = tarea.Id;
        _nombre = tarea.Nombre;
        _descripcion = tarea.Descripcion;
        _color = tarea.Color;
        Estado = tarea.Estado;
        _idUsuarioAsignado = tarea.IdUsuarioAsignado;
    }


    public int Id { get => _id; set => _id = value; }
    public string? Nombre { get => _nombre; set => _nombre = value; }
    public string? Descripcion { get => _descripcion; set => _descripcion = value; }
    public string? Color { get => _color; set => _color = value; }
    public Estado Estado { get => _estado; set => _estado = value; }
    public int? IdUsuarioAsignado { get => _idUsuarioAsignado; set => _idUsuarioAsignado = value; }


    public List<ListarTareaViewModel> convertirLista(List<Tarea> listaTarea)
    {
        var listaTareas = new List<ListarTareaViewModel>();
        foreach (Tarea T in listaTarea)
        {
            var nuevoViewModel = new ListarTareaViewModel(T);
            listaTareas.Add(nuevoViewModel);
        }
        return listaTareas;
    }
}