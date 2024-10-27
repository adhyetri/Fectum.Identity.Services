using Fectum.IdentityService.AuthUser.UI.Context;
using Fectum.IdentityService.AuthUser.UI.IUserInterfaces;
using Fectum.IdentityService.AuthUser.UI.UserServices;
using HttpResponseMessage = Fectum.IdentityService.Model.Models.HttpResponse.HttpResponseMessage<string>;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using IHostEnvironment = Microsoft.Extensions.Hosting.IHostEnvironment;

namespace Fectum.IdentityService.AuthUser.UI
{
    public class Startup(IConfiguration Configuration)
    {
        public IConfiguration Configuration { get; } = Configuration;

        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(option =>
            {
                option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            services.AddTransient<ILoginUser, AuthenticateUserService>();
            services.AddTransient<IRegistration, AuthenticateUserService>();
            services.AddScoped<HttpResponseMessage>();
            services.AddTransient<IActiveUser, ActiveUserService>();
            services.AddTransient<IUserInformation, UsersInformationList>();
            services.AddTransient<ILoginUser, AuthenticateUserService>();
            services.AddTransient<IModifyUser, UsersInformationList>();
            services.AddTransient<IActiveUser, ActiveUserService>();
            services.AddTransient<HttpResponseMessage>();
            services.AddDbContext<IdentityContext>();
            services.AddCors(options =>
            {
                options.AddPolicy("clientApp", option =>
                {
                    option.AllowAnyOrigin();
                    option.AllowAnyHeader();
                    option.AllowAnyMethod();
                });
            });
            /*services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = Configuration["JWT:ISSUER"],
                    ValidAudience = Configuration["JWT:AUDIENCE"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:KEY"]!))
                };
                options.Authority = Configuration[""];
                options.Audience = Configuration[""];
                options.RefreshInterval = TimeSpan.FromMinutes(30);
            });*/

            /*IServiceCollection res = services.AddAuthorization(options =>
            {
                options.AddPolicy("Authorized", options => { options.RequireClaim("Admin", "User"); });
                options.AddPolicy("UnAuthorized", options => { options.RequireClaim("Guest", "User"); });
            });*/

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseCors("clientApp");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(e => { e.MapControllers(); });
            app.UseHttpsRedirection();
        }
    }
}
