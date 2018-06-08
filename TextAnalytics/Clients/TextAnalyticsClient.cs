using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TextAnalytics.Models;

namespace TextAnalytics.Clients
{
    public class TextAnalyticsClient : ITextAnalyticsClient
    {
        private HttpClient _client;
        private readonly IConfiguration _configuration;
        private HttpClient Client => _client ?? (_client = GetClient());

        private HttpClient GetClient()
        {
            var client = new HttpClient();
            var apiKey = _configuration["TextAnalyticsApiKey"];
            var baseUrl = _configuration["TextAnalyticsEndPoint"];
            
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", apiKey);

            return client;
        }

        public TextAnalyticsClient(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<SentimentResult> SentimentAsync(DocumentsInput documentsInput)
        {
            
            var response = await Client.PostAsJsonAsync("sentiment", documentsInput);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<SentimentResult>();
            }

            return new SentimentResult();
        }

        public async Task<KeyPhrasesResult> KeyPhrasesAsync(DocumentsInput documentsInput)
        {
            var response = await Client.PostAsJsonAsync("keyPhrases", documentsInput);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<KeyPhrasesResult>();
            }
            return new KeyPhrasesResult();
        }
    }
}
