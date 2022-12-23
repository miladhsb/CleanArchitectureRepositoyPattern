using CleanTemplateRepositoyPattern.Application.DTOs.BlogDTos;
using CleanTemplateRepositoyPattern.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTemplateRepositoyPattern.Application.Services.BlogPostService
{
    public interface IBlogPostService
    {
        Task<BaseResponse> CreatePostAsunc(RequestBlugPostDTO requestBlugPostDTO);
    }
}
