using DevagramCSharp.Dtos;
using DevagramCSharp.Models;

namespace DevagramCSharp.IMapper
{
    public interface IUsuarioMapper : IMapper<UsuarioRequisicaoDto, Usuario>
    {
        public UsuarioDto MapearEntidadeParaUsuarioDto(Usuario src);
    }
}
