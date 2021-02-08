using ExpectedObjects;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WebApiDemo;
using WebApiDemo.Models;
using WebMotions.Fake.Authentication.JwtBearer;
using Xunit;

namespace IntegrationTestsDemo
{
    public class DemoExceptionControllerTests
    {
        public class GetTests
        {
            private readonly HttpClient _httpClient;

            public GetTests()
            {
                var host = new HostBuilder()
              .ConfigureWebHost(webBuilder =>
              {
                  webBuilder.UseStartup<StartupTest>();
                  webBuilder
                      .UseTestServer()
                      .ConfigureTestServices(collection =>
                      {
                          collection.AddAuthentication(FakeJwtBearerDefaults.AuthenticationScheme).AddFakeJwtBearer();
                      });
              })
              .Start();

                _httpClient = host.GetTestServer().CreateClient();
            }

            [Fact]
            public async Task WhenRaiseAnException_WebAPIShouldHandleItAndAnswerAProperErrorObjectAndStatusError500()
            {
                // Arrange

                // Act
                var response = await _httpClient.GetAsync("/api/DemoException");
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
