using DevagramCSharp.Dtos;
using DevagramCSharp.Models;
using DevagramCSharp.Repository;
using DevagramCSharp.Utils;

namespace DevagramCSharp.Services.Impl
{
    public class PublicacaoServiceImpl : IPublicacaoService
    {
        private readonly IPublicacaoRepository _repository;
        private readonly ICosmicService _cosmicService;
        private readonly ILogger<Publicacao> _logger;
        private readonly ICurtidaRepository _curtidaRepository;
        private readonly IComentarioRepository _comentarioRepository;
        public PublicacaoServiceImpl(IPublicacaoRepository repository, ICosmicService cosmicService, 
                                     ILogger<Publicacao> logger, ICurtidaRepository curtidaRepository,
                                     IComentarioRepository comentarioRepository)
        {
            _repository = repository;
            _cosmicService = cosmicService;
            _logger = logger;
            _curtidaRepository = curtidaRepository;
            _comentarioRepository = comentarioRepository;
        }

        public Pacote<List<PublicacaoFeedRespostaDto>> GetFeedHome(int idUsuario)
        {
           var pubFeed = _repository.GetFeedHome(idUsuario);
            PopulaComentariosECurtidas(ref pubFeed);
            //foreach(var feed in pubFeed)
            //{
            //    var comentarios = _comentarioRepository
            //                       .BuscarTodosPor(c => c.IdPublicacao.Equals(feed.IdPublicacao))
            //                       .Select(c => new ComentarioDto { Id = c.Id, IdUsuario = c.IdUsuario, Descricao = c.Descricao })
            //                       .ToList();
            //                                            ;
            //    var curtidas = _curtidaRepository
            //                    .BuscarTodosPor(c => c.IdPublicacao.Equals(feed.IdPublicacao))
            //                    .Select(c => new CurtidaDto { Id = c.Id, IdUsuario = c.IdUsuario})
            //                    .ToList();

            //    feed.Comentarios = comentarios;
            //    feed.Curtidas = curtidas;
            //}

            return Pacote<List<PublicacaoFeedRespostaDto>>.Sucess(pubFeed);
        }

        public Pacote<List<PublicacaoFeedRespostaDto>> GetFeedUsuario(int idUsuario)
        {
            var pubFeed = _repository.GetFeedUsuario(idUsuario);
            PopulaComentariosECurtidas(ref pubFeed);
            //foreach (var feed in pubFeed)
            //{
            //    var comentarios = _comentarioRepository
            //                       .BuscarTodosPor(c => c.IdPublicacao.Equals(feed.IdPublicacao))
            //                       .Select(c => new ComentarioDto { Id = c.Id, IdUsuario = c.IdUsuario, Descricao = c.Descricao })
            //                       .ToList();
            //    ;
            //    var curtidas = _curtidaRepository
            //                    .BuscarTodosPor(c => c.IdPublicacao.Equals(feed.IdPublicacao))
            //                    .Select(c => new CurtidaDto { Id = c.Id, IdUsuario = c.IdUsuario })
            //                    .ToList();

            //    feed.Comentarios = comentarios;
            //    feed.Curtidas = curtidas;
            //}
            return Pacote<List<PublicacaoFeedRespostaDto>>.Sucess(pubFeed);
        }

        public Pacote<string> Publicar(PublicacaoRequisicaoDto publicacaoDto, int idUsuario)
        {
            var validacoes = ValidarDto(publicacaoDto);
            if (validacoes.Any())
            {
                _logger.LogError("Erro de validação.");
                return Pacote<string>.Error(Enumerators.EStatusCode.ERRO_VALIDACAO, validacoes);
            }
                

            var publicacao = new Publicacao()
            {
                Descricao = publicacaoDto.Descricao,
                IdUsuario = idUsuario
            };

            if (publicacaoDto.Foto != null)
            {
                var imagemDto = new ImagemDto()
                {
                    Nome = "Publicacao",
                    Imagem = publicacaoDto.Foto,
                };
                publicacao.UrlFotoPublicacao = _cosmicService.EnviarImagem(imagemDto);
            }

            if (!_repository.Salvar(publicacao))
            {
                _logger.LogError("Erro ao tentar salvar publicação.");
                return Pacote<string>.Error(Enumerators.EStatusCode.ERR_INTERNO, "Erro ao tentar salvar publicação.");
            }
                

            return Pacote<string>.Sucess("Publicação salva com sucesso.");
        }
        public List<string> ValidarDto(PublicacaoRequisicaoDto dto)
        {
            var validacoes = new List<string>();

            if (dto == null)
            {
                validacoes.Add("Descrição e foto são obrigatório");
                return validacoes;
            }
            if (string.IsNullOrEmpty(dto.Descricao) || string.IsNullOrWhiteSpace(dto.Descricao))
                validacoes.Add("Descrição inválida.");
            if (dto.Foto == null)
                validacoes.Add("Foto é obrigatório na publicação.");

            return validacoes;
        }
        private void PopulaComentariosECurtidas(ref List<PublicacaoFeedRespostaDto> listPubFeed)
        {
            foreach (var feed in listPubFeed)
            {
                var comentarios = _comentarioRepository
                                   .BuscarTodosPor(c => c.IdPublicacao.Equals(feed.IdPublicacao))
                                   .Select(c => new ComentarioDto { Id = c.Id, IdUsuario = c.IdUsuario, Descricao = c.Descricao })
                                   .ToList();
                ;
                var curtidas = _curtidaRepository
                                .BuscarTodosPor(c => c.IdPublicacao.Equals(feed.IdPublicacao))
                                .Select(c => new CurtidaDto { Id = c.Id, IdUsuario = c.IdUsuario })
                                .ToList();

                feed.Comentarios = comentarios;
                feed.Curtidas = curtidas;
            }
        }
    }
}
