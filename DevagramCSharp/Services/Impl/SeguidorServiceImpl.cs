using DevagramCSharp.Models;
using DevagramCSharp.Repository;
using DevagramCSharp.Utils;

namespace DevagramCSharp.Services.Impl
{
    public class SeguidorServiceImpl : ISeguidorService
    {
        private readonly ISeguidorRepository _repository;
        private readonly ILogger<Seguidor> _logger;
        public SeguidorServiceImpl(ISeguidorRepository seguidorRepository, ILogger<Seguidor> logger)
        {
            _repository = seguidorRepository;
            _logger = logger;
        }

        public Pacote<string> AtualizaSeguidor(int idUsuarioSeguidor, int idUsuarioSeguido)
        {
            var seguidor = _repository.BuscarSomente(x => x.IdUsuarioSeguido.Equals(idUsuarioSeguido) && x.IdUsuarioSeguidor.Equals(idUsuarioSeguidor));

            if (seguidor != null)
            {
                if (!_repository.Excluir(seguidor))
                {
                    _logger.LogError("Erro ao tentar deixar de seguir a pessoa: " + seguidor.UsuarioSeguido.Nome);
                    return Pacote<string>.Error(Enumerators.EStatusCode.ERR_INTERNO, "Erro ao tentar deixar de seguir a pessoa: " + seguidor.UsuarioSeguido.Nome);
                }
                    

                return Pacote<string>.Sucess("Ação atualizada com sucesso!");
            }

            seguidor = new Seguidor()
            {
                IdUsuarioSeguido = idUsuarioSeguido,
                IdUsuarioSeguidor = idUsuarioSeguidor
            };

            if (!_repository.Salvar(seguidor))
            {
                _logger.LogError("Erro ao tentar seguir a pessoa: " + seguidor.UsuarioSeguido.Nome);
                return Pacote<string>.Error(Enumerators.EStatusCode.ERR_INTERNO, "Erro ao tentar seguir a pessoa: " + seguidor.UsuarioSeguido.Nome);
            }                

            return Pacote<string>.Sucess("Ação cadastrada com sucesso!");
        }
    }
}
