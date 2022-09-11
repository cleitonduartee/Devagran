namespace DevagramCSharp.Services.Architecture
{
    public abstract class BaseService
    {
        private readonly List<string> mensagens;

        public bool EhValido { get; set; }

        public BaseService()
        {
            mensagens = new List<string>();
            EhValido = true;
        }

        public void AdicionarMensagem(string msg)
        {
            mensagens.Add(msg);
        }
        public void AdicionarMensagem(List<string> mensagens)
        {
            foreach(var msg in mensagens)
                mensagens.Add(msg);
        }
        public List<string> ObterMensagnes()
        {
            return mensagens;
        }
    }
}
