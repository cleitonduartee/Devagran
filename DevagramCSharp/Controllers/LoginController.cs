using DevagramCSharp.Dtos;
using DevagramCSharp.Enumerators;
using DevagramCSharp.Models;
using DevagramCSharp.Services;
using DevagramCSharp.Services.Impl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevagramCSharp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IUsuarioService _usuarioService;

        public LoginController(ILogger<LoginController> logger, IUsuarioService usuarioService)
        {
            _logger = logger;
            _usuarioService = usuarioService;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult EfetuarLogin([FromBody] LoginRequisicaoDto login)
        {
            var retorno = _usuarioService.EfetuarLogin(login); 
            if (EStatusCode.OK.Equals(retorno.StatusCode))
                return Ok(retorno);
            return BadRequest(retorno);            
        }
    }
}
