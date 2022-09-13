using DevagramCSharp.Dtos;
using DevagramCSharp.Models;
using DevagramCSharp.Repository;
using DevagramCSharp.Utils;

namespace DevagramCSharp.Services.Impl
{
    public class ComentarioServiceImpl : IComentarioService
    {
        private readonly IComentarioRepository _repository;
        private readonly ILogger<Comentario> _logger;

        public ComentarioServiceImpl(IComentarioRepository repository, ILogger<Comentario> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public Pacote<string> Comentar(ComentarioRequisicaoDto comentarioDto, int idUsuario)
        {
            var validacoes = ValidarDto(comentarioDto);
            if (validacoes.Any())
            {
                _logger.LogError("Erro de validação.");
                return Pacote<string>.Error(Enumerators.EStatusCode.ERRO_VALIDACAO, validacoes);
            }
                

            var comentario = new Comentario()
            {
                IdUsuario = idUsuario,
                IdPublicacao = comentarioDto.IdPublicacao,
                Descricao = comentarioDto.Descricao
            };

            if (!_repository.Salvar(comentario))
            {
                _logger.LogError("Erro ao salvar comentário.");
                return Pacote<string>.Error(Enumerators.EStatusCode.ERR_INTERNO, "Erro ao salvar comentário.");
            }
                

            return Pacote<string>.Sucess("Comentário salvo com sucesso.");
        }
        public List<string> ValidarDto(ComentarioRequisicaoDto comentarioDto)
        {
            var validacoes = new List<string>();

            if(comentarioDto == null)
            {
                validacoes.Add("Descrição e Publicação são obrigatórias.");
                return validacoes;
            }
            if (string.IsNullOrEmpty(comentarioDto.Descricao) || string.IsNullOrWhiteSpace(comentarioDto.Descricao))
                validacoes.Add("Descrição inválida.");
            if (comentarioDto.IdPublicacao == 0)
                validacoes.Add("Publicação não encontrada");

            return validacoes;
        }
    }
}
