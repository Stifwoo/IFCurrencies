using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IFCurrenciesApp.Shared.Services
{
    public class ApiService<T>
    {
        private readonly HttpClient _client;

        public ApiService()
        {
            _client = new HttpClient
            {
                MaxResponseContentBufferSize = 256000
            };
        }

        public async Task<T> GetData(string url)
        {
            var uri = new Uri(string.Format(url, string.Empty));
            var response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content);
            }

            return default(T);
        }
    }
}
