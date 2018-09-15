using Microsoft.AspNetCore.Http;
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

        public CustomExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
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
            var message = "Unexpected Error";
            var code = (int)HttpStatusCode.InternalServerError;

            response.ContentType = "application/json";

            if (exception is ModelValidationException)
            {
                
                response.StatusCode = code;
                message = "Validation errors";
                await response.WriteAsync(JsonConvert.SerializeObject(new
                {
                    Message = message,
                    Errors = ((ModelValidationException)exception).Errors
                }));

                // autre type d'erreurs custom
                /*else if
                {

                }*/
            }
                 
        }
    }
}
