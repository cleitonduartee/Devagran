using DevagramCSharp.Dtos;
using DevagramCSharp.Enumerators;
using DevagramCSharp.Services;
using Microsoft.AspNetCore.Mvc;

namespace DevagramCSharp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PublicacaoController : BaseController
    {
        private readonly IPublicacaoService _publicacaoService;
        public PublicacaoController(IUsuarioService usuarioService, IPublicacaoService publicacaoService) : base(usuarioService)
        {
            _publicacaoService = publicacaoService;
        }
        [HttpPost]
        public IActionResult Publicar([FromForm] PublicacaoRequisicaoDto publicaoDto)
        {
            var usuario = ObterUsuarioLogado();
            if (usuario == null)
                return Unauthorized("Por gentileza, fazer login novamente.");

            var pacote = _publicacaoService.Publicar(publicaoDto, usuario.Id);
            if(EStatusCode.OK.Equals(pacote.StatusCode))
                return Ok(pacote);

            return BadRequest(pacote);
        }

        [HttpGet]
        public IActionResult FeedHome()
        {
            var usuario = ObterUsuarioLogado();
            if (usuario == null)
                return Unauthorized("Por gentileza, fazer login novamente.");

            var pacote = _publicacaoService.GetFeedHome(usuario.Id);

            if (EStatusCode.OK.Equals(pacote.StatusCode))
                return Ok(pacote);

            return BadRequest(pacote);            
        }
    }
}
