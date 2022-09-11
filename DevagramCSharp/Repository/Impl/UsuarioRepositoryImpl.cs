using DevagramCSharp.Models;
using System.Data.Entity;
using System.Linq.Expressions;

namespace DevagramCSharp.Repository.Impl
{
    public class UsuarioRepositoryImpl : RepositoryGenericoImpl<Usuario>, IUsuarioRepository
    {
        private readonly DevagramContext _context;
        public UsuarioRepositoryImpl(DevagramContext devagramContext, ILogger<RepositoryGenericoImpl<Usuario>> logger) : base(devagramContext, logger)
        {
            _context = devagramContext;
        }

        public bool JaTemEsseEmail(string email)
        {
            return _context.Usuarios.Any(u => u.Email.ToLower().Equals(email.ToLower()));
        }
    }
}
