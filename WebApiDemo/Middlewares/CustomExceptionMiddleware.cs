using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebApiDemo.Middlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionMiddleware> _logger;

        public CustomExceptionMiddleware(RequestDelegate next, ILogger<CustomExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            var message = exception.Message;

            response.ContentType = "application/json";

            if (exception is ModelValidationException)
            {
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                var errors = ((ModelValidationException)exception).Errors;

                _logger.LogError(message + " " +  string.Join(',', errors));

                await response.WriteAsync(JsonConvert.SerializeObject(new
                {
                    Code = "00001",
                    Message = message,
                    Errors = errors
                }));

                
            }
            // autre type d'erreurs custom
            /*else if
            {

            }*/
            else
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await response.WriteAsync(JsonConvert.SerializeObject(new
                {
                    Code = "00009",
                    Message = message
                }));
                _logger.LogError(exception, exception.Message);
            }
                 
        }
    }
}
