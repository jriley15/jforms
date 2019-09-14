using JForms.Data.Dto;
using JForms.Data.Dto.Auth;
using JForms.Data.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JForms.Application.Services
{

    public interface IAuthService
    {

        Task<Response> Login(LoginDto loginDto);

        Task<Response> Register(RegisterDto registerDto);

        string GetCurrentUserId();

        Task<ApplicationUser> GetCurrentUser();
    }


    public class AuthService : IAuthService
    {

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _contextAccessor;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _contextAccessor = contextAccessor;
        }


        public async Task<Response> Login(LoginDto loginDto)
        {
            var invalidResponse = new Response();
            var result = await _signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, false, false);

            if (result.Succeeded)
            {
                var appUser = _userManager.Users.SingleOrDefault(r => r.Email == loginDto.Email && r.UserName == loginDto.Email);
                invalidResponse = new DataResponse<object>() { Data = GenerateJwtToken(loginDto.Email, appUser), Success = true };
            }
            else
            {
                invalidResponse.AddError("*", "Invalid username or password");
            }

            return invalidResponse;
        }

        public async Task<Response> Register(RegisterDto registerDto)
        {
            var response = new Response();

            if (!registerDto.Password.Equals(registerDto.ConfirmPassword))
            {
                response.AddError("password", "Passwords must match");
                return response;
            }

            var user = new ApplicationUser
            {
                //hack for now - force users to activate email before logging in?
                UserName = registerDto.Email,
                Email = registerDto.Email
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                response = new DataResponse<object>() { Data = GenerateJwtToken(registerDto.Email, user), Success = true };
            }
            else
            {
                foreach (IdentityError Error in result.Errors)
                {
                    if (Error.Code == "DuplicateUserName")
                    {
                        response.AddError("email", "Email already in use");
                    }
                    else
                    {
                        response.AddError("*", Error.Description);
                    }

                }
            }

            return response;
        }

        private object GenerateJwtToken(string email, IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                null,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //anything calling this must first use the [authorize] middleware in the request
        public string GetCurrentUserId()
        {
            return _contextAccessor.HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        }

        public async Task<ApplicationUser> GetCurrentUser()
        {
            return await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
        }
    }
}
