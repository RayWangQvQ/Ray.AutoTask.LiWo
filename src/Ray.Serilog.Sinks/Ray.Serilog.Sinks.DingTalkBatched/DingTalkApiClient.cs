using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Ray.Infrastructure.Extensions;
using Ray.Serilog.Sinks.Batched;

namespace Ray.Serilog.Sinks.DingTalkBatched
{
    public class DingTalkApiClient : IPushService
    {
        private readonly string _title;
        private readonly Uri _apiUrl;
        private readonly HttpClient _httpClient = new HttpClient();

        public DingTalkApiClient(string webHookUrl, string title = "")
        {
            _title = title;
            _apiUrl = new Uri(webHookUrl);
        }

        public async Task<HttpResponseMessage> PushMessageAsync(string message)
        {
            var json = new { msgtype = "markdown", markdown = new { title = _title, text = message } }.ToJson();
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_apiUrl, content);
            return response;
        }
    }
}
