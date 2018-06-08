using System.Collections.Generic;
using Newtonsoft.Json;

namespace TextAnalytics.Models
{
    public class KeyPhrasesResult
    {
        [JsonProperty(PropertyName = "documents")]
        public IList<KeyPhraseDocumentResult> Documents { get; set; }
        [JsonProperty(PropertyName = "errors")]
        public IList<ErrorResult> Errors { get; set; }
    }
}
