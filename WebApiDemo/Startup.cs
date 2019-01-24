using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Reflection;
using WebApiDemo.Middlewares;
using WebApiDemo.Models;
using WebApiDemo.Validators;
using System.Linq;
using System.Security.Claims;
using WebApiDemo.AuthorizationHandlers;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using WebApiDemo.Repositories;
using ImpromptuInterface;
using WebApiDemo.Services;
using WebApiDemo.Services.Tenants;

namespace WebApiDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            // Init Serilog configuration
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
            Configuration = configuration;
            TypesToRegister = Assembly.Load("WebApiDemo").GetTypes()
                                .Where(x => !string.IsNullOrEmpty(x.Namespace))
                                .Where(x => x.IsClass)
                                .Where(x => x.Namespace.StartsWith("WebApiDemo.Services.Tenants")).ToList();
        }

        public IConfiguration Configuration { get; }

        public List<Type> TypesToRegister { get; }

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
                    // Using value text for demo show, else use enum : ClaimTypes.Role
                    p.RequireClaim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "SurveyCreator");

                });

                opts.AddPolicy("SuperSurveyCreator", p =>
                {
                    // Using value text for demo show, else use enum : ClaimTypes.Role
                    //p.RequireClaim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "SurveyCreator");
                    //p.RequireClaim("groups", "8115e3be-ac7a-4886-a1e6-5b6aaf810a8f");
                    p.Requirements.Add(new SuperSurveyCreatorRequirement("SurveyCreator", "8115e3be-ac7a-4886-a1e6-5b6aaf810a8f"));
                });
            });

            // Authorization handlers
            services.AddSingleton<IAuthorizationHandler, SuperSurveyCreatorAutorizationHandler>();

            // Validators
            services.AddSingleton<IValidator<User>, UserValidator>();

            // Repositories
             services.AddScoped<IMyRepository>(c =>
            {
                var config = new { ConnectionString = c.GetService<IConfiguration>()["ConnectionStrings:MyDatabase"] } ;
                return new MyRepository(config.ActLike<IConfig>());
            });


            // cache in memory
            services.AddMemoryCache();

            // caching response for middlewares
            services.AddResponseCaching();

            // Automapper
            services.AddAutoMapper();

            // mvc + validating
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddFluentValidation();

            // override modelstate for fluentvalidation
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    var errors = context.ModelState.Values.SelectMany(x => x.Errors.Select(p => p.ErrorMessage)).ToList();
                    var result = new
                    {
                        Code = "00009",
                        Message = "Validation errors",
                        Errors = errors
                    };
                    return new BadRequestObjectResult(result);
                };
            });

            // documenting
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API + profiler integrated on top left page", Version = "v1" });
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

            // Tenant Services
            TypesToRegister.ForEach(x => services.AddScoped(x));
            AddScopedDynamic<ITenantService>(services);
            /*
            services.AddScoped<Func<string, ITenantService>>(serviceProvider => tenant =>
            {
                var type = TypesToRegister.Where(y => y.GetInterfaces().Contains(typeof(ITenantService)))
                                          .Where(x => x.Name.Contains(tenant))
                                          .FirstOrDefault();
                if (null == type)
                    throw new KeyNotFoundException("Aucune instance trouvée pour le tenant fournit.");

                return (ITenantService)serviceProvider.GetService(type);
            });*/
        }

        private void AddScopedDynamic<T>(IServiceCollection services)
        {
            services.AddScoped<Func<string, T>>(serviceProvider => tenant =>
            {
                var type = TypesToRegister.Where(y => y.GetInterfaces().Contains(typeof(T)))
                                          .Where(x => x.Name.Contains(tenant))
                                          .FirstOrDefault();
                if (null == type)
                    throw new KeyNotFoundException("Aucune instance trouvée pour le tenant fournit.");

                return (T)serviceProvider.GetService(type);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // caching all response that resturn 200 ok
            //app.UseResponseCaching();

            // profiling, url to see last profile check: http://localhost:62258/profiler/results
            app.UseMiniProfiler();

            // documenting
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "api-doc";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                // index.html customizable downloadable here: https://github.com/domaindrivendev/Swashbuckle.AspNetCore/blob/master/src/Swashbuckle.AspNetCore.SwaggerUI/index.html
                // this custom html has miniprofiler integration
                c.IndexStream = () => GetType().GetTypeInfo().Assembly.GetManifestResourceStream("WebApiDemo.SwaggerIndex.html");
            });

            // logging
            loggerFactory.AddSerilog();

            // authenticating
            app.UseAuthentication();

            // global caching
            app.UseMiddleware<CachingMiddleware>();

            // global exception handling
            app.UseMiddleware<CustomExceptionMiddleware>();

            // mini profiler 
            //app.UseMiddleware<MiniProfilerMiddleware>();

            app.UseMvc();
        }
    }
}
