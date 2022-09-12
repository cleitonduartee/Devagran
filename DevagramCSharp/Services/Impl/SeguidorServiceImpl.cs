using DevagramCSharp.Models;
using DevagramCSharp.Repository;
using DevagramCSharp.Utils;

namespace DevagramCSharp.Services.Impl
{
    public class SeguidorServiceImpl : ISeguidorService
    {
        private readonly ISeguidorRepository _repository;
        public SeguidorServiceImpl(ISeguidorRepository seguidorRepository)
        {
            _repository = seguidorRepository;
        }

        public Pacote<string> AtualizaSeguidor(int idUsuarioSeguidor, int idUsuarioSeguido)
        {
            var seguidor = _repository.BuscarSomente(x => x.IdUsuarioSeguido.Equals(idUsuarioSeguido) && x.IdUsuarioSeguidor.Equals(idUsuarioSeguidor));

            if (seguidor != null)
            {
                if (!DesSeguir(seguidor))
                    return Pacote<string>.Error(Enumerators.EStatusCode.ERR_INTERNO, "Erro ao tentar deixar de seguir a pessoa: " + seguidor.UsuarioSeguido.Nome);

                return Pacote<string>.Sucess("Ação atualizada com sucesso!");
            }

            seguidor = new Seguidor()
            {
                IdUsuarioSeguido = idUsuarioSeguido,
                IdUsuarioSeguidor = idUsuarioSeguidor
            };

            if (!Seguir(seguidor))
                return Pacote<string>.Error(Enumerators.EStatusCode.ERR_INTERNO, "Erro ao tentar seguir a pessoa: " + seguidor.UsuarioSeguido.Nome);

            return Pacote<string>.Sucess("Ação cadastrada com sucesso!");

        }

        public bool DesSeguir(Seguidor seguidor)
        {
            return _repository.DesSeguir(seguidor);
        }

        public bool Seguir(Seguidor seguidor)
        {
            return _repository.Seguir(seguidor);
        }
    }
}
