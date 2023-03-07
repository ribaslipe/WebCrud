using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Util.IService;
using Util.Models;

namespace Util.Service
{
    public class ConsultaAPI : IConsultaAPI
    {
        private readonly HttpService _httpService;
        private readonly Memory _memory;
        private readonly IConfiguration _configuration;

        public ConsultaAPI(HttpService httpService, IConfiguration configuration, Memory memory)
        {
            _httpService = httpService;
            _configuration = configuration;
            _memory = memory;
        }

        public async Task<TokenModel?> GetLogin(LoginModel login)
        {
            var url = $"{_configuration.GetSection("UrlApi").Value}/v1/Auth/CreateToken";
            var result = await _httpService.RunAsync("", HttpMethod.Post, url, JsonConvert.SerializeObject(login));
            var content = result.Content.ReadAsStringAsync().Result;
            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return new TokenModel { Success = false };
            }
            else if (!String.IsNullOrEmpty(content))
            {
                TokenModel? tokenModel = JsonConvert.DeserializeObject<TokenModel>(content);
                return tokenModel;
            }

            return new TokenModel { Success = false };
        }

        public async Task<ClientsModel?> GetClients()
        {
            var token = _memory.GetMemoryItem<string>("Token");
            var url = $"{_configuration.GetSection("UrlApi").Value}/v1/Client/GetClients";
            var result = await _httpService.RunAsync(token, HttpMethod.Post, url, "{\r\n    \r\n}");
            var content = result.Content.ReadAsStringAsync().Result;
            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return new ClientsModel();
            }
            else if (!String.IsNullOrEmpty(content))
            {
                ClientsModel? tokenModel = JsonConvert.DeserializeObject<ClientsModel>(content);
                return tokenModel;
            }

            return new ClientsModel();
        }

        public async Task<bool> UpdateClients(Clients clients)
        {
            var token = _memory.GetMemoryItem<string>("Token");
            var url = $"{_configuration.GetSection("UrlApi").Value}/v1/Client/UpdateClient";
            var result = await _httpService.RunAsync(token, HttpMethod.Put, url, JsonConvert.SerializeObject(clients));
            var content = result.Content.ReadAsStringAsync().Result;
            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return false;
            }
            else if (!String.IsNullOrEmpty(content))
            {
                return true;
            }
            return false;
        }

        public async Task<bool> CreateClients(Clients clients)
        {
            var token = _memory.GetMemoryItem<string>("Token");
            var url = $"{_configuration.GetSection("UrlApi").Value}/v1/Client/CreateClient";
            var result = await _httpService.RunAsync(token, HttpMethod.Post, url, JsonConvert.SerializeObject(clients));
            var content = result.Content.ReadAsStringAsync().Result;
            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return false;
            }
            else if (!String.IsNullOrEmpty(content))
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteClients(int codcliente)
        {
            var token = _memory.GetMemoryItem<string>("Token");
            var url = $"{_configuration.GetSection("UrlApi").Value}/v1/Client/DeleteClient";
            var result = await _httpService.RunAsync(token, HttpMethod.Delete, url, "{\r\n    \"codcliente\": " + codcliente + "\r\n}");
            var content = result.Content.ReadAsStringAsync().Result;
            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return false;
            }
            else if (!String.IsNullOrEmpty(content))
            {
                return true;
            }
            return false;
        }

    }
}
