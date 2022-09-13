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
    public class LoginController : BaseController
    {
        public LoginController(IUsuarioService usuarioService) : base(usuarioService)
        {
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult EfetuarLogin([FromBody] LoginRequisicaoDto login)
        {
            var pacote = _usuarioService.EfetuarLogin(login); 
            if (EStatusCode.OK.Equals(pacote.StatusCode))
                return Ok(pacote);
            return BadRequest(pacote);            
        }
    }
}
