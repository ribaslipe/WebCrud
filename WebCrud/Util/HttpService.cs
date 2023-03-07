using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
    public class HttpService
    {

        /// <summary>
        /// Executa a requisição a url solicitada
        /// </summary>
        /// <param name="headers">Cabeçalho Header de requisição em que a página recebeu.</param>
        /// <param name="method">Metodo da requisição (GET, POST, PUT, etc..).</param>        
        /// <param name="url">URL completa da requisição</param>        
        /// <param name="body">Conteúdo a ser enviado no body da requisição [POST ou PUT]</param>
        /// <param name="contentMock">Conteudo texto simulando o retorno</param>
        public async Task<HttpResponseMessage> RunAsync(string token, HttpMethod method, string url, string body, string encodig = "iso-8859-1")
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(method, url);
                if (!String.IsNullOrEmpty(token))
                    request.Headers.Add("Authorization", "Bearer " + token);

                var content = new StringContent(body, null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                return response;
            }
            catch (Exception e)
            {
                throw;
            }
        }


    }
}
