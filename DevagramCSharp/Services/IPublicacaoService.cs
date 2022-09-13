using DevagramCSharp.Dtos;
using DevagramCSharp.Models;
using DevagramCSharp.Utils;

namespace DevagramCSharp.Services
{
    public interface IPublicacaoService
    {
        public Pacote<string> Publicar(PublicacaoRequisicaoDto publicacaoDto, int idUsuario);
    }
}
