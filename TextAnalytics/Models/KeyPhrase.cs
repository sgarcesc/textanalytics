using System;
using Dapper.Contrib.Extensions;

namespace TextAnalytics.Models
{
    public class KeyPhrase
    {
        [ExplicitKey]
        public string FileName { get; set; }
        public string KeyPhrases { get; set; }
        public DateTime LastUpdated { set; get; }

        public KeyPhrase()
        {
            LastUpdated = DateTime.UtcNow;
        }
    }
}
