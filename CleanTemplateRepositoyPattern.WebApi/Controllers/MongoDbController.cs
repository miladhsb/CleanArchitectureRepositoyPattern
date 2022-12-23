using CleanTemplateRepositoyPattern.Application.Contracts.Persistence.MongoDb;
using CleanTemplateRepositoyPattern.Application.Models.MongoDb;
using CleanTemplateRepositoyPattern.Application.Responses;
using CleanTemplateRepositoyPattern.WebApi.Controllers.Common;
using CleanTemplateRepositoyPattern.WebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CleanTemplateRepositoyPattern.WebApi.Controllers
{
    
    public class MongoDbController : BaseController
    {
        private readonly IMongoGenericRepository<AplicationHistory> _mongoGeneric;
        private readonly IMongoBsonDocumentRepository _mongoBsonDocument;

        public MongoDbController(IMongoGenericRepository<AplicationHistory> mongoGeneric, IMongoBsonDocumentRepository mongoBsonDocument )
        {
            this._mongoGeneric = mongoGeneric;
            this._mongoBsonDocument = mongoBsonDocument;
        }

        [HttpPost("AddHistory")]
        public async Task<IActionResult> AddHistory()
        {
            var result = await _mongoGeneric.AddAsync(new AplicationHistory() { CreateTime = DateTime.Now, Creator = "Admin", log = "123456" });
            return Ok(ResponseFactory.CreateDataResponseSuccess("ok",result));
        }

        [HttpGet("GetHistory")]
        public async Task<IActionResult> GetHistory()
        {
            var result = await _mongoGeneric.GetAllAsync();
          
            return Ok(ResponseFactory.CreateDataResponseSuccess("ok",result));
        }

        [HttpPost("AddBsonDocument")]
        public async Task<IActionResult> AddBsonDocument()
        {
            var NewDoc = new AplicationHistory() {Id=Guid.NewGuid().ToString(), CreateTime = DateTime.Now, Creator = "Admin", log = " این یک لاگ تستی است" };
           var Doc= JsonSerializer.Serialize(NewDoc);
            var result = await _mongoBsonDocument.AddOneDocumentAsync( "AppHistory01", Doc);

            return Ok(ResponseFactory.CreateDataResponseSuccess("ok", result));
        }


        [HttpGet("GetBsonDocument")]
        public async Task<IActionResult> GetBsonDocument()
        {
            var result = await _mongoBsonDocument.GetDocumentAllAsync<AplicationHistoryViewModel>("AppHistory01");

            return Ok(ResponseFactory.CreateDataResponseSuccess($"get Count list {result.Count}  ",result));
        }
    }

}
