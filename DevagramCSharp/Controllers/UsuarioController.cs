using DevagramCSharp.Dtos;
using DevagramCSharp.Enumerators;
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
        public readonly ILogger<UsuarioController> _logger;
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(ILogger<UsuarioController> logger, IUsuarioService iusuarioService)
        {
            _logger = logger;
            _usuarioService = iusuarioService;
        }

        [HttpGet]
        public IActionResult ObterUsuario()
        {
            Usuario usuario = new Usuario()
            {
                Nome = "Cleiton DUarte",
                Email = "teste@devaria.com.br",
                Id = 100
            };
            return Ok(usuario);
        }
        [HttpPost("SalvarUsuario")]
        [AllowAnonymous]
        public IActionResult SalvarUsuario([FromBody] UsuarioDto usuarioDto)
        {
            var retorno = _usuarioService.CadastrarUsuario(usuarioDto);
            if (EStatusCode.OK.Equals(retorno.StatusCode))
                return Ok(retorno);
            return BadRequest(retorno);
        }
    }
}
