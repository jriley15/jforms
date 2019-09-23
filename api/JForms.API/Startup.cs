using AutoMapper;
using JForms.API.Extensions;
using JForms.Application.Profiles;
using JForms.Application.Services;
using JForms.Data;
using JForms.Data.Entity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;

namespace JForms
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(ValidateModelStateAttribute));
            }).AddNewtonsoftJson(options => { 
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; 
            });

            services.AddAutoMapper(typeof(MappingProfile));

            services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(Configuration.GetConnectionString("postgres")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<DatabaseContext>().
                AddDefaultTokenProviders();

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtKey"])),
                        ValidIssuer = Configuration["JwtIssuer"],
                        ValidateLifetime = true,
                        ValidateAudience = false
                    };
                });

            services.AddHttpClient();

            services.AddCors();

            services.AddTransient<ISubmitService, SubmitService>();
            services.AddTransient<IFormService, FormService>();
            services.AddTransient<IFormFieldService, FormFieldService>();
            services.AddTransient<IFormSnippetservice, FormSnippetService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseCors(policy =>
            {
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
                policy.SetIsOriginAllowed((host) => true);
                policy.AllowCredentials();
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
