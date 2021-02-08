using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Dynamic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApiDemo;
using WebMotions.Fake.Authentication.JwtBearer;
using Xunit;

namespace IntegrationTestsDemo
{
    public class AuthorizationControllerTests 
    {
        public class GetTests
        {
            private readonly HttpClient _httpClient;
            private const string _url = "/api/DemoAuthorization/5";
            private const string _email = "anthony.giretti@gmail.com";

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
            public async Task WhenInvokedWithoutAValidToken_ShouldAnswerUnAuthorized()
            {
                // Arrange

                // Act
                var response = await _httpClient.GetAsync(_url);

                // Assert
                response
                .StatusCode
                .Should()
                .Be(HttpStatusCode.Unauthorized);
            }

            [Fact]
            public async Task WhenInvokedWithAValidToken_ShouldAnswerOkWithExpectedData()
            {
                // Arrange
                dynamic data = new System.Dynamic.ExpandoObject();
                data.email = _email;
                _httpClient.SetFakeBearerToken("Anthony Giretti", new string[] {}, (object)data);

                // Act
                var response = await _httpClient.GetAsync(_url);
                var responseContent = await response.Content.ReadAsStringAsync();

                // Assert
                responseContent
                .Should()
                .Be("Hello");

                response
                .StatusCode
                .Should()
                .Be(HttpStatusCode.OK);
            }
        }

        public class PostTests
        {
            private readonly HttpClient _httpClient;
            private readonly StringContent _content;
            private const string _url = "/api/DemoAuthorization/";
            private const string _email = "anthony.giretti@gmail.com";
            private const string _username = "Anthony Giretti";
            private const string _validRole = "SurveyCreator";
            private const string _invalidRole = "Testor";
            private const string _payload = "{\"gender\": \"mister\", \"firstname\": \"anthony\", \"lastname\": \"giretti\", \"sin\": \"510390115\"}";

            public PostTests()
            {
                _content = new StringContent(_payload, Encoding.UTF8, "application/json");
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
            public async Task WhenInvokedWithoutAValidToken_ShouldAnswerUnAuthorized()
            {
                // Arrange

                // Act
                var response = await _httpClient.PostAsync(_url, _content);

                // Assert
                response
                .StatusCode
                .Should()
                .Be(HttpStatusCode.Unauthorized);
            }

            [Fact]
            public async Task WhenInvokedWithAValidTokenAndWithoutProperRole_ShouldAnswerForbidden()
            {
                dynamic data = new System.Dynamic.ExpandoObject();
                data.email = _email;
                data.sub = "Anthony Giretti";
               
               
                _httpClient.SetFakeBearerToken((object)data);

                // Act
                var response = await _httpClient.PostAsync(_url, _content);

                // Assert
                response
                .StatusCode
                .Should()
                .Be(HttpStatusCode.Forbidden);
            }

            [Fact]
            public async Task WhenInvokedWithAValidTokenAndProperRole_ShouldAnswerOk()
            {
                // Arrange
                dynamic data = new System.Dynamic.ExpandoObject();
                data.email = _email;
                _httpClient.SetFakeBearerToken(_username, new[] { _validRole }, (object)data);

                // Act
                var response = await _httpClient.PostAsync(_url, _content);

                // Assert
                response
                .StatusCode
                .Should()
                .Be(HttpStatusCode.OK);
            }
        }
    }
}
