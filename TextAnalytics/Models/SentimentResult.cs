using System.Collections.Generic;
using Newtonsoft.Json;

namespace TextAnalytics.Models
{
    public class SentimentResult
    {
        [JsonProperty(PropertyName = "documents")]
        public IList<DocumentResult> Documents { get; set; }
        [JsonProperty(PropertyName = "errors")]
        public IList<ErrorResult> Errors { get; set; }

    }
}
