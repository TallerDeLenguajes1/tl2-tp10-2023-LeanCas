using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class TareaController : ControllerBase
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


    [HttpPut]
    [Route("ModificarTarea")]
    public ActionResult<Tarea> ModificarTarea(int id, Tarea tarea)
    {
        repository.Set(id, tarea);
        return Ok("La tarea fue modificada con exito" + tarea);
    }

    //MODIFICAR TAREA POR ESTADO

    [HttpPut]
    [Route("ModificarTareaEstado")]
    public ActionResult<Tarea> ModificarTareaEstado(int id, Tarea tarea)
    {
        repository.Set(id, tarea);
        return Ok("La tarea fue modificada con exito" + tarea);
    }

    [HttpDelete]
    [Route("EliminarTarea")]
    public ActionResult<Tarea> EliminarTarea(int idTarea)
    {
        repository.Delete(idTarea);
        return Ok("La tarea fue eliminada con exito" + idTarea);
    }

    [HttpGet]
    [Route("CantidadTareasPorEstado")]
    public ActionResult<List<Tarea>> CantidadTareasPorEstado(Estado estado)
    {
        var tareas = repository.GetAll();
        var tareasPorEstado = tareas.FindAll(T => T.Estado == estado);
        if (tareasPorEstado == null)
        {
            return BadRequest("No se encontraron tareas con el estado " + estado);
        }
        else
        {
            return Ok("Se encontraron las tareas : " + tareasPorEstado.Count());
        }
    }

    [HttpGet]
    [Route("TareasAsignadasAUsuario")]
    public ActionResult<List<Tarea>> TareasAsignadasAUsuario(int idUsuario)
    {
        var tareas = repository.GetAll();
        var tareasPorUsuario = tareas.FindAll(T => T.IdUsuarioAsignado == idUsuario);
        if (tareasPorUsuario == null)
        {
            return BadRequest("No se encontraron tareas con el Usuario " + idUsuario);
        }
        else
        {
            return Ok("Se encontraron las tareas : " + tareasPorUsuario);
        }
    }

    /* TAREAS ASIGNADAS A UN TABLERO

        [HttpGet]
        [Route("TareasAsignadasATablero")]
        public ActionResult<List<Tarea>> TareasAsignadasATablero(int idTablero)
        {
            var tareas = repository.GetAll();
            var tareasPorUsuario = tareas.FindAll(T => T.);
            if (tareasPorUsuario == null)
            {
                return BadRequest("No se encontraron tareas con el Usuario " + usuario);
            }
            else
            {
                return Ok("Se encontraron las tareas : " + tareasPorUsuario);
            }
        }
    */
}