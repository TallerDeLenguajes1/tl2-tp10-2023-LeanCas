using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tp10_tl2.Models;

namespace tp10_tl2.Controllers;

public class UsuarioController : Controller
{

    private readonly ILogger<UsuarioController> _logger;

    private readonly IUsuarioRepositorio repository;

    public UsuarioController(ILogger<UsuarioController> logger)
    {
        _logger = logger;
        repository = new UsuarioRepositorio();
    }


    public IActionResult Index()
    {
        var usuarios = repository.GetAll();
        return View(usuarios);
    }

    [HttpGet]
    public IActionResult CrearUsuario()
    {
        return View(new Usuario());
    }

    [HttpPost]
    public IActionResult CrearUsuario(Usuario usuario) // Le añadi el [FromForm] porque sino no anda 
    {
        repository.Create(usuario);
        return RedirectToAction("Index");
    }


    [HttpGet]
    public IActionResult ModificarUsuario(int id)
    {
        var usuario = repository.GetUsuario(id);
        return View(usuario);
    }

    [HttpPost]
    public IActionResult ModificarUsuario(int id, Usuario usuario)
    {
        repository.Set(id, usuario);
        return RedirectToAction("Index");
    }

    public IActionResult EliminarUsuario(int id)
    {
        repository.Delete(id);
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}