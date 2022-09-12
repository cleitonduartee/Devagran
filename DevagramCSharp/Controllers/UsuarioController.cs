using DevagramCSharp.Dtos;
using DevagramCSharp.Enumerators;
using DevagramCSharp.IMapper;
using DevagramCSharp.Mapper;
using DevagramCSharp.Models;
using DevagramCSharp.Repository;
using DevagramCSharp.Services;
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
                return Unauthorized();
            return Ok(_usuarioService.MapearEntidadeParaUsuarioDto(usuario));
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
