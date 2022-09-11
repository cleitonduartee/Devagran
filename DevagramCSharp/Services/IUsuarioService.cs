using DevagramCSharp.Dtos;
using DevagramCSharp.Utils;

namespace DevagramCSharp.Services
{
    public interface IUsuarioService
    {
        public Pacote<UsuarioDto> CadastrarUsuario(UsuarioDto usuarioDto);
    }
}
