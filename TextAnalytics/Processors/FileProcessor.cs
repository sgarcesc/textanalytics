using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Internal;
using TextAnalytics.Clients;
using TextAnalytics.Extensions;
using TextAnalytics.Models;
using TextAnalytics.Repositories;

namespace TextAnalytics.Processors
{
    public class FileProcessor : IFileProcessor
    {
        private readonly ITextAnalyticsClient _analyticsClient;
        private readonly IKeyPhraseRepository _keyPhraseRepository;
        private readonly IHubContext<Hubs.File> _hubContext;
        public FileProcessor(ITextAnalyticsClient analyticsClient, IKeyPhraseRepository keyPhraseRepository, IHubContext<Hubs.File> hubContext)
        {
            _analyticsClient = analyticsClient ?? throw new ArgumentNullException(nameof(analyticsClient));
            _keyPhraseRepository = keyPhraseRepository ?? throw new ArgumentNullException(nameof(keyPhraseRepository));
            _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
        }

        public async Task ProcessFileAsync(IFormFile file)
        {
            var saveTask = SaveFileAsync(file);
            var keyPhrasesTask = GetKeyPhrasesAsync(file);
            await Task.WhenAll(saveTask, keyPhrasesTask);
            var saved = await SaveOrUpdateKeyPhrases(file, keyPhrasesTask.Result);
            if (saved)
            {
                await _hubContext.Clients.All.SendAsync("ReceiveFile", file.FileName);
            }
        }

        private async Task SaveFileAsync(IFormFile file)
        {
            // full path to file in temp location
            var filePath = $"{Path.GetTempPath()}{file.FileName}";
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
        }

        private async Task<KeyPhrasesResult> GetKeyPhrasesAsync(IFormFile file)
        {
            var content = await file.ReadAsStringAsync();
            return await _analyticsClient.KeyPhrasesAsync(new DocumentsInput
            {
                Documents = new List<DocumentInput>
                {
                    new DocumentInput { Id = "1", Language = "en", Text = content }
                }
            });
        }

        private async Task<bool> SaveOrUpdateKeyPhrases(IFormFile file, KeyPhrasesResult result)
        {
            if (!result.Documents.Any() || !result.Documents[0].KeyPhrases.Any()) return false;
            return await _keyPhraseRepository.AddOrUpdateAsync(new KeyPhrase
            {
                FileName = file.FileName,
                KeyPhrases = result.Documents[0].KeyPhrases.ToCommaSeparatedString()
            });
        }
    }
}
