using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace tp10_tl2.Models;

public class ListarUsuarioViewModel
{
    private int _id;
    private string? _nombre;

    public int Id { get => _id; set => _id = value; }
    public string? Nombre { get => _nombre; set => _nombre = value; }
    public ListarUsuarioViewModel() { }

    ListarUsuarioViewModel(Usuario usuario)
    {
        this.Id = usuario.Id;
        this.Nombre = usuario.NombreDeUsuario;
    }

    public List<ListarUsuarioViewModel> convertirLista(List<Usuario> listaUsuario)
    {
        var listarUsuarios = new List<ListarUsuarioViewModel>();
        foreach (Usuario U in listaUsuario)
        {
            var nuevoViewModel = new ListarUsuarioViewModel(U);
            listarUsuarios.Add(nuevoViewModel);
        }
        return listarUsuarios;
    }
}