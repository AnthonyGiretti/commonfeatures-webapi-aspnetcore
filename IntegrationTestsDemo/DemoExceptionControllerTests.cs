using FluentAssertions;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WebApiDemo.Models;
using Xunit;

namespace IntegrationTestsDemo
{
    public class DemoExceptionControllerTests : IClassFixture<WebApiTestsFactory>
    {
        private readonly WebApiTestsFactory _factory;

        public DemoExceptionControllerTests(WebApiTestsFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task WhenGetMethodRaiseAnException_WebAPIShouldHandleItandAnswerAProperErrorObjectAndStatusError500()
        {
            // Arrange
            var httpClient = _factory.CreateClient();
            // Act
            var response = await httpClient.GetAsync("/api/DemoException");

            // Assert
            var responseData = await response
            .Content
            .ReadAsAsync<Error>();

            responseData
            .Should()
            .BeEquivalentTo(new Error { Message = "Unhandled error", Errors = new List<string>(), Code = "00009" });

            response.StatusCode
            .Should()
            .Be((int)HttpStatusCode.InternalServerError);

            response
            .Content
            .Headers
            .ContentType
            .MediaType
            .Should()
            .Be("application/json");
        }    
    }
}
