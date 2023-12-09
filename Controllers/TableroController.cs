using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tp10_tl2.Models;
using TableroRepository;

namespace tp10_tl2.Controllers;

public class TableroController : Controller
{

    private readonly ILogger<TableroController> _logger;

    private readonly ITableroRepositorio repository;

    public TableroController(ILogger<TableroController> logger, ITableroRepositorio TableroRepository)
    {
        _logger = logger;
        repository = TableroRepository;
    }

    public IActionResult Index()
    {
        var ListarTableroViewModel = new ListarTableroViewModel();
        if ((!string.IsNullOrEmpty(HttpContext.Session.GetString("usuario")) && (HttpContext.Session.GetString("rol")) == "Administrador"))
        {
            var tableros = repository.GetAll();
            var tablerosViewModel = ListarTableroViewModel.convertirLista(tableros);
            return View(tablerosViewModel);
        }
        else if (!string.IsNullOrEmpty(HttpContext.Session.GetString("usuario")) && (HttpContext.Session.GetString("rol")) == "Operador")
        {
            var tablerosPorID = repository.GetTableroUsuario(Convert.ToInt32(HttpContext.Session.GetString("id")));
            var tablerosPorIDViewModel = ListarTableroViewModel.convertirLista(tablerosPorID);
            return View(tablerosPorIDViewModel);
        }
        else
        {
            return RedirectToRoute(new { controller = "Logueo", action = "Index" });
        }
    }

    [HttpGet]
    public IActionResult CrearTablero()
    {
        return View(new CrearTableroViewModel());
    }

    [HttpPost]
    public IActionResult CrearTablero(CrearTableroViewModel tableroViewModel)
    {
        if (!ModelState.IsValid) return RedirectToAction("Index");
        var tablero = new Tablero(tableroViewModel.IdUsuarioPropietario, tableroViewModel.Nombre, tableroViewModel.Descripcion);
        repository.Create(tablero);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult ModificarTablero(int id)
    {
        var tablero = repository.GetTablero(id);
        var tableroViewModel = new ModificarTableroViewModel(tablero);
        return View(tableroViewModel);
    }

    [HttpPost]
    public IActionResult ModificarTablero(int id, ModificarTableroViewModel tableroViewModel)
    {
        if (!ModelState.IsValid) return RedirectToAction("Index");
        var viewModel = new ModificarTableroViewModel();
        var tablero = viewModel.convertirTablero(tableroViewModel);
        repository.Set(id, tablero);
        return RedirectToAction("Index");
    }

    public IActionResult EliminarTablero(int id)
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