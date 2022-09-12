using DevagramCSharp.Models;

namespace DevagramCSharp.Repository.Impl
{
    public class SeguidorRepositoryImpl : RepositoryGenericoImpl<Seguidor>, ISeguidorRepository
    {
        public SeguidorRepositoryImpl(DevagramContext devagramContext, ILogger<RepositoryGenericoImpl<Seguidor>> logger) : base(devagramContext, logger)
        {
        }

        public bool Seguir(Seguidor seguidor)
        {
            try
            {
                _contexto.Add(seguidor);
                _contexto.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool DesSeguir(Seguidor seguidor)
        {
            try
            {
                _contexto.Remove(seguidor);
                _contexto.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
