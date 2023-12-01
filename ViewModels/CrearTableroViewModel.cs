using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace tp10_tl2.Models;

public class CrearTableroViewModel
{
    private int _id;
    private int _idUsuarioPropietario;

    private string? _nombre;

    private string? _descripcion;

    public CrearTableroViewModel() { }

    public CrearTableroViewModel(Tablero tablero)
    {
        _id = tablero.Id;
        _idUsuarioPropietario = tablero.IdUsuarioPropietario;
        _nombre = tablero.Nombre;
        _descripcion = tablero.Descripcion;
    }

    public int IdUsuarioPropietario { get => _idUsuarioPropietario; set => _idUsuarioPropietario = value; }
    public string? Nombre { get => _nombre; set => _nombre = value; }
    public string? Descripcion { get => _descripcion; set => _descripcion = value; }
    public int Id { get => _id; set => _id = value; }

}