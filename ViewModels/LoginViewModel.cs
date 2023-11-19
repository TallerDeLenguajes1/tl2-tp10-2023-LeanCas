using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace tp10_tl2.Models;

public class LogueoViewModel
{
    private string nombreUsuario;
    private string password;

    [Required(ErrorMessage = "Este campo es requerido")]
    [Display(Name = "Nombre de usuario")]
    public string NombreUsuario { get => nombreUsuario; set => nombreUsuario = value; }

    [Required(ErrorMessage = "Este campo es requerido")]
    [Display(Name = "Contraseña")]
    public string Password { get => password; set => password = value; }
}