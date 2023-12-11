using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tp10_tl2.Models;
using UsuarioRepository;

namespace tp10_tl2.Controllers;

public class LogueoController : Controller
{

    private readonly ILogger<LogueoController> _logger;

    private readonly IUsuarioRepositorio repository;

    public LogueoController(ILogger<LogueoController> logger, IUsuarioRepositorio UsuarioRepository)
    {
        _logger = logger;
        repository = UsuarioRepository;
    }


    public IActionResult Index()
    {
        try
        {
            return View();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    [HttpPost]
    public IActionResult LogueoVerificar(LogueoViewModel login)
    {
        try
        {
            if (!ModelState.IsValid) return RedirectToAction("Index");

            var usuarioVerificar = repository.VerificarUsuario(login.NombreUsuario, login.Password);
            if (string.IsNullOrEmpty(usuarioVerificar.NombreDeUsuario))
            {
                _logger.LogWarning("Intento de acceso invalido de Usuario : " + login.NombreUsuario + " Contraseña : " + login.Password);
                return RedirectToAction("Index");
            }
            else
            {
                Loguear(usuarioVerificar);
                _logger.LogInformation("Ingreso correctamente " + login.NombreUsuario);
                if (HttpContext.Session.GetString("rol") == Rol.Administrador.ToString())
                {
                    return RedirectToRoute(new { controller = "Usuario", action = "Index" });
                }
                else
                {
                    return RedirectToRoute(new { controller = "Tablero", action = "Index" });
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    private void Loguear(Usuario usuario)
    {
        HttpContext.Session.SetString("id", usuario.Id.ToString());
        HttpContext.Session.SetString("usuario", usuario.NombreDeUsuario);
        HttpContext.Session.SetString("rol", usuario.Rol.ToString());
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}