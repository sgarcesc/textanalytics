using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;
using TextAnalytics.Models;

namespace TextAnalytics.Repositories
{
    public class KeyPhraseRepository : IKeyPhraseRepository
    {
        private readonly IConfiguration _configuration;
        private IDbConnection _connection;

        public KeyPhraseRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        private IDbConnection Connection => _connection ?? (_connection = GetConnecton());

        private IDbConnection GetConnecton()
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            return new SqlConnection(connectionString);
        }

        public async Task<bool> AddOrUpdateAsync(KeyPhrase keyPhrase)
        {
            var result = await Connection.GetAsync<KeyPhrase>(keyPhrase.FileName);
            if (result != null) return await Connection.UpdateAsync(keyPhrase);
            return await Connection.InsertAsync(keyPhrase) >= 0;
        }

        public async Task<IEnumerable<string>> GetFileNames()
        {
            return (await Connection.GetAllAsync<KeyPhrase>()).Select(c => c.FileName);
        }

        public async Task<KeyPhrase> GetByFileName(string fileName)
        {
            return await Connection.GetAsync<KeyPhrase>(fileName);
        }
    }
}