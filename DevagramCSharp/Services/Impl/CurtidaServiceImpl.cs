using DevagramCSharp.Models;
using DevagramCSharp.Repository;
using DevagramCSharp.Utils;

namespace DevagramCSharp.Services.Impl
{
    public class CurtidaServiceImpl : ICurtidaService
    {
        private readonly ICurtidaRepository _repository;
        private readonly ILogger<Curtida> _logger;
        public CurtidaServiceImpl(ICurtidaRepository repository, ILogger<Curtida> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public Pacote<string> CurtirOuDescurtir(int idPublicacao, int idUsuario)
        {
            if(idPublicacao == 0)
            {
                _logger.LogError("Publicação deve ser informada.");
                return Pacote<string>.Error(Enumerators.EStatusCode.ERRO_VALIDACAO, "Publicação deve ser informada.");
            }

            var curtida = _repository.BuscarSomente(x => x.IdPublicacao.Equals(idPublicacao) && x.IdUsuario.Equals(idUsuario));

            if(curtida != null)
            {
                if (!_repository.Excluir(curtida))
                {
                    _logger.LogError("Erro ao descurtir publicação de id: " + curtida.IdPublicacao);
                    return Pacote<string>.Error(Enumerators.EStatusCode.ERR_INTERNO, "Erro ao descurtir publicação de id: " + curtida.IdPublicacao);
                }
                return Pacote<string>.Sucess("Descurtida salva com sucesso.");
            }

            curtida = new Curtida()
            {
                IdPublicacao = idPublicacao,
                IdUsuario = idUsuario,
            };
            if (!_repository.Salvar(curtida))
            {
                _logger.LogError("Erro ao curtir publicação de id: " + curtida.IdPublicacao);
                return Pacote<string>.Error(Enumerators.EStatusCode.ERR_INTERNO, "Erro ao descurtir publicação de id: " + curtida.IdPublicacao);
            }
            return Pacote<string>.Sucess("Curtida salva com sucesso.");
        }
    }
}
