using DevagramCSharp.Dtos;
using DevagramCSharp.Models;
using DevagramCSharp.Utils;

namespace DevagramCSharp.Services
{
    public interface IComentarioService
    {
        public Pacote<string> Comentar(ComentarioRequisicaoDto comentarioDto, int idUsuario);
    }
}
