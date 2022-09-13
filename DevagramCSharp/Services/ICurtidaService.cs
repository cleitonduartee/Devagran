using DevagramCSharp.Utils;

namespace DevagramCSharp.Services
{
    public interface ICurtidaService
    {
        public Pacote<string> CurtirOuDescurtir(int idPublicacao, int idUsuario);
    }
}
