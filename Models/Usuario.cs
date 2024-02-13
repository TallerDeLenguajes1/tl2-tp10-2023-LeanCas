

using Microsoft.AspNetCore.Identity;

public enum Rol
{
    Administrador,
    Operador
}

public class Usuario
{
    private int _id;
    private string? _nombreDeUsuario;

    private string? _password;

    private Rol? _rol;

    public int Id { get => _id; set => _id = value; }
    public string? NombreDeUsuario { get => _nombreDeUsuario; set => _nombreDeUsuario = value; }
    public string? Password { get => _password; set => _password = value; }
    public Rol? Rol { get => _rol; set => _rol = value; }

    public Usuario() { }

    public Usuario(string? Nombre)
    {
        this._nombreDeUsuario = Nombre;
    }
    public Usuario(string? Nombre, string? Password)
    {
        this._nombreDeUsuario = Nombre;
        this._password = Password;
        this._rol = global::Rol.Operador;
    }
}