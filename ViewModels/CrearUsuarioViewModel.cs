using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace tp10_tl2.Models;

public class CrearUsuarioViewModel
{
    private int _id;
    private string? _nombre;
    private string? _password;

    private string? _errorMessage;

    [Required(ErrorMessage = "Este campo es requerido")]
    [Display(Name = "Nombre de usuario")]
    public string? Nombre { get => _nombre; set => _nombre = value; }

    [Required(ErrorMessage = "Este campo es requerido")]
    [Display(Name = "Password")]
    public string? Password { get => _password; set => _password = value; }
    public string? ErrorMessage { get => _errorMessage; set => _errorMessage = value; }
    public int Id { get => _id; set => _id = value; }

    public CrearUsuarioViewModel() { }

    CrearUsuarioViewModel(Usuario usuario)
    {
        this.Nombre = usuario.NombreDeUsuario;
    }

}