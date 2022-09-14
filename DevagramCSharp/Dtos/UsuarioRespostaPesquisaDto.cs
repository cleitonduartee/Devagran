namespace DevagramCSharp.Dtos
{
    public class UsuarioRespostaPesquisaDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string UrlFotoPerfil { get; set; }
        public int QtdPublicacoes { get; set; }
        public int QtdSeguidores { get; set; }
        public int QtdSeguindo { get; set; }
    }
}
