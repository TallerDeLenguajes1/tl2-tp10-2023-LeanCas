namespace TableroRepository;

public interface ITableroRepositorio
{
    public void Create(Tablero usuario);
    public void Set(int id, Tablero usuario);
    public List<Tablero> GetAll();
    public Tablero GetTablero(int id);
    public List<Tablero> GetTableroUsuario(int idUsuario);
    public void Delete(int id);
}