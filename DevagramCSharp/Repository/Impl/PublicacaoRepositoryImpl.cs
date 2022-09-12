using DevagramCSharp.Models;

namespace DevagramCSharp.Repository.Impl
{
    public class PublicacaoRepositoryImpl : RepositoryGenericoImpl<Publicacao>, IPublicacaoRepository
    {
        public PublicacaoRepositoryImpl(DevagramContext devagramContext, ILogger<RepositoryGenericoImpl<Publicacao>> logger) : base(devagramContext, logger)
        {
        }
    }
}
