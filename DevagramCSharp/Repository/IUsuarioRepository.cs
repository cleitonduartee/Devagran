using DevagramCSharp.Models;
using System.Linq.Expressions;

namespace DevagramCSharp.Repository
{
    public interface IUsuarioRepository : IRepositoryGenerico<Usuario>
    {        
        public bool JaTemEsseEmail(string email);
    }
}
