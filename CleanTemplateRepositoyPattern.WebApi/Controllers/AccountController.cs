using CleanTemplateRepositoyPattern.Application.Contracts.Identity;
using CleanTemplateRepositoyPattern.Application.Models.Identity;
using CleanTemplateRepositoyPattern.Application.Responses;
using CleanTemplateRepositoyPattern.WebApi.Controllers.Common;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CleanTemplateRepositoyPattern.WebApi.Controllers
{

    public class AccountController : BaseController
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            this._authService = authService;
        }
        // GET: api/<AccountController>
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]LoginRequest loginRequest)
        {

            return Ok(await _authService.LoginAsync(loginRequest));
        }

       
    }
}
