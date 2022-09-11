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

        public UsuarioServiceImpl(IUsuarioRepository usuarioRepository, IUsuarioMapper usuarioMapper)
        {
            _repository = usuarioRepository;
            _usuarioMapper = usuarioMapper; 
        }

        public Pacote<UsuarioDto> CadastrarUsuario(UsuarioDto usuarioDto)
        {
            var validacoes = ValidarDto(usuarioDto);
            if (validacoes.Any())
                return Pacote<UsuarioDto>.Error(EStatusCode.ERRO_VALIDACAO, validacoes);
            
            var usuario = _usuarioMapper.MapearDtoParaEntidade(usuarioDto);
            usuario.Senha = Utils.MD5Utils.GerarHashMD5(usuario.Senha);
            if (_repository.Salvar(usuario))
            {
                usuarioDto = _usuarioMapper.MapearEntidadeParaDto(usuario);
                return Pacote<UsuarioDto>.Sucess(usuarioDto);
            }
            return Pacote<UsuarioDto>.Error(EStatusCode.ERR_INTERNO, "Erro ao salvar Usuário");
                
        }

        private List<string> ValidarDto(UsuarioDto usuarioDto)
        {
            var validacoes = new List<string>();
            if (usuarioDto == null)
                validacoes.Add("Informe todos os campos do usuário");

            if (string.IsNullOrEmpty(usuarioDto.Nome) || string.IsNullOrWhiteSpace(usuarioDto.Nome))
                validacoes.Add("Nome do usuário inválido.");

            if (string.IsNullOrEmpty(usuarioDto.Senha) || string.IsNullOrWhiteSpace(usuarioDto.Senha))
                validacoes.Add("Senha não informada.");

            if (string.IsNullOrWhiteSpace(usuarioDto.Email) || !usuarioDto.Email.Contains("@"))
                validacoes.Add("E-mail inválido");

            if (_repository.JaTemEsseEmail(usuarioDto.Email))
                validacoes.Add("E-mail já está sendo usado.");

            return validacoes;
        }
    }
}
