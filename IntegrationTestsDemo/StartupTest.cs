using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using GST.Fake.Authentication.JwtBearer;
using ImpromptuInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using WebApiDemo.AuthorizationHandlers;
using WebApiDemo.Database;
using WebApiDemo.Extensions;
using WebApiDemo.HttpClients;
using WebApiDemo.Middlewares;
using WebApiDemo.Models;
using WebApiDemo.Providers;
using WebApiDemo.Repositories;
using WebApiDemo.Services;
using WebApiDemo.Services.Tenants;
using WebApiDemo.Validators;

namespace WebApiDemo
{
    public class StartupTest
    {
        public StartupTest(IConfiguration configuration)
        {
            // Init Serilog configuration
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
            Configuration = configuration;

            TypesToRegister = Assembly.Load("WebApiDemo")
                                      .GetTypes()
                                      .Where(x => !string.IsNullOrEmpty(x.Namespace))
                                      .Where(x => x.IsClass)
                                      .Where(x => x.Namespace.StartsWith("WebApiDemo.Services.Tenants")).ToList();
        }

        public IConfiguration Configuration { get; }

        public List<Type> TypesToRegister { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region DemoAuthentication
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = FakeJwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = FakeJwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = FakeJwtBearerDefaults.AuthenticationScheme;
            }).AddFakeJwtBearer();
            #endregion

            #region DemoAuthorization
            services.AddAuthorization(opts =>
            {
                opts.AddPolicy("SurveyCreator", p =>
                {
                    // Using value text for demo show, else use enum : ClaimTypes.Role
                    p.RequireClaim("role", "SurveyCreator");

                });

                opts.AddPolicy("SuperSurveyCreator", p =>
                {
                    p.RequireClaim("role", "SuperSurveyCreator");
                });
            });

            // Authorization handlers
            services.AddSingleton<IAuthorizationHandler, SuperSurveyCreatorAutorizationHandler>();
            #endregion

            #region Demo Validator
            services.AddSingleton<IValidator<User>, UserValidator>();
            #endregion

            #region DemoConfig
            var config = new {
                ConnectionString = Configuration.GetSection("MySecretConnectionString").Value // <-- from Azure Keyvault
            }
            .ActLike<IConfig>();

            services.AddScoped<IMyRepository>(c =>
            {
                return new MyRepository(config);
            });
            #endregion

            #region DemoCRUD EF + ORMLite
            //services.AddScoped<ICountryRepository>(c =>
            //{
            //    return new OrmLiteCountryRepository(config);
            //});
            services.AddDbContext<DemoDbContext>(options => options.UseSqlServer(config.ConnectionString));
            services.AddScoped<ICountryRepository, EFCountryRepository>();
            #endregion

            #region DemoCache
            services.AddMemoryCache();
            #endregion

            #region DemoResponseCaching
            // caching response for middlewares
            services.AddResponseCaching();
            #endregion

            #region DemoMapping Automapper
            services.AddAutoMapper(Assembly.Load("WebApiDemo"));
            #endregion

            #region MVC + FluentValidation
            services.AddControllers().SetCompatibilityVersion(CompatibilityVersion.Version_3_0).AddFluentValidation();
            #endregion

            #region override modelstate for fluentvalidation
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
            #endregion

            #region DemoHttpClient
            services.AddHttpClient<IDataClient, DataClient>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:56190/api/");
            });
            //.AddPolicyHandlers("PolicyConfig", null, Configuration);

            services.AddHttpClient<IStreamingClient, StreamingClient>(client =>
            {
                client.BaseAddress = new Uri("https://anthonygiretti.blob.core.windows.net/videos/");
            });
            #endregion

            #region DemoCompression
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
                options.EnableForHttps = true;
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/x-sql", "video/mp4" });
            });

            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });
            #endregion

            #region DemoApiVersionning
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });
            #endregion

            #region DemoMultiTenant
            // Classes to register
            TypesToRegister.ForEach(x => services.AddScoped(x));
            // Multitenant interface with its related classes
            services.AddScopedDynamic<ITenantService>(TypesToRegister);

            // Global Service provider
            services.AddScoped(typeof(IServicesProvider<>), typeof(ServicesProvider<>));
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region Logging
            //loggerFactory.AddSerilog();
            #endregion

            app.UseRouting();

            #region Authenticating & authorization
            app.UseAuthentication();
            app.UseAuthorization();
            #endregion

            #region Global caching middleware
            app.UseMiddleware<CachingMiddleware>();
            #endregion

            #region Global exception handling middleware
            app.UseMiddleware<CustomExceptionMiddleware>();
            #endregion

            #region Compression
            //app.UseResponseCompression();
            #endregion

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}