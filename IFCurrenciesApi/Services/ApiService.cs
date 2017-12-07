using System.Net;

namespace IFCurrenciesApi.Services
{
    public class ApiService : IExternalApiService
    {
        public string GetResponse(string url)
        {
            using (var client = new WebClient())
            {
                return client.DownloadString(url);
            }
        }
    }
}