using DevagramCSharp.Dtos;
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
            try
            {
                Usuario usuario = new Usuario()
                {
                    Nome = "Cleiton DUarte",
                    Email = "teste@devaria.com.br",
                    Id = 100
                };
                return Ok(usuario);
            }catch(Exception ex)
            {
                _logger.LogError("Ocorreu um erro ao obter usuário: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorRespostaDto()
                {
                    Descricao = "Ocorreu o seguinte erro: "+ ex.Message,
                    Status = StatusCodes.Status500InternalServerError
                });
            }
        }
        [HttpPost("SalvarUsuario")]
        [AllowAnonymous]
        public IActionResult SalvarUsuario([FromBody] UsuarioDto usuarioDto)
        {
            return Ok(_usuarioService.CadastrarUsuario(usuarioDto));
        }
    }
}
