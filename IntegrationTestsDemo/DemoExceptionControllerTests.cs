using ExpectedObjects;
using FluentAssertions;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WebApiDemo.Models;
using Xunit;

namespace IntegrationTestsDemo
{
    public class DemoExceptionControllerTests
    {
        public class GetTests : IClassFixture<WebApiTestsFactory>
        {
            private readonly WebApiTestsFactory _factory;

            public GetTests(WebApiTestsFactory factory)
            {
                _factory = factory;
            }

            [Fact]
            public async Task WhenRaiseAnException_WebAPIShouldHandleItAndAnswerAProperErrorObjectAndStatusError500()
            {
                // Arrange
                var httpClient = _factory.CreateClient();

                // Act
                var response = await httpClient.GetAsync("/api/DemoException");
                var responseData = await response
                .Content
                .ReadAsAsync<Error>();

                // Assert
                var expectedError = new Error { Message = "Unhandled error", Errors = new List<string>(), Code = "00009" }.ToExpectedObject();
                expectedError.ShouldEqual(responseData);

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
}
