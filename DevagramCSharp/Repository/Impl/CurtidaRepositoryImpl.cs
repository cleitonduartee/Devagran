using DevagramCSharp.Models;

namespace DevagramCSharp.Repository.Impl
{
    public class CurtidaRepositoryImpl : RepositoryGenericoImpl<Curtida>, ICurtidaRepository
    {
        public CurtidaRepositoryImpl(DevagramContext devagramContext, ILogger<RepositoryGenericoImpl<Curtida>> logger) : base(devagramContext, logger)
        {
        }
    }
}
