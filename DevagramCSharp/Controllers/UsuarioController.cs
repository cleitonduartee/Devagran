using DevagramCSharp.Dtos;
using DevagramCSharp.Enumerators;
using DevagramCSharp.IMapper;
using DevagramCSharp.Mapper;
using DevagramCSharp.Migrations;
using DevagramCSharp.Models;
using DevagramCSharp.Repository;
using DevagramCSharp.Services;
using DevagramCSharp.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevagramCSharp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : BaseController
    {

        public UsuarioController(IUsuarioService usuarioService) : base(usuarioService)
        {
        }
        [HttpGet]
        public IActionResult ObterUsuario()
        {
            var usuario = ObterUsuarioLogado();
            if (usuario == null)
                return Unauthorized("Por gentileza, fazer login novamente.");
            return Ok(_usuarioService.MapearEntidadeParaUsuarioDto(usuario));
        }

        [HttpGet()]
        [Route("PesquisaUsuarioPorId/{idUsuario}")]
        public IActionResult PesquisaUsuarioPorId(int idUsuario)
        {
            var usuarioLogado = ObterUsuarioLogado();
            if (usuarioLogado == null)
                return Unauthorized("Por gentileza, fazer login novamente.");

            var usuario = _usuarioService.GetUsuarioPorID(idUsuario);
            if(usuario == null)
                return NotFound(Pacote<string>.Error(EStatusCode.NAO_ENCONTRADO, "Usuário não encontrado."));

            var usuarioPesquisaDto = new UsuarioRespostaPesquisaDto()
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                UrlFotoPerfil = usuario.UrlFotoPerfil
            };
            _usuarioService.ConfigUsuarioPesquisa(ref usuarioPesquisaDto);

            return Ok(Pacote<UsuarioRespostaPesquisaDto>.Sucess(usuarioPesquisaDto));
        }
        [HttpGet()]
        [Route("PesquisaUsuarioPorNome/{nome}")]
        public IActionResult PesquisaUsuarioPorNome(string nome)
        {
            var usuarioLogado = ObterUsuarioLogado();
            if (usuarioLogado == null)
                return Unauthorized("Por gentileza, fazer login novamente.");

            var usuarioList = _usuarioService.GetUsuarioPorNome(nome);
            if (!usuarioList.Any())
                return Ok(Pacote<List<Usuario>>.Sucess(usuarioList));

            var usuarioPesquisaDtoList = new List<UsuarioRespostaPesquisaDto>();
            foreach(var usuario in usuarioList)
            {
                var usuarioPesquisaDto = new UsuarioRespostaPesquisaDto()
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    Email = usuario.Email,
                    UrlFotoPerfil = usuario.UrlFotoPerfil
                };
                _usuarioService.ConfigUsuarioPesquisa(ref usuarioPesquisaDto);
                usuarioPesquisaDtoList.Add(usuarioPesquisaDto);
            }
            return Ok(Pacote<List<UsuarioRespostaPesquisaDto>>.Sucess(usuarioPesquisaDtoList));
        }

        [HttpPut("AtualizarUsuario")]
        public IActionResult AtualizarUsuario([FromForm] UsuarioRequisicaoDto usuarioDto)
        {
            var usuario = ObterUsuarioLogado();
            var pacote = _usuarioService.AtualizarUsuario(usuarioDto, usuario);
            if (!EStatusCode.OK.Equals(pacote.StatusCode))
                return Ok(pacote);
            return BadRequest(pacote);
        }

        [HttpPost("SalvarUsuario")]
        [AllowAnonymous]
        public IActionResult SalvarUsuario([FromForm] UsuarioRequisicaoDto usuarioDto)
        {
            var retorno = _usuarioService.CadastrarUsuario(usuarioDto);
            if (EStatusCode.OK.Equals(retorno.StatusCode))
                return Ok(retorno);
            return BadRequest(retorno);
        }
    }
}
