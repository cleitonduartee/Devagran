using DevagramCSharp.Dtos;
using DevagramCSharp.Models;
using DevagramCSharp.Utils;

namespace DevagramCSharp.Services
{
    public interface IPublicacaoService
    {
        public Pacote<string> Publicar(PublicacaoRequisicaoDto publicacaoDto, int idUsuario);
        public Pacote<List<PublicacaoFeedRespostaDto>> GetFeedHome(int idUsuario);
        public Pacote<List<PublicacaoFeedRespostaDto>> GetFeedUsuario(int idUsuario);
    }
}
