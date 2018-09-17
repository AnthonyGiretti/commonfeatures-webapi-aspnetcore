using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using FluentValidation;
using WebApiDemo.Models;
using FluentValidation.AspNetCore;
using WebApiDemo.Validators;
using WebApiDemo.Middlewares;
using Serilog;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using System.Reflection;
using System.Collections.Generic;

namespace WebApiDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = "https://login.microsoftonline.com/136544d9-038e-4646-afff-10accb370679";
                options.Audience = "257b6c36-1168-4aac-be93-6f2cd81cec43";
                options.TokenValidationParameters.ValidateLifetime = true;
                options.TokenValidationParameters.ClockSkew = TimeSpan.Zero;
            });

            // Autorization + policies
            services.AddAuthorization(opts =>
            {
                opts.AddPolicy("SurveyCreator", p =>
                {
                    p.RequireClaim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "SurveyCreator");

                });

                opts.AddPolicy("SuperSurveyCreator", p =>
                {
                    p.RequireClaim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "SurveyCreator");
                    p.RequireClaim("groups", "8115e3be-ac7a-4886-a1e6-5b6aaf810a8f");

                });
            });

            // Validators
            services.AddSingleton<IValidator<User>, UserValidator>();

            // override modelstate for fluentvalidation
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            // cache in memory
            services.AddMemoryCache();
            // caching response for middlewares
            services.AddResponseCaching();

            // mvc + validating
            services.AddMvc().AddFluentValidation();

            // documenting
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "Bearer", new string[] { } }
                });
            });

            // profiling
            services.AddMiniProfiler(options =>
                options.RouteBasePath = "/profiler"
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // caching response for middlewares
            app.UseResponseCaching();

            // profiling, url to see last profile check: http://localhost:62258/profiler/results
            app.UseMiniProfiler();

            // documenting
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.RoutePrefix = "api-doc";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                //index.html customizable downloadable here: https://github.com/domaindrivendev/Swashbuckle.AspNetCore/blob/master/src/Swashbuckle.AspNetCore.SwaggerUI/index.html
                c.IndexStream = () => GetType().GetTypeInfo().Assembly.GetManifestResourceStream("WebApiDemo.SwaggerIndex.html");
            });

            // logging
            loggerFactory.AddSerilog();

            // authenticating
            app.UseAuthentication();

            // global caching
            //app.UseMiddleware<CachingMiddleware>();

            // global exception handling
            app.UseMiddleware<CustomExceptionMiddleware>();

            app.UseMvc();
        }
    }
}
