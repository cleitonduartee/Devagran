using DevagramCSharp.Dtos;
using DevagramCSharp.IMapper;
using DevagramCSharp.Mapper;
using DevagramCSharp.Models;
using DevagramCSharp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DevagramCSharp.Controllers
{
    [Authorize]
    public class BaseController : ControllerBase
    {
        protected readonly IUsuarioService _usuarioService;
        private readonly IUsuarioMapper _usuarioMapper;
        public BaseController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
            _usuarioMapper = new UsuarioMapper();
        }

        protected UsuarioDto? LerToken()
        {
            var idUsuario = User.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).FirstOrDefault();

            if (string.IsNullOrEmpty(idUsuario))
                return null;

            return _usuarioMapper.MapearEntidadeParaDto(_usuarioService.GetUsuarioPorID(int.Parse(idUsuario)));
        }
    }
}
