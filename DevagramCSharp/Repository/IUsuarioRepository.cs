using DevagramCSharp.Models;

namespace DevagramCSharp.Repository
{
    public interface IUsuarioRepository : IRepositoryGenerico<Usuario>
    {        
        public bool JaTemEsseEmail(string email);
    }
}
