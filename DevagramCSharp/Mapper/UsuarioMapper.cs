using DevagramCSharp.Dtos;
using DevagramCSharp.IMapper;
using DevagramCSharp.Models;

namespace DevagramCSharp.Mapper
{
    public class UsuarioMapper : IUsuarioMapper
    {
        public Usuario MapearDtoParaEntidade(UsuarioRequisicaoDto src)
        {
            return new Usuario()
            {
                Nome = src.Nome,
                Email = src.Email,
                Senha = src.Senha,
            };
        }

        public UsuarioDto MapearEntidadeParaUsuarioDto(Usuario src)
        {
            return new UsuarioDto()
            {
                Nome = src.Nome,
                Email = src.Email,
                UrlFotoPerfil = src.UrlFotoPerfil
            };
        }

        public UsuarioRequisicaoDto MapearEntidadeParaDto(Usuario src)
        {
            throw new NotImplementedException();
        }
    }
}
