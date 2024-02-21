using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace tp10_tl2.Models;

public class ModificarUsuarioViewModel
{
    private int _id;
    private string? _nombre;

    private string? _password;

    [Required(ErrorMessage = "Este campo es requerido")]
    [Display(Name = "Nombre de usuario")]
    public string? Nombre { get => _nombre; set => _nombre = value; }
    public int Id { get => _id; set => _id = value; }

    [Required(ErrorMessage = "Este campo es requerido")]
    [Display(Name = "Password")]
    public string? Password { get => _password; set => _password = value; }

    public ModificarUsuarioViewModel() { }

    public ModificarUsuarioViewModel(Usuario usuario)
    {
        this.Nombre = usuario.NombreDeUsuario;
        this.Password = usuario.Password;
        this.Id = usuario.Id;
    }

    public Usuario convertirUsuario(ModificarUsuarioViewModel viewModel)
    {
        var usuario = new Usuario(viewModel.Nombre, viewModel.Password);
        return usuario;
    }

}