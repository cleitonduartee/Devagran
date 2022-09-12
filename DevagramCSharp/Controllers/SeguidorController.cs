using DevagramCSharp.Enumerators;
using DevagramCSharp.Repository;
using DevagramCSharp.Services;
using Microsoft.AspNetCore.Mvc;

namespace DevagramCSharp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeguidorController : BaseController
    {
        private readonly ISeguidorService _service;
        public SeguidorController(ISeguidorService service, IUsuarioService usuarioService) : base(usuarioService)
        {
            _service = service; 
        }

        [HttpPut()]
        public IActionResult Seguir(int idUsuarioSeguido)
        {
            var idUsuarioSeguidor = ObterUsuarioLogado().Id;
            var pacote =  _service.AtualizaSeguidor(idUsuarioSeguidor, idUsuarioSeguido);

            if(EStatusCode.OK.Equals(pacote.StatusCode))
                return Ok(pacote);

            return BadRequest(pacote);
        }
    }
}
