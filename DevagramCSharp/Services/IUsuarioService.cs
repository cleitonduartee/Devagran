using DevagramCSharp.Dtos;
using DevagramCSharp.Models;
using DevagramCSharp.Utils;
using Microsoft.AspNetCore.Mvc;

namespace DevagramCSharp.Services
{
    public interface IUsuarioService
    {
        public Pacote<string> CadastrarUsuario(UsuarioRequisicaoDto usuarioDto);
        public Pacote<string> AtualizarUsuario(UsuarioRequisicaoDto usuarioDto, Usuario usuarioDB);
        public Pacote<LoginRespostaDto> EfetuarLogin(LoginRequisicaoDto login);
        public Usuario GetUsuarioPorID(int id);
        public List<Usuario> GetUsuarioPorNome(string nome);
        public Pacote<UsuarioDto> MapearEntidadeParaUsuarioDto(Usuario usuario);
        void ConfigUsuarioPesquisa(ref UsuarioRespostaPesquisaDto usuarioRespostaPesquisaDto);
    }
}
