using DevagramCSharp.Models;

namespace DevagramCSharp.Repository
{
    public interface ISeguidorRepository : IRepositoryGenerico<Seguidor>
    {
        public bool Seguir(Seguidor seguidor);
        public bool DesSeguir(Seguidor seguidor);
    }
}
