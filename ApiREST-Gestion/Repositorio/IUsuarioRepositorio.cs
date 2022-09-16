using ApiREST_Gestion.Modelos;

namespace ApiREST_Gestion.Repositorio
{
    public interface IUsuarioRepositorio
    {
        Task<int> Registrar(Usuario usuario, string password);
        Task<string> Login(string nombreUsuario, string password);
        Task<bool> ExisteUsuario(string nombreUsuario);
    }
}
