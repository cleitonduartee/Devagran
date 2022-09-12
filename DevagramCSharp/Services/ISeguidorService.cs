using DevagramCSharp.Models;
using DevagramCSharp.Utils;

namespace DevagramCSharp.Services
{
    public interface ISeguidorService
    {
        public bool Seguir(Seguidor seguidor);
        public bool DesSeguir(Seguidor seguidor);
        public Pacote<string> AtualizaSeguidor(int idUsuarioSeguidor, int idUsuarioSeguido);
    }
}
