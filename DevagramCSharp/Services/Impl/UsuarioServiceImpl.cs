﻿using DevagramCSharp.Dtos;
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

        public UsuarioServiceImpl(IUsuarioRepository usuarioRepository, IUsuarioMapper usuarioMapper, ICosmicService cosmicService)
        {
            _repository = usuarioRepository;
            _usuarioMapper = usuarioMapper;
            _cosmicService = cosmicService;   
        }

        public Pacote<UsuarioDto> CadastrarUsuario(UsuarioRequisicaoDto usuarioReqDto)
        {
            var validacoes = ValidarDto(usuarioReqDto);
            if (validacoes.Any())
                return Pacote<UsuarioDto>.Error(EStatusCode.ERRO_VALIDACAO, validacoes);
            
            var usuario = _usuarioMapper.MapearDtoParaEntidade(usuarioReqDto);
            usuario.UrlFotoPerfil = _cosmicService.EnviarImagem(new ImagemDto()
            {
                Nome = usuario.Nome.Replace(" ", ""),
                Imagem = usuarioReqDto.FotoPerfil
            }); 
            usuario.Senha = Utils.MD5Utils.GerarHashMD5(usuario.Senha);
            if (_repository.Salvar(usuario))
            {
                var usuarioDto = _usuarioMapper.MapearEntidadeParaUsuarioDto(usuario);
                return Pacote<UsuarioDto>.Sucess(usuarioDto);
            }
            return Pacote<UsuarioDto>.Error(EStatusCode.ERR_INTERNO, "Erro ao salvar Usuário");
                
        }
        public Pacote<LoginRespostaDto> EfetuarLogin(LoginRequisicaoDto login)
        {
            if (String.IsNullOrEmpty(login.Email) || String.IsNullOrEmpty(login.Senha))
                return Pacote<LoginRespostaDto>.Error(EStatusCode.ERRO_VALIDACAO, "Informe o e-mail e senha.");

            var usuario = _repository.BuscarSomente(x => x.Email.Equals(login.Email) && x.Senha.Equals(MD5Utils.GerarHashMD5(login.Senha)));
            if (usuario == null)
                return Pacote<LoginRespostaDto>.Error(EStatusCode.ERRO_AUTENTICACAO, "Usuário ou senha inválida.");

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

        private List<string> ValidarDto(UsuarioRequisicaoDto usuarioDto)
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
