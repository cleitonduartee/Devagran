using DevagramCSharp.Dtos;
using DevagramCSharp.Models;
using DevagramCSharp.Utils;
using Microsoft.AspNetCore.Mvc;

namespace DevagramCSharp.Services
{
    public interface IUsuarioService
    {
        public Pacote<UsuarioDto> CadastrarUsuario(UsuarioRequisicaoDto usuarioDto);
        public Pacote<UsuarioDto> AtualizarUsuario(UsuarioRequisicaoDto usuarioDto, Usuario usuarioDB);
        public Pacote<LoginRespostaDto> EfetuarLogin(LoginRequisicaoDto login);
        public Usuario GetUsuarioPorID(int id);
        public Pacote<UsuarioDto> MapearEntidadeParaUsuarioDto(Usuario usuario);
    }
}
