using DevagramCSharp.Enumerators;
using DevagramCSharp.Services;
using Microsoft.AspNetCore.Mvc;

namespace DevagramCSharp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurtidaController : BaseController
    {
        private readonly ICurtidaService _service;
        public CurtidaController(IUsuarioService usuarioService, ICurtidaService service) : base(usuarioService)
        {
            _service = service;
        }
        [HttpPut("{publicacaoId:int}")]
        public IActionResult CurtirDescurtir(int publicacaoId)
        {
            var usuario = ObterUsuarioLogado();
            if (usuario == null)
                return Unauthorized("Por gentileza, fazer login novamente.");

            var pacote = _service.CurtirOuDescurtir(publicacaoId, usuario.Id);

            if (EStatusCode.OK.Equals(pacote.StatusCode))
                return Ok(pacote);

            return BadRequest(pacote);
        }
    }
}
