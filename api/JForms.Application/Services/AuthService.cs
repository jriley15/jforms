using Google.Apis.Auth;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Web;
using JForms.Data.Dto;
using JForms.Data.Dto.Auth;
using JForms.Data.Dto.Gtihub;
using JForms.Data.Entity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JForms.Application.Services
{

    public interface IAuthService
    {

        Task<Response> Login(LoginDto loginDto);

        Task<Response> GithubLogin(string code);

        Task<Response> GoogleLogin(string code);

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
        private readonly IHttpClientFactory _clientFactory;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration, IHttpContextAccessor contextAccessor, IHttpClientFactory clientFactory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _contextAccessor = contextAccessor;
            _clientFactory = clientFactory;
        }


        public async Task<Response> Login(LoginDto loginDto)
        {
            var response = new Response();
            var result = await _signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, false, false);

            if (result.Succeeded)
            {
                var appUser = _userManager.Users.SingleOrDefault(r => r.Email == loginDto.Email && r.UserName == loginDto.Email);
                response = new DataResponse<object>() { Data = GenerateJwtToken(appUser), Success = true };
            }
            else
            {
                response.AddError("*", "Invalid username or password");
            }

            return response;
        }


        public async Task<Response> GoogleLogin(string code)
        {
            var response = new Response();
            try
            {
                var flow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer()
                {
                    ClientSecrets = new ClientSecrets()
                    {
                        ClientId = _configuration["GoogleClientId"],
                        ClientSecret = _configuration["GoogleClientSecret"]
                    },
                    Scopes = new[] { "email", "profile" },

                });

                var token = await flow.ExchangeCodeForTokenAsync("user", code, _configuration["GoogleRedirectUri"], CancellationToken.None);
                var payload = (await GoogleJsonWebSignature.ValidateAsync(token.IdToken, new GoogleJsonWebSignature.ValidationSettings()));
                var appUser = await _userManager.FindByNameAsync(payload.Email);

                if (appUser != null)
                {
                    appUser.UserName = payload.Email;
                    appUser.AvatarUrl = payload.Picture;
                    await _signInManager.SignInAsync(appUser, false);
                    response = new DataResponse<object>() { Data = GenerateJwtToken(appUser), Success = true };
                }
                else
                {
                    //new user
                    var user = new ApplicationUser
                    {
                        //hack for now - force users to activate email before logging in?
                        UserName = payload.Email,
                        Email = payload.Email,
                        AvatarUrl = payload.Picture
                    };

                    var identityResult = await _userManager.CreateAsync(user);

                    if (identityResult.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, false);
                        response = new DataResponse<object>() { Data = GenerateJwtToken(user), Success = true };
                    }
                    else
                    {
                        foreach (IdentityError Error in identityResult.Errors)
                        {
                            response.AddError("*", Error.Description);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                response.AddError("*", "Error authenticating with Google");
            }
            return response;
        }

        public async Task<Response> GithubLogin(string code)
        {

            var response = new Response();
            var client = _clientFactory.CreateClient();

            var parameters = new
            {
                client_id = _configuration["GithubClientId"],
                client_secret = _configuration["GithubClientSecret"],
                code = code,
                redirect_uri = _configuration["GithubRedirectUri"],
                state = ""
            };
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            var oAuthResponse = await client.PostAsync("https://github.com/login/oauth/access_token",
                new StringContent(JsonConvert.SerializeObject(parameters), Encoding.UTF8, "application/json"));

            var result = JsonConvert.DeserializeObject<GithubToken>(await oAuthResponse.Content.ReadAsStringAsync());


            client.DefaultRequestHeaders.Remove("Accept");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + result.access_token);
            client.DefaultRequestHeaders.Add("User-Agent", "Asp net core");
            var githubUser = JsonConvert.DeserializeObject<GithubUser>(await client.GetStringAsync("https://api.github.com/user"));

            var appUser = await _userManager.FindByNameAsync(githubUser.login);

            if (appUser != null)
            {
                appUser.AvatarUrl = githubUser.avatar_url;
                appUser.UserName = githubUser.login;
                //success
                await _signInManager.SignInAsync(appUser, false);
                /*await _signInManager.UpdateExternalAuthenticationTokensAsync(new ExternalLoginInfo()
                {
                    LoginProvider = "Github",
                    ProviderKey = githubUser.id,
                    AuthenticationTokens = new List<AuthenticationToken>() { new AuthenticationToken() { Name = "Bearer", Value = result.access_token } },
                    ProviderDisplayName = githubUser.login,
                    Principal = new ClaimsPrincipal()

                });*/
                response = new DataResponse<object>() { Data = GenerateJwtToken(appUser), Success = true };
            }
            else
            {
                //new user
                var user = new ApplicationUser
                {
                    //hack for now - force users to activate email before logging in?
                    UserName = githubUser.login,
                    Email = githubUser.email,
                    AvatarUrl = githubUser.avatar_url
                };

                var identityResult = await _userManager.CreateAsync(user);

                if (identityResult.Succeeded)
                {
                    await _signInManager.SignInAsync(appUser, false);
                    response = new DataResponse<object>() { Data = GenerateJwtToken(user), Success = true };
                }
                else
                {
                    foreach (IdentityError Error in identityResult.Errors)
                    {
                        response.AddError("*", Error.Description);
                    }
                }
            }

            return response;
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
                response = new DataResponse<object>() { Data = GenerateJwtToken(user), Success = true };
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

        private object GenerateJwtToken(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };
            if (user.AvatarUrl != null)
            {
                claims.Add(new Claim("avatar", user.AvatarUrl));
            }

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
