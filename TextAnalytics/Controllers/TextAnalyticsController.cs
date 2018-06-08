using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TextAnalytics.Clients;
using TextAnalytics.Models;

namespace TextAnalytics.Controllers
{
    [Route("api/[controller]")]
    public class TextAnalyticsController : Controller
    {
        private readonly ITextAnalyticsClient _analyticsClient;
        public TextAnalyticsController(ITextAnalyticsClient analyticsClient)
        {
            _analyticsClient = analyticsClient ?? throw new ArgumentNullException(nameof(analyticsClient));
        }

        [HttpPost("[action]")]
        public async Task<IList<DocumentResult>> Sentiment([FromBody] IList<DocumentInput> documents)
        {
            var result = await _analyticsClient.SentimentAsync(new DocumentsInput{Documents = documents});
            return result.Documents;
        }
    }
}
