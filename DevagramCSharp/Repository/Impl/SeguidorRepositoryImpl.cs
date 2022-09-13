using DevagramCSharp.Models;

namespace DevagramCSharp.Repository.Impl
{
    public class SeguidorRepositoryImpl : RepositoryGenericoImpl<Seguidor>, ISeguidorRepository
    {
        public SeguidorRepositoryImpl(DevagramContext devagramContext, ILogger<RepositoryGenericoImpl<Seguidor>> logger) : base(devagramContext, logger)
        {
        }
    }
}
