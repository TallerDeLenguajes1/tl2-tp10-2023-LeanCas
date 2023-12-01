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

        if (!string.IsNullOrEmpty(HttpContext.Session.GetString("usuario")) && (HttpContext.Session.GetString("rol")) == "Administrador")
        {
            var usuarios = repository.GetAll();
            var listarUsuarioViewModel = new ListarUsuarioViewModel();
            var usuariosViewModel = listarUsuarioViewModel.convertirLista(usuarios);
            return View(usuariosViewModel);
        }
        else
        {
            return RedirectToRoute(new { controller = "Logueo", action = "Index" });
        }
    }

    [HttpGet]
    public IActionResult CrearUsuario()
    {
        return View(new CrearUsuarioViewModel());
    }

    [HttpPost]
    public IActionResult CrearUsuario(CrearUsuarioViewModel usuarioViewModel)
    {
        var usuario = new Usuario(usuarioViewModel.Nombre);
        repository.Create(usuario);
        return RedirectToAction("Index");
    }


    [HttpGet]
    public IActionResult ModificarUsuario(int id)
    {
        var usuario = repository.GetUsuario(id);
        var usuarioViewModel = new ModificarUsuarioViewModel(usuario);
        return View(usuarioViewModel);
    }

    [HttpPost]
    public IActionResult ModificarUsuario(int id, ModificarUsuarioViewModel usuarioViewModel)
    {
        var viewModel = new ModificarUsuarioViewModel();
        var usuario = viewModel.convertirUsuario(usuarioViewModel);
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