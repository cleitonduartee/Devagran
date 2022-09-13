using DevagramCSharp.Dtos;
using DevagramCSharp.Enumerators;
using DevagramCSharp.Services;
using Microsoft.AspNetCore.Mvc;

namespace DevagramCSharp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComentarioController : BaseController
    {
        private readonly IComentarioService _service;
        public ComentarioController(IUsuarioService usuarioService, IComentarioService service) : base(usuarioService)
        {
            _service = service;
        }

        [HttpPut]
        public IActionResult Comentar([FromBody] ComentarioRequisicaoDto comentarioDto)
        {
            var usuario = ObterUsuarioLogado();
            if(usuario == null)
                return Unauthorized("Por gentileza, fazer login novamente.");

            var pacote = _service.Comentar(comentarioDto, usuario.Id);
            if(EStatusCode.OK.Equals(pacote.StatusCode))
                return Ok(pacote);

            return BadRequest(pacote);
        }
    }
}
