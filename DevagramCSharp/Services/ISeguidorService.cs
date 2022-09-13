using DevagramCSharp.Models;
using DevagramCSharp.Utils;

namespace DevagramCSharp.Services
{
    public interface ISeguidorService
    {
        public Pacote<string> AtualizaSeguidor(int idUsuarioSeguidor, int idUsuarioSeguido);
    }
}
