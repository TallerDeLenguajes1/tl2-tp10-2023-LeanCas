using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tp10_tl2.Models;
using TableroRepository;
using UsuarioRepository;

namespace tp10_tl2.Controllers;

public class TableroController : Controller
{

    private readonly ILogger<TableroController> _logger;

    private readonly ITableroRepositorio repository;

    private readonly IUsuarioRepositorio usuarioRepositorio;

    public TableroController(ILogger<TableroController> logger, ITableroRepositorio TableroRepository, IUsuarioRepositorio usuRepositorio)
    {
        _logger = logger;
        repository = TableroRepository;
        usuarioRepositorio = usuRepositorio;
    }

    public IActionResult Index()
    {
        try
        {
            var ListarTableroViewModel = new ListarTableroViewModel();
            if ((!string.IsNullOrEmpty(HttpContext.Session.GetString("usuario")) && (HttpContext.Session.GetString("rol")) == "Administrador"))
            {
                var tableros = repository.GetAll();
                var tablerosViewModel = new List<ListarTableroViewModel>();
                foreach (Tablero T in tableros)
                {
                    var tableroVM = new ListarTableroViewModel(T, usuarioRepositorio.GetUsuario(T.IdUsuarioPropietario).NombreDeUsuario);
                    tablerosViewModel.Add(tableroVM);
                }
                return View(tablerosViewModel);
            }
            else if (!string.IsNullOrEmpty(HttpContext.Session.GetString("usuario")) && (HttpContext.Session.GetString("rol")) == "Operador")
            {
                var tablerosPorID = repository.GetTableroUsuario(Convert.ToInt32(HttpContext.Session.GetString("id")));
                var tablerosViewModelPorID = new List<ListarTableroViewModel>();
                foreach (Tablero T in tablerosPorID)
                {
                    var tableroVM = new ListarTableroViewModel(T, usuarioRepositorio.GetUsuario(T.IdUsuarioPropietario).NombreDeUsuario);
                    tablerosViewModelPorID.Add(tableroVM);
                }
                return View(tablerosViewModelPorID);
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
    public IActionResult CrearTablero()
    {
        try
        {
            return View(new CrearTableroViewModel());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    [HttpPost]
    public IActionResult CrearTablero(CrearTableroViewModel tableroViewModel)
    {
        try
        {
            if (!ModelState.IsValid) return RedirectToAction("Index");
            var tablero = new Tablero(Convert.ToInt32(HttpContext.Session.GetString("id")), tableroViewModel.Nombre, tableroViewModel.Descripcion);
            repository.Create(tablero);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    [HttpGet]
    public IActionResult ModificarTablero(int id)
    {
        try
        {
            var tablero = repository.GetTablero(id);
            var tableroViewModel = new ModificarTableroViewModel(tablero);
            return View(tableroViewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }

    }

    [HttpPost]
    public IActionResult ModificarTablero(int id, ModificarTableroViewModel tableroViewModel)
    {
        try
        {
            if (!ModelState.IsValid) return RedirectToAction("Index");
            var viewModel = new ModificarTableroViewModel();
            var tablero = viewModel.convertirTablero(tableroViewModel);
            repository.Set(id, tablero);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }

    }

    public IActionResult EliminarTablero(int id)
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

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}