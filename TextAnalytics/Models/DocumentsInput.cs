using System.Collections.Generic;
using Newtonsoft.Json;

namespace TextAnalytics.Models
{
    public class DocumentsInput
    {
        [JsonProperty(PropertyName = "documents")]
        public IList<DocumentInput> Documents { get; set; }
    }
}
