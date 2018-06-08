using System.Collections.Generic;
using System.Threading.Tasks;
using TextAnalytics.Models;

namespace TextAnalytics.Repositories
{
    public interface IKeyPhraseRepository
    {
        Task<bool> AddOrUpdateAsync(KeyPhrase keyPhrase);
        Task<IEnumerable<string>> GetFileNames();
        Task<KeyPhrase> GetByFileName(string fileName);
    }
}