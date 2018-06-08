using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TextAnalytics.Processors
{
    public interface IFileProcessor
    {
        Task ProcessFileAsync(IFormFile file);
    }
}