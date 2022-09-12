using DevagramCSharp.Dtos;
using System.Net;
using System.Net.Http.Headers;

namespace DevagramCSharp.Services.Impl
{
    public class CosmicServiceImpl : ICosmicService
    {
        public string EnviarImagem(ImagemDto imagemDto)
        {
            var urlFotoDefault = "https://cdn3.iconfinder.com/data/icons/toolbar-people/512/user_problem_man_male_person_profile-512.png";
            var url = "https://upload.cosmicjs.com/v2/buckets/devaria-devagran/media";
            var token = "FXJi01f1X9t8d7g7guKaNbEchfAfkJSwfv135tQFUVHptpnS0Z";
            Stream imagem = imagemDto.Imagem.OpenReadStream();
            var nomeImagem = imagemDto.Nome;
            

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var request = new HttpRequestMessage(HttpMethod.Post, "file");
            request.Content = new MultipartFormDataContent
            {
                {new StreamContent(imagem), "media", nomeImagem }
            };

            try
            {
                var retorno = client.PostAsync(url, request.Content).Result;
                if (retorno.StatusCode != HttpStatusCode.OK)
                    return urlFotoDefault;

                var cosmicRespostaDto = retorno.Content.ReadFromJsonAsync<CosmicRespostaDto>().Result;
                return cosmicRespostaDto.media.url;
            }
            catch(Exception e)
            {
                return urlFotoDefault;
            }
        }
    }
}
