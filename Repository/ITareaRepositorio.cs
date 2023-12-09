namespace TareaRepository
{
    public interface ITareaRepositorio
    {
        public Tarea Create(int idTablero, Tarea tarea);
        public void Set(int id, Tarea tarea);
        public List<Tarea> GetAll();
        public Tarea GetTarea(int id);
        public List<Tarea> GetTareaTablero(int idTablero);
        public void Delete(int idTarea);
        public void Asignar(int idUsuario, int idTarea);
    }
}