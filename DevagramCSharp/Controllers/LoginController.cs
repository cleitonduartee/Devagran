using DevagramCSharp.Dtos;
using DevagramCSharp.Models;
using DevagramCSharp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevagramCSharp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult EfetuarLogin([FromBody] LoginRequisicaoDto login)
        {
            try
            {
                if(!String.IsNullOrEmpty(login.Email) && !String.IsNullOrEmpty(login.Senha))
                {
                    string email = "cleiton@devaria.com.br";
                    string senha = "Senha@123";

                    if(email.Equals(login.Email) && senha.Equals(login.Senha))
                    {
                        Usuario usuario = new Usuario()
                        {
                            Email = login.Email,
                            Id = 12,
                            Nome = "Cleiton Durte"
                        };
                        return Ok(new LoginRespostaDto()
                        {
                            Email = usuario.Email,
                            Nome = usuario.Nome,
                            Token = TokenService.CriarToken(usuario)
                        });
                    }
                    return BadRequest(new ErrorRespostaDto()
                    {
                        Descricao = "Email ou Senha inválido",
                        Status = StatusCodes.Status400BadRequest
                    });
                }
                else
                {
                    return BadRequest(new ErrorRespostaDto()
                    {
                        Descricao = "Usuário não preencheu os campo de login corretamente.",
                        Status = StatusCodes.Status400BadRequest
                    });
                }
                
            }catch(Exception ex)
            {
                _logger.LogError("Ocorreu um erro no login: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorRespostaDto()
                {
                    Descricao = "Ocorreu um erro ao fazer o login",
                    Status = StatusCodes.Status500InternalServerError
                });
            }
        }
    }
}
