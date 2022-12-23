using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTemplateRepositoyPattern.Application.DTOs.BlogDTos.Validator
{
    public class RequestBlugPostDTOValidator : AbstractValidator<RequestBlugPostDTO>
    {
        public RequestBlugPostDTOValidator()
        {

            //RuleFor(p => p.Title)
            //    .NotEmpty().WithMessage("{PropertyName} نمیتواند خالی باشد")
            //    .NotNull().WithMessage("{PropertyName} نمیتواند خالی باشد")
            //    .Length(5, 60).WithMessage(m => "{PropertyName} باید بین {MinLength} و {MaxLength} باشد");
            
            RuleFor(p => p.Title)
               .NotEmpty()
               .NotNull()
               .Length(5, 60);

            RuleFor(p => p.Body)
               .NotEmpty()
               .NotNull()
               .MinimumLength(5);

            RuleFor(p => p.MetaDescription)
          .NotEmpty()
          .NotNull()
          .Length(5, 150);

            RuleFor(p => p.MetaKeywords)
         .NotEmpty()
         .NotNull()
         .Length(5, 150);

        }
    }
}
