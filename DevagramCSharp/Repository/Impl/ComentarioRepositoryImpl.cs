using DevagramCSharp.Models;

namespace DevagramCSharp.Repository.Impl
{
    public class ComentarioRepositoryImpl : RepositoryGenericoImpl<Comentario>, IComentarioRepository
    {
        public ComentarioRepositoryImpl(DevagramContext devagramContext, ILogger<RepositoryGenericoImpl<Comentario>> logger) : base(devagramContext, logger)
        {

        }
    }
}
