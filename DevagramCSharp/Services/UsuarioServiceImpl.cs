using DevagramCSharp.Models;
using DevagramCSharp.Repository;
using DevagramCSharp.Services.Architecture;

namespace DevagramCSharp.Services
{
    public class UsuarioService : BaseService, IEstadoValido
    {
        private readonly IUsuarioRepository _repository;
        public Usuario usuario { get; set; }

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public bool Salvar()
        {
            usuario.Senha = Utils.MD5Utils.GerarHashMD5(usuario.Senha);
            return _repository.Salvar(usuario);
        }

        public bool EstadoValido()
        {
            if (usuario == null)
            {
                AdicionarMensagem("Informe todos os campos do usuário");
                EhValido = false;
            }
            if (string.IsNullOrEmpty(usuario.Nome) || string.IsNullOrWhiteSpace(usuario.Nome))
            {
                AdicionarMensagem("Nome do usuário inválido.");
                EhValido = false;
            }
            if (string.IsNullOrEmpty(usuario.Senha) || string.IsNullOrWhiteSpace(usuario.Senha))
            {
                AdicionarMensagem("Senha não informada.");
                EhValido = false;
            }
            if (string.IsNullOrWhiteSpace(usuario.Email) || !usuario.Email.Contains("@"))
            {
                AdicionarMensagem("E-mail inválido");
                EhValido = false;
            }
            if (_repository.JaTemEsseEmail(usuario.Email))
            {
                AdicionarMensagem("E-mail já está sendo usado.");
                EhValido = false;
            }
            return true;

        }
    }
}
