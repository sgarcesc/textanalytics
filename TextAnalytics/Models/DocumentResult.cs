using Newtonsoft.Json;

namespace TextAnalytics.Models
{
    public class DocumentResult
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "score")]
        public decimal Score { get; set; }
    }
}
