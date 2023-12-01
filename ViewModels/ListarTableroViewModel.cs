using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace tp10_tl2.Models;

public class ListarTableroViewModel
{
    private int _id;
    private int _idUsuarioPropietario;

    private string? _nombre;

    private string? _descripcion;

    public ListarTableroViewModel() { }

    public ListarTableroViewModel(Tablero tablero)
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

    public List<ListarTableroViewModel> convertirLista(List<Tablero> listaTablero)
    {
        var listaTableros = new List<ListarTableroViewModel>();
        foreach (Tablero T in listaTablero)
        {
            var nuevoViewModel = new ListarTableroViewModel(T);
            listaTableros.Add(nuevoViewModel);
        }
        return listaTableros;
    }
}