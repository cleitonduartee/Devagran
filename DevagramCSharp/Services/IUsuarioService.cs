using DevagramCSharp.Dtos;
using DevagramCSharp.Utils;
using Microsoft.AspNetCore.Mvc;

namespace DevagramCSharp.Services
{
    public interface IUsuarioService
    {
        public Pacote<UsuarioDto> CadastrarUsuario(UsuarioDto usuarioDto);
        public Pacote<LoginRespostaDto> EfetuarLogin(LoginRequisicaoDto login);
    }
}
