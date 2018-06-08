using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TextAnalytics.Models;
using TextAnalytics.Processors;
using TextAnalytics.Repositories;

namespace TextAnalytics.Controllers
{
    [Route("api/[controller]")]
    public class FilesController : Controller
    {
        private readonly IFileProcessor _fileProcessor;
        private readonly IKeyPhraseRepository _phraseRepository;
        public FilesController(IFileProcessor fileProcessor, IKeyPhraseRepository phraseRepository)
        {
            _fileProcessor = fileProcessor ?? throw new ArgumentNullException(nameof(fileProcessor));
            _phraseRepository = phraseRepository ?? throw new ArgumentNullException(nameof(phraseRepository));
        }

        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            return await _phraseRepository.GetFileNames();
        }

        [HttpGet("{fileName}")]
        public async Task<KeyPhrase> GetByFileName([FromRoute] string fileName)
        {
            return await _phraseRepository.GetByFileName(fileName);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload(IFormFile files)
        {
            await _fileProcessor.ProcessFileAsync(files);
            return NoContent();
        }
    }
}
