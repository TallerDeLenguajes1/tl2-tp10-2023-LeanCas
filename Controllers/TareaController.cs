using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tp10_tl2.Models;

namespace tp10_tl2.Controllers;

public class TareaController : Controller
{

    private readonly ILogger<TareaController> _logger;

    private readonly ITareaRepositorio repository;

    public TareaController(ILogger<TareaController> logger)
    {
        _logger = logger;
        repository = new TareaRepositorio();
    }
    /* CREAR TAREA
        [HttpPost]
        [Route("CrearTarea")]
        public ActionResult<Usuario> CrearTarea(Tarea tarea)
        {
            repository.Create()
            return Ok("El usuario fue creado con exito" + usuario);
        }
    */

    public IActionResult Index()
    {
        if (!string.IsNullOrEmpty(HttpContext.Session.GetString("usuario")) && (HttpContext.Session.GetString("rol")) == "Administrador")
        {
            var listaTareas = repository.GetAll();
            var listaTareasViewModel = new ListarTareaViewModel();
            var tareasViewModel = listaTareasViewModel.convertirLista(listaTareas);
            return View(tareasViewModel);
        }
        else
        {
            return RedirectToRoute(new { controller = "Logueo", action = "Index" });
        }

    }

    [HttpGet]
    public IActionResult CrearTarea(int id)
    {
        return View(new CrearTareaViewModel());
    }

    [HttpPost]
    public IActionResult CrearTarea(int id, CrearTareaViewModel tareaViewModel)
    {
        if (!ModelState.IsValid) return RedirectToAction("Index");
        var tarea = new Tarea(tareaViewModel.Nombre, tareaViewModel.Descripcion, tareaViewModel.Color, tareaViewModel.Estado, tareaViewModel.IdUsuarioAsignado);
        repository.Create(id, tarea);
        return RedirectToAction("Index");
    }


    [HttpGet]
    public IActionResult TareasUsuario(int id)
    {
        var listaTareas = repository.GetAll();
        var listaTareasViewModel = new ListarTareaViewModel();
        var tareasViewModel = listaTareasViewModel.convertirLista(listaTareas);
        var listaTareasPorId = tareasViewModel.FindAll(T => T.IdUsuarioAsignado == id);
        return View(listaTareasPorId);
    }

    [HttpGet]
    public IActionResult ModificarTarea(int id)
    {
        var tarea = repository.GetTarea(id);
        var tareaViewModel = new ModificarTareaViewModel(tarea);
        return View(tareaViewModel);
    }

    [HttpPost]
    public IActionResult ModificarTarea(int id, ModificarTareaViewModel tareaViewModel)
    {
        if (!ModelState.IsValid) return RedirectToAction("Index");
        var viewModel = new ModificarTareaViewModel();
        var tarea = viewModel.convertirTarea(tareaViewModel);
        repository.Set(id, tarea);
        return RedirectToAction("Index");
    }

    public IActionResult EliminarTarea(int id)
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