using DevagramCSharp.Dtos;
using DevagramCSharp.Models;

namespace DevagramCSharp.Repository
{
    public interface IPublicacaoRepository : IRepositoryGenerico<Publicacao>
    {
        public List<PublicacaoFeedRespostaDto> GetFeedHome(int idUsuario);
        public List<PublicacaoFeedRespostaDto> GetFeedUsuario(int idUsuario);
    }
}
