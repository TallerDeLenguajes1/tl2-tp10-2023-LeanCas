using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace tp10_tl2.Models;

public class ModificarUsuarioViewModel
{
    private int _id;
    private string? _nombre;
    public string? Nombre { get => _nombre; set => _nombre = value; }
    public int Id { get => _id; set => _id = value; }

    public ModificarUsuarioViewModel() { }

    public ModificarUsuarioViewModel(Usuario usuario)
    {
        this.Nombre = usuario.NombreDeUsuario;

        this.Id = usuario.Id;
    }

    public Usuario convertirUsuario(ModificarUsuarioViewModel viewModel)
    {
        var usuario = new Usuario(viewModel.Nombre);
        return usuario;
    }

}