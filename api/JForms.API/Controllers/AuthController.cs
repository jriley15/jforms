using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JForms.Application.Services;
using JForms.Data.Dto.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using JForms.API.Extensions;

namespace JForms.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            return this.GenerateResponse(await _authService.Login(loginDto));
        }

        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            return this.GenerateResponse(await _authService.Register(registerDto));
        }


        public async Task<IActionResult> GithubLogin()
        {
            return null;
        }

    }
}