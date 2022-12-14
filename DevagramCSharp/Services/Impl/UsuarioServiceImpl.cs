using DevagramCSharp.Dtos;
using DevagramCSharp.Enumerators;
using DevagramCSharp.IMapper;
using DevagramCSharp.Mapper;
using DevagramCSharp.Migrations;
using DevagramCSharp.Models;
using DevagramCSharp.Repository;
using DevagramCSharp.Utils;

namespace DevagramCSharp.Services.Impl
{
    public class UsuarioServiceImpl : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;
        private readonly IUsuarioMapper _usuarioMapper;
        private readonly ICosmicService _cosmicService;
        private readonly IPublicacaoService _publicacaoService;
        private readonly ISeguidorService _seguidorService;
        private readonly ILogger<Usuario> _logger;

        public UsuarioServiceImpl(IUsuarioRepository usuarioRepository, IUsuarioMapper usuarioMapper, ICosmicService cosmicService,
                                  IPublicacaoService publicacaoService, ISeguidorService seguidorService, ILogger<Usuario> logger)
        {
            _repository = usuarioRepository;
            _usuarioMapper = usuarioMapper;
            _cosmicService = cosmicService; 
            _publicacaoService = publicacaoService;
            _seguidorService = seguidorService;
            _logger = logger;
        }

        public Pacote<string> AtualizarUsuario(UsuarioRequisicaoDto usuarioReqDto, Usuario usuarioDB)
        {
            var validacoes = ValidarDto(usuarioReqDto, true);
            if (validacoes.Any())
            {
                _logger.LogError("Erro de validação.");
                return Pacote<string>.Error(EStatusCode.ERRO_VALIDACAO, validacoes);
            }
                
                        
            if(usuarioDB == null)
            {
                _logger.LogError("Usuário não encontrado.");
                return Pacote<string>.Error(EStatusCode.NAO_ENCONTRADO, "Usuário não encontrado.");
            }
                


            usuarioDB.Nome = usuarioReqDto.Nome;
            usuarioDB.UrlFotoPerfil = _cosmicService.EnviarImagem(new ImagemDto(){
                Nome = usuarioReqDto.Nome,
                Imagem = usuarioReqDto.FotoPerfil,
            });

            if (!_repository.Atualizar(usuarioDB))
            {
                _logger.LogError("Erro ao atualizar Usuário.");
                return Pacote<string>.Error(EStatusCode.ERR_INTERNO, "Erro ao atualizar Usuário.");
            }
                
            return Pacote<string>.Sucess("Usuário atualizado com sucesso.");
        }

        public Pacote<string> CadastrarUsuario(UsuarioRequisicaoDto usuarioReqDto)
        {
            var validacoes = ValidarDto(usuarioReqDto, false);
            if (validacoes.Any())
            {
                _logger.LogError("Erro de validação.");
                return Pacote<string>.Error(EStatusCode.ERRO_VALIDACAO, validacoes);
            }                
            
            var usuario = _usuarioMapper.MapearDtoParaEntidade(usuarioReqDto);
            usuario.UrlFotoPerfil = _cosmicService.EnviarImagem(new ImagemDto()
            {
                Nome = usuario.Nome.Replace(" ", ""),
                Imagem = usuarioReqDto.FotoPerfil
            }); 
            usuario.Senha = Utils.MD5Utils.GerarHashMD5(usuario.Senha);
            if (!_repository.Salvar(usuario))
            {
                _logger.LogError("Erro ao salvar Usuário.");
                return Pacote<string>.Error(EStatusCode.ERR_INTERNO, "Erro ao salvar Usuário.");
            }                
            return Pacote<string>.Sucess("Usuário cadastrado com sucesso.");
        }

        public void ConfigUsuarioPesquisa(ref UsuarioRespostaPesquisaDto usuarioDto)
        {
            var qtdPublic = _publicacaoService.QtdPublicacaoPorUsuario(usuarioDto.Id);
            var qtdSeguidores = _seguidorService.QtdSeguidorPorUsuario(usuarioDto.Id);
            var qtdSeguindo = _seguidorService.QtdUsuarioEstaSeguindo(usuarioDto.Id);

            usuarioDto.QtdPublicacoes = qtdPublic;
            usuarioDto.QtdSeguindo = qtdSeguindo;
            usuarioDto.QtdSeguidores = qtdSeguidores;
        }

        public Pacote<LoginRespostaDto> EfetuarLogin(LoginRequisicaoDto login)
        {
            if (String.IsNullOrEmpty(login.Email) || String.IsNullOrEmpty(login.Senha))
            {
                _logger.LogError("Informe o e-mail e senha.");
                return Pacote<LoginRespostaDto>.Error(EStatusCode.ERRO_VALIDACAO, "Informe o e-mail e senha.");
            }
                

            var usuario = _repository.BuscarSomente(x => x.Email.Equals(login.Email) && x.Senha.Equals(MD5Utils.GerarHashMD5(login.Senha)));
            if (usuario == null)
            {
                _logger.LogError("Usuário ou senha inválida.");
                return Pacote<LoginRespostaDto>.Error(EStatusCode.ERRO_AUTENTICACAO, "Usuário ou senha inválida.");
            }
                

            var LoginResposta = new LoginRespostaDto()
            {
                Nome = usuario.Nome,
                Email = usuario.Email,
                Token = TokenServiceImpl.CriarToken(usuario)
            };
            return Pacote<LoginRespostaDto>.Sucess(LoginResposta);        
        }

        public Usuario GetUsuarioPorID(int id)
        {
            return _repository.BuscarPorID(id);
        }
        public List<Usuario> GetUsuarioPorNome(string nome)
        {
            return _repository.BuscarTodosPor(c => c.Nome.Contains(nome));
        }

        public Pacote<UsuarioDto> MapearEntidadeParaUsuarioDto(Usuario usuario)
        {
            var usuarioDto = _usuarioMapper.MapearEntidadeParaUsuarioDto(usuario);
            return Pacote<UsuarioDto>.Sucess(usuarioDto);
        }

        private List<string> ValidarDto(UsuarioRequisicaoDto usuarioDto, bool ehEdicao)
        {
            var validacoes = new List<string>();
            if (usuarioDto == null)
                validacoes.Add("Informe todos os campos do usuário");

            if (string.IsNullOrEmpty(usuarioDto.Nome) || string.IsNullOrWhiteSpace(usuarioDto.Nome))
                validacoes.Add("Nome do usuário inválido.");

            if (!ehEdicao && (string.IsNullOrEmpty(usuarioDto.Senha) || string.IsNullOrWhiteSpace(usuarioDto.Senha)))
                validacoes.Add("Senha não informada.");

            if (!ehEdicao && (string.IsNullOrWhiteSpace(usuarioDto.Email) || !usuarioDto.Email.Contains("@")))
                validacoes.Add("E-mail inválido");

            if (!ehEdicao && _repository.JaTemEsseEmail(usuarioDto.Email))
                validacoes.Add("E-mail já está sendo usado.");

            return validacoes;
        }
    }
}
