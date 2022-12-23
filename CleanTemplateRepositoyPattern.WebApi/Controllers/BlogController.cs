using CleanTemplateRepositoyPattern.Application.Contracts.Persistence.MongoDb;
using CleanTemplateRepositoyPattern.Application.DTOs.BlogDTos;
using CleanTemplateRepositoyPattern.Application.Models.MongoDb;
using CleanTemplateRepositoyPattern.Application.Services.BlogPostService;
using CleanTemplateRepositoyPattern.WebApi.Controllers.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanTemplateRepositoyPattern.WebApi.Controllers
{
  
    public class BlogController : BaseController
    {
        private readonly IBlogPostService _blogPostService;
       

        public BlogController(IBlogPostService blogPostService)
        {
            this._blogPostService = blogPostService;
            
        }
        // GET: BlogController
        [HttpPost("CreatePost")]
        public async Task<IActionResult> CreatePost(RequestBlugPostDTO  blugPostDTO)
        {
           
            return Ok(await _blogPostService.CreatePostAsunc(blugPostDTO));
        }
       
    }
}
