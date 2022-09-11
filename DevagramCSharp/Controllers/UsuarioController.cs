using DevagramCSharp.Dtos;
using DevagramCSharp.Enumerators;
using DevagramCSharp.IMapper;
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
            var usuarioDto = LerToken();
            if (usuarioDto == null)
                return Unauthorized();
            return Ok(usuarioDto);
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
