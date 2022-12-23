using CleanTemplateRepositoyPattern.Application.Responses;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace CleanTemplateRepositoyPattern.Application.Exceptions
{
    public class ValidationModelException : ApplicationException
    {
        public List<ApplicationErrorResponse> Errors { get; set; } =new List<ApplicationErrorResponse>();
        
        public ValidationModelException(ValidationResult validationResult)
        {
         
            foreach (var error in validationResult.Errors)
            {
                Errors.Add(new ApplicationErrorResponse() { Code= error .ErrorCode,Description=error.ErrorMessage});
            }
        }
    }
}
