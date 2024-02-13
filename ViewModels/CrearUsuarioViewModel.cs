using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace tp10_tl2.Models;

public class CrearUsuarioViewModel
{
    private string? _nombre;
    private string? _password;

    [Required(ErrorMessage = "Este campo es requerido")]
    [Display(Name = "Nombre de usuario")]
    public string? Nombre { get => _nombre; set => _nombre = value; }

    [Required(ErrorMessage = "Este campo es requerido")]
    [Display(Name = "Password")]
    public string? Password { get => _password; set => _password = value; }

    public CrearUsuarioViewModel() { }

    CrearUsuarioViewModel(Usuario usuario)
    {
        this.Nombre = usuario.NombreDeUsuario;
    }

}