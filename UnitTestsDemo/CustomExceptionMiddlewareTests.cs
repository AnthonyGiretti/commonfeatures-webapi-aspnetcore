using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using WebApiDemo;
using WebApiDemo.Middlewares;
using Xunit;

namespace UnitTestsDemo
{
    public class CustomExceptionMiddlewareTests
    {
        [Fact]
        public async Task WhenACustomExceptionIsRaised_CustomExceptionMiddlewareShouldHandleItToCustomErrorResponse()
        {
            // Arrange
            var loggerMock = Substitute.For<ILogger<CustomExceptionMiddleware>>();
            var middleware = new CustomExceptionMiddleware((innerHttpContext) =>
            {
                throw new ModelValidationException(new List<string>() { "Error1, Error2" });
            }, loggerMock);

            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();

            //Act
            await middleware.Invoke(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(context.Response.Body);
            var streamText = reader.ReadToEnd();
            var objResponse = JsonConvert.DeserializeObject<dynamic>(streamText);

            //Assert
            //objResponse
            //.Should()
            //.BeEquivalentTo(new { Message = "Validation errors", Errors = new List<string>() { "Error1, Error2" } });

            context.Response.StatusCode
            .Should()
            .Be((int)HttpStatusCode.BadRequest);
        }
    }
}
