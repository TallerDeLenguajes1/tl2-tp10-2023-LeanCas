using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tp10_tl2.Models;

namespace tp10_tl2.Controllers;

public class LogueoController : Controller
{

    private readonly ILogger<LogueoController> _logger;

    private readonly IUsuarioRepositorio repository;

    public LogueoController(ILogger<LogueoController> logger)
    {
        _logger = logger;
        repository = new UsuarioRepositorio();
    }


    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult LogueoVerificar(LogueoViewModel login)
    {
        var usuarioVerificar = repository.VerificarUsuario(login.NombreUsuario, login.Password);
        if (string.IsNullOrEmpty(usuarioVerificar.NombreDeUsuario))
        {
            return RedirectToAction("Index");
        }
        else
        {
            Loguear(usuarioVerificar);
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