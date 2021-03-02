using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using YoutubeAuthentication.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace YoutubeAuthentication
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
            // services.AddControllers();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {

                options.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "Fiver.Security.Bearer",
                    ValidAudience = "Fiver.Security.Bearer",
                    IssuerSigningKey =
                            JwtSecurityKey.Create("fiver-security-key")
                };

                options.Events = new JwtBearerEvents {
                    OnAuthenticationFailed = context => {
                        Console.WriteLine("OnAuthenticationFailed :" + context.Exception.Message);
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context => {
                        Console.WriteLine("OnTokenValidated :" + context.SecurityToken);
                        return Task.CompletedTask;
                    },
                };
            });


            services.AddAuthorization(options => {
                options.AddPolicy("Member", policy => policy.RequireClaim("MembershipId"));
            });


            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

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
