using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tp10_tl2.Models;
using TareaRepository;
using TableroRepository;
using UsuarioRepository;

namespace tp10_tl2.Controllers;

public class TareaController : Controller
{

    private readonly ILogger<TareaController> _logger;

    private readonly ITareaRepositorio repository;
    private readonly ITableroRepositorio TableroRepository;
    private readonly IUsuarioRepositorio UsuarioRepository;
    private readonly ListarTareaViewModel listartareaVM = new ListarTareaViewModel();

    public TareaController(ILogger<TareaController> logger, ITareaRepositorio TareaRepository, ITableroRepositorio tableroRepositorio, IUsuarioRepositorio usuarioRepositorio)
    {
        _logger = logger;
        repository = TareaRepository;
        TableroRepository = tableroRepositorio;
        UsuarioRepository = usuarioRepositorio;
    }

    public IActionResult Index()
    {
        try
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("usuario")))
            {
                var listaTareas = repository.GetAll();
                var tareaslistaVM = new List<ListarTareaViewModel>();
                foreach (Tarea T in listaTareas)
                {
                    tareaslistaVM.Add(listartareaVM.convertirVM(T, UsuarioRepository.GetUsuario(T.IdUsuarioAsignado).NombreDeUsuario, TableroRepository.GetTablero(T.IdTablero).Nombre));
                }

                return View(tareaslistaVM);

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
    public IActionResult CrearTarea(int id)
    {
        try
        {
            return View(new CrearTareaViewModel(GetUsuarios(), GetTableros()));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }

    }

    [HttpPost]
    public IActionResult CrearTarea(CrearTareaViewModel tareaViewModel)
    {
        try
        {
            if (!ModelState.IsValid) return RedirectToAction("Index");
            var tarea = new Tarea(tareaViewModel.Nombre, tareaViewModel.Descripcion, tareaViewModel.Color, tareaViewModel.Estado, tareaViewModel.IdUsuarioAsignado, tareaViewModel.IdTablero);
            repository.Create(tarea.IdTablero, tarea);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }

    }

    [HttpGet]
    public IActionResult TareasUsuario(int id)
    {
        try
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("usuario")))
            {
                var listaTareasAll = repository.GetAll();
                var listaTareas = listaTareasAll.FindAll(T => T.IdUsuarioAsignado == id);
                var tareaslistaVM = new List<ListarTareaViewModel>();
                foreach (Tarea T in listaTareas)
                {
                    tareaslistaVM.Add(listartareaVM.convertirVM(T, UsuarioRepository.GetUsuario(T.IdUsuarioAsignado).NombreDeUsuario, TableroRepository.GetTablero(T.IdTablero).Nombre));
                }
                return View(tareaslistaVM);

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
    public IActionResult ModificarTarea(int id)
    {
        try
        {
            var tarea = repository.GetTarea(id);
            var tareaViewModel = new ModificarTareaViewModel(tarea, GetUsuarios(), GetTableros());
            return View(tareaViewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }

    }

    [HttpPost]
    public IActionResult ModificarTarea(int id, ModificarTareaViewModel tareaViewModel)
    {
        try
        {
            if (!ModelState.IsValid) return RedirectToAction("Index");
            var viewModel = new ModificarTareaViewModel();
            var tarea = viewModel.convertirTarea(tareaViewModel);
            repository.Set(id, tarea);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }

    }

    public IActionResult EliminarTarea(int id)
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

    public IActionResult TareaPorTablero(int id)
    {
        try
        {
            var tareas = repository.GetAll();
            var tareasListVM = new List<ListarTareaViewModel>();
            foreach (Tarea T in tareas)
            {
                if (T.IdTablero == id)
                {
                    var tareaVM = new ListarTareaViewModel(T, UsuarioRepository.GetUsuario(T.IdUsuarioAsignado).NombreDeUsuario, TableroRepository.GetTablero(T.IdTablero).Nombre);
                    tareasListVM.Add(tareaVM);
                }
            }
            return View(tareasListVM);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }

    }

    private List<Usuario> GetUsuarios()
    {
        try
        {
            var listaUsuario = UsuarioRepository.GetAll();
            return listaUsuario;
        }
        catch
        {
            throw;
        }


    }

    private List<Tablero> GetTableros()
    {
        try
        {
            var listaTablero = TableroRepository.GetAll();
            return listaTablero;
        }
        catch
        {
            throw;
        }

    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}