using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Idenitity.Server.Core.Data;
using Idenitity.Server.Core.Models;
using Identity.Server.Manager.Configuration;
using IdentityServer4;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Identity.Server4
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<MyIdentityContext>(options =>
                    options.UseSqlServer("Data Source=.;Initial Catalog=AtlassIDentity; Integrated Security=true;"));
                    services.AddIdentity<MyUsers, MyRoles>()
                               .AddEntityFrameworkStores<MyIdentityContext>();

            var builder = services.AddIdentityServer()
                .AddInMemoryIdentityResources(ConfigIds.Get)
                .AddInMemoryApiResources(ConfigApis.Get)
                .AddInMemoryClients(ConfigClients.Get)
                .AddAspNetIdentity<MyUsers>()
                .AddDeveloperSigningCredential()
                ;


            services.AddAuthentication()

            .AddOpenIdConnect("oidc", "Demo IdentityServer", options =>
            {
                options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                options.SignOutScheme = IdentityServerConstants.SignoutScheme;
                options.SaveTokens = true;

                options.Authority = "https://demo.identityserver.io/";
                options.ClientId = "native.code";
                options.ClientSecret = "secret";
                options.ResponseType = "code";

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = "name",
                    RoleClaimType = "role"
                };
            });
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
                //options.Password


            });

            services.AddSwaggerGen(p =>
            {

                p.SwaggerDoc("Identity", new OpenApiInfo
                {
                }
                );
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();           
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI( p=>
            {
                p.SwaggerEndpoint("/Swagger/Identity/Swagger.json", "Identity API");
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{Controller=Account}/{action=Login}"
                    );
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
