
namespace UsuarioRepository;
public interface IUsuarioRepositorio
{
    public void Create(Usuario usuario);
    public void Set(int id, Usuario usuario);
    public List<Usuario> GetAll();
    public Usuario GetUsuario(int? id);

    public Usuario VerificarUsuario(string usuario, string password);
    public void Delete(int id);
}