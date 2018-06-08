using Newtonsoft.Json;

namespace TextAnalytics.Models
{
    public class ErrorResult
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
    }
}
