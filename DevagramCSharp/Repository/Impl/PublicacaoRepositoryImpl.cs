using DevagramCSharp.Dtos;
using DevagramCSharp.Models;
using Microsoft.EntityFrameworkCore;

namespace DevagramCSharp.Repository.Impl
{
    public class PublicacaoRepositoryImpl : RepositoryGenericoImpl<Publicacao>, IPublicacaoRepository
    {        
        public PublicacaoRepositoryImpl(DevagramContext devagramContext, ILogger<RepositoryGenericoImpl<Publicacao>> logger) : base(devagramContext, logger)
        {
        }

        public List<PublicacaoFeedRespostaDto> GetFeedHome(int idUsuario)
        {
            var feed = (from publicacao in _contexto.Publicacoes 
                        join seguidores in _contexto.Seguidores on publicacao.IdUsuario equals seguidores.IdUsuarioSeguido 
                        join usu in _contexto.Usuarios on publicacao.IdUsuario equals usu.Id
                        where seguidores.IdUsuarioSeguidor == idUsuario
                        select new PublicacaoFeedRespostaDto
                        {
                            IdPublicacao = publicacao.Id,
                            Descricao = publicacao.Descricao,
                            UrlFoto = publicacao.UrlFotoPublicacao,                         
                            Usuario = new UsuarioDto
                            {
                                Id = usu.Id,
                                Nome = usu.Nome,
                                Email = usu.Email,
                                UrlFotoPerfil = usu.UrlFotoPerfil
                            }
                        }).ToList();

            return feed;
        }

        public List<PublicacaoFeedRespostaDto> GetFeedUsuario(int idUsuario)
        {
            var feedUsuario = (from publicacao in _contexto.Publicacoes
                               join usu in _contexto.Usuarios on publicacao.IdUsuario equals usu.Id
                               where publicacao.IdUsuario == idUsuario
                               select new PublicacaoFeedRespostaDto
                               {
                                   IdPublicacao = publicacao.Id,
                                   Descricao = publicacao.Descricao,
                                   UrlFoto = publicacao.UrlFotoPublicacao,
                                   Usuario = new UsuarioDto
                                   {
                                       Id = usu.Id,
                                       Nome = usu.Nome,
                                       Email = usu.Email,
                                       UrlFotoPerfil = usu.UrlFotoPerfil
                                   }
                               }).ToList();

            return feedUsuario;
        }
    }
}
