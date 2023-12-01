using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace tp10_tl2.Models;

public class CrearUsuarioViewModel
{
    private string? _nombre;
    public string? Nombre { get => _nombre; set => _nombre = value; }
    public CrearUsuarioViewModel() { }

    CrearUsuarioViewModel(Usuario usuario)
    {
        this.Nombre = usuario.NombreDeUsuario;
    }

}