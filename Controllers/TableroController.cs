using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tp10_tl2.Models;

namespace tp10_tl2.Controllers;

public class TableroController : Controller
{

    private readonly ILogger<TableroController> _logger;

    private readonly ITableroRepositorio repository;

    public TableroController(ILogger<TableroController> logger)
    {
        _logger = logger;
        repository = new TableroRepositorio();
    }

    public IActionResult Index()
    {
        var tableros = repository.GetAll();
        return View(tableros);
    }

    [HttpGet]
    public IActionResult CrearTablero()
    {
        return View(new Tablero());
    }

    [HttpPost]
    public IActionResult CrearTablero(Tablero tablero) // Le añadi el [FromForm] porque sino no anda 
    {
        repository.Create(tablero);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult ModificarTablero(int id)
    {
        var tablero = repository.GetTablero(id);
        return View(tablero);
    }

    [HttpPost]
    public IActionResult ModificarTablero(int id, Tablero tablero)
    {
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