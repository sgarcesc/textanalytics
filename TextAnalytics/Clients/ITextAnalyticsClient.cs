using System.Threading.Tasks;
using TextAnalytics.Models;

namespace TextAnalytics.Clients
{
    public interface ITextAnalyticsClient
    {
        Task<SentimentResult> SentimentAsync(DocumentsInput documentsInput);
        Task<KeyPhrasesResult> KeyPhrasesAsync(DocumentsInput documentsInput);
    }
}
