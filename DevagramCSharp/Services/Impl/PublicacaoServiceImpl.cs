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
        public PublicacaoServiceImpl(IPublicacaoRepository repository, ICosmicService cosmicService)
        {
            _repository = repository;
            _cosmicService = cosmicService;
        }

        public Pacote<string> Publicar(PublicacaoRequisicaoDto publicacaoDto, int idUsuario)
        {
            var validacoes = ValidarDto(publicacaoDto);
            if(validacoes.Any())
                return Pacote<string>.Error(Enumerators.EStatusCode.ERRO_VALIDACAO, validacoes);

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
                return Pacote<string>.Error(Enumerators.EStatusCode.ERR_INTERNO, "Erro ao tentar salvar publicação.");

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
    }
}
