using FluentAssertions;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebApiDemo.Models;
using Xunit;

namespace IntegrationTestsDemo
{
    public class DemoExceptionControllerTests
    {
        [Fact]
        public async Task WhenGetMethodRaiseAnException_WebAPIShouldHandleItandAnswerAProperErrorObjectAndStatusError500()
        {
            //Arrange
            using (TestServerFixture fixture = new TestServerFixture())
            {
                // Act
                var response = await fixture.Client.GetAsync("/api/DemoException");

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
}
