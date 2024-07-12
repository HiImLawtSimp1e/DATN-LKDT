using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Shop.WebAdmin.Controllers
{
    public class BaseMvcControler:Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<BaseMvcControler> _logger;
        protected BaseMvcControler(IHttpClientFactory httpClientFactory, ILogger<BaseMvcControler> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger=logger;
        }

        protected async void CallApiAsync<TResult>(string url, string namedClient = null)
        {
            using (var httpClient = string.IsNullOrEmpty(namedClient)
                ? _httpClientFactory.CreateClient()
                : _httpClientFactory.CreateClient(namedClient))
            {
                var response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var respone = await response.Content.ReadAsStringAsync();
            }
        }

        protected async Task<HttpResponseMessage> CallApiAsync(string url, string namedClient = null)
        {
            using (var httpClient = string.IsNullOrEmpty(namedClient)
                ? _httpClientFactory.CreateClient()
                : _httpClientFactory.CreateClient(namedClient))
            {
                return await httpClient.GetAsync(url);
            }
        }
    }
}
