using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TextAnalytics.Models
{
    public class KeyPhraseDocumentResult
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "keyPhrases")]
        public IList<string> KeyPhrases { get; set; }
    }
}
