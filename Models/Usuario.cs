

using Microsoft.AspNetCore.Identity;

public enum Rol
{
    Administrador,
    Operador
}

public class Usuario
{
    private int _id;
    private string _nombreDeUsuario;

    private string _password;

    private Rol _rol;

    public int Id { get => _id; set => _id = value; }
    public string NombreDeUsuario { get => _nombreDeUsuario; set => _nombreDeUsuario = value; }
    public string Password { get => _password; set => _password = value; }
    public Rol Rol { get => _rol; set => _rol = value; }
}