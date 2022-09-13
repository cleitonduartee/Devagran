using DevagramCSharp.Models;

namespace DevagramCSharp.Repository.Impl
{
    public class UsuarioRepositoryImpl : RepositoryGenericoImpl<Usuario>, IUsuarioRepository
    {      
        public UsuarioRepositoryImpl(DevagramContext devagramContext, ILogger<RepositoryGenericoImpl<Usuario>> logger) : base(devagramContext, logger)
        {
        }

        public bool JaTemEsseEmail(string email)
        {
            return _contexto.Usuarios.Any(u => u.Email.ToLower().Equals(email.ToLower()));
        }
    }
}
