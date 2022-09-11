using DevagramCSharp.Enumerators;
using System.Reflection.Metadata.Ecma335;

namespace DevagramCSharp.Utils
{
    public class Pacote<TResult>
    {
        public EStatusCode StatusCode { get; }

        public TResult Data { get; }

        public ICollection<string> Errors { get; } = new List<string>();

        protected Pacote(EStatusCode statusCode, TResult data)
        {
            StatusCode = statusCode;
            Data = data;            
        }
        protected Pacote(EStatusCode statusCode, params string[] erros)
        {
            StatusCode = statusCode;
            Errors = new List<string>();

            foreach(var erro in erros)
            {
                Errors.Add(erro);
            }
        }
        protected Pacote(EStatusCode statusCode, IEnumerable<string> erros)
        {
            StatusCode = statusCode;
            Errors = new List<string>();

            foreach (var erro in erros)
            {
                Errors.Add(erro);
            }
        }

        public static Pacote<TResult> Sucess(TResult data)
        {
            return new Pacote<TResult>(EStatusCode.OK, data);
        }
        public static Pacote<TResult> Error(EStatusCode statusCode, string msgErro)
        {
            return new Pacote<TResult>(statusCode, msgErro);
        }
        public static Pacote<TResult> Error(EStatusCode statusCode, IEnumerable<string> msgErro)
        {
            return new Pacote<TResult>(statusCode, msgErro);
        }
    }
}
