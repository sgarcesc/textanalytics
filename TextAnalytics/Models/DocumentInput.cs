using Newtonsoft.Json;

namespace TextAnalytics.Models
{
    public class DocumentInput
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "language")]
        public string Language { get; set; }
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

    }
}
