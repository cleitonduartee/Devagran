using DevagramCSharp.Dtos;
using DevagramCSharp.IMapper;
using DevagramCSharp.Models;

namespace DevagramCSharp.Mapper
{
    public class UsuarioMapper : IUsuarioMapper
    {
        public Usuario MapearDtoParaEntidade(UsuarioDto src)
        {
            return new Usuario()
            {
                Nome = src.Nome,
                Email = src.Email,
                Senha = src.Senha,
            };
        }

        public UsuarioDto MapearEntidadeParaDto(Usuario src)
        {
            return new UsuarioDto()
            {
                Nome = src.Nome,
                Email = src.Email,
                Senha = "****",
            };
        }
    }
}
