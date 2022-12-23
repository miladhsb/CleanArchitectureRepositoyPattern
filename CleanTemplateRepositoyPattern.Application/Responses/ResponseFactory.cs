using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTemplateRepositoyPattern.Application.Responses
{
    public static class ResponseFactory
    {

        public static BaseResponse CreateBaseResponseSuccess(string ErrorMessage )
        {
            return new BaseResponse() { Message= new List<ApplicationErrorResponse>() {new ApplicationErrorResponse() { Code="01",Description= ErrorMessage } } ,IsSuccess= true };
        }
       
        public static BaseResponse CreateBaseResponsefailed(string ErrorMessage)
        {
            return new BaseResponse() { Message = new List<ApplicationErrorResponse>() { new ApplicationErrorResponse() { Code = "01", Description = ErrorMessage } }, IsSuccess = false };
        }
        public static BaseResponse CreateBaseResponsefailed(List<ApplicationErrorResponse> ErrorMessages)
        {
            return new BaseResponse() { Message = ErrorMessages, IsSuccess = false };
        }
        public static DataResponse<Tdata> CreateDataResponseSuccess<Tdata>(string ErrorMessage , Tdata Data)
        {
            return new DataResponse<Tdata>() { Message = new List<ApplicationErrorResponse>() { new ApplicationErrorResponse() { Code = "01", Description = ErrorMessage } }, IsSuccess = true,Data= Data };
        }
        public static DataResponse<Tdata> CreateDataResponsefailed<Tdata>(string ErrorMessage, Tdata Data)
        {
            return new DataResponse<Tdata>() { Message = new List<ApplicationErrorResponse>() { new ApplicationErrorResponse() { Code = "01", Description = ErrorMessage } }, IsSuccess = false, Data = Data };
        }
        public static DataResponse<Tdata> CreateDataResponsefailed<Tdata>(List<ApplicationErrorResponse> ErrorMessages, Tdata Data)
        {
            return new DataResponse<Tdata>() { Message = ErrorMessages, IsSuccess = false, Data = Data };
        }
    }
}
