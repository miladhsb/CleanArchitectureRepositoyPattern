using AutoMapper;
using CleanTemplateRepositoyPattern.Application.Contracts.Persistence.EntityFramework;
using CleanTemplateRepositoyPattern.Application.DTOs.BlogDTos;
using CleanTemplateRepositoyPattern.Application.Exceptions;
using CleanTemplateRepositoyPattern.Application.Responses;
using CleanTemplateRepositoyPattern.Domain.Entities.Blogs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CleanTemplateRepositoyPattern.Application.Services.BlogPostService
{
    public class BlogPostService:IBlogPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<RequestBlugPostDTO> _validator;

        public BlogPostService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<RequestBlugPostDTO> Validator)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            _validator = Validator;
        }

        public async Task<BaseResponse> CreatePostAsunc(RequestBlugPostDTO requestBlugPostDTO)
        {

             var Validate= _validator.Validate(requestBlugPostDTO);
            
            if (!Validate.IsValid)
            {
           
                throw new ValidationModelException(Validate);
            }
           
           
           
           
            var PostModel = _mapper.Map<BlogPost>(requestBlugPostDTO);
            PostModel.PostSlug = requestBlugPostDTO.Title.Replace(" ", "-");
            PostModel.MetaTitle = requestBlugPostDTO.Title;
            var PostResult = await _unitOfWork.BlogPostRepository.AddAsync(PostModel);

            var SaveResult=  await _unitOfWork.SaveChangesAsync();

            if (SaveResult == 0)
            {
                return ResponseFactory.CreateBaseResponsefailed("ذخیره اطلاعات با مشکل مواجه شد");
            }
            return ResponseFactory.CreateBaseResponseSuccess("پست با موفقیت ثبت شد");
        }

    }
}
