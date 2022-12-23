using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTemplateRepositoyPattern.Application.Contracts.Persistence.MongoDb
{
    public interface IMongoBsonDocumentRepository
    {
        Task<bool> AddOneDocumentAsync(string CollectionName, string JsonDocument);
        Task<List<TEntity>> GetDocumentAllAsync<TEntity>(string CollectionName);
    }
}
