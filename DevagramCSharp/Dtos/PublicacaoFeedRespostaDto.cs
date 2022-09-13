using DevagramCSharp.Models;

namespace DevagramCSharp.Dtos
{
    public class PublicacaoFeedRespostaDto
    {
        public int IdPublicacao { get; set; }
        public string Descricao { get; set; }
        public string UrlFoto { get; set; }
        public List<ComentarioDto> Comentarios { get; set; }
        public List<CurtidaDto> Curtidas { get; set; }
        public UsuarioDto Usuario { get; set; }
    }
}
