using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tp10_tl2.Models;
using UsuarioRepository;

namespace tp10_tl2.Controllers;

public class UsuarioController : Controller
{

    private readonly ILogger<UsuarioController> _logger;
    private readonly IUsuarioRepositorio repository;

    public UsuarioController(ILogger<UsuarioController> logger, IUsuarioRepositorio UserRepository)
    {
        _logger = logger;
        repository = UserRepository;
    }


    public IActionResult Index()
    {
        try
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
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }

    }

    [HttpGet]
    public IActionResult CrearUsuario()
    {
        try
        {
            return View(new CrearUsuarioViewModel());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }

    }

    [HttpPost]
    public IActionResult CrearUsuario(CrearUsuarioViewModel usuarioViewModel)
    {
        try
        {
            if (!ModelState.IsValid) return RedirectToAction("Index");
            if (UsuarioExiste(usuarioViewModel.Nombre, usuarioViewModel.Id))
            {
                ModelState.AddModelError(nameof(usuarioViewModel.Nombre), "El Nombre de Usuario ya está en uso.");
                return View(usuarioViewModel);
            }
            var usuario = new Usuario(usuarioViewModel.Nombre, usuarioViewModel.Password);
            repository.Create(usuario);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("CrearUsuario", usuarioViewModel);
        }

    }


    [HttpGet]
    public IActionResult ModificarUsuario(int id)
    {
        try
        {
            var usuario = repository.GetUsuario(id);
            var usuarioViewModel = new ModificarUsuarioViewModel(usuario);
            return View(usuarioViewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }

    }

    [HttpPost]
    public IActionResult ModificarUsuario(ModificarUsuarioViewModel usuarioViewModel)
    {
        try
        {
            if (!ModelState.IsValid) return RedirectToAction("Index");
            if (UsuarioExiste(usuarioViewModel.Nombre, usuarioViewModel.Id))
            {
                ModelState.AddModelError(nameof(usuarioViewModel.Nombre), "El Nombre de Usuario ya está en uso.");
                return View(usuarioViewModel);
            }
            var viewModel = new ModificarUsuarioViewModel();
            var usuario = viewModel.convertirUsuario(usuarioViewModel);
            repository.Set(usuarioViewModel.Id, usuario);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }

    }

    public IActionResult EliminarUsuario(int id)
    {
        try
        {
            repository.Delete(id);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }

    }

    private bool UsuarioExiste(string? nombre, int id)
    {
        var lista = repository.GetAll();
        foreach (var u in lista)
        {
            if (u.NombreDeUsuario == nombre && u.Id != id)
            {
                return true;
            }
        }
        return false;
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}