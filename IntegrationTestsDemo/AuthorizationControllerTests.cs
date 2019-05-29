using FluentAssertions;
using GST.Fake.Authentication.JwtBearer;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTestsDemo
{
    public class AuthorizationControllerTests 
    {
        public class GetTests : IClassFixture<WebApiTestsFactory>
        {
            private readonly WebApiTestsFactory _fixture;

            public GetTests (WebApiTestsFactory fixture)
            {
                _fixture = fixture;
            }

            [Fact]
            public async Task WhenInvokedWithoutAValidToken_ShouldAnswerUnAuthorized()
            {
                // Arrange
                var httpClient = _fixture.CreateClient();

                // Act
                var response = await httpClient.GetAsync("/api/DemoAuthorization/5");

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
                var httpClient = _fixture.CreateClient();

                //dynamic data = new System.Dynamic.ExpandoObject();
                //data.email = "anthony.giretti@gmail.com";
                //data.sub = "Anthony Giretti";
                //data.roles = new[] { "SurveyCreator" };
                //var serializedToken = JsonConvert.SerializeObject((object)data);
                //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("FakeBearer", serializedToken);

                //httpClient.SetFakeBearerToken("Anthony Giretti", new[] { "SurveyCreator" }, (object)data);

                var response = await httpClient.GetAsync("/api/DemoAuthorization/5");

                // Act
                var responseContent = await response.Content.ReadAsStringAsync();

                // Assert
                responseContent
                .Should()
                .Be("Hello");
            }
        }

        public class PostTests : IClassFixture<WebApiTestsFactory>
        {
            private readonly WebApiTestsFactory _fixture;

            public PostTests(WebApiTestsFactory fixture)
            {
                _fixture = fixture;
            }

            [Fact]
            public async Task WhenInvokedWithoutAValidToken_ShouldAnswerUnAuthorized()
            {
                // Arrange
                var httpClient = _fixture.CreateClient();

                // Act
                var response = await httpClient.PostAsync("/api/DemoAuthorization/", new StringContent("hello"));

                // Assert
                response
                .StatusCode
                .Should()
                .Be(HttpStatusCode.Unauthorized);
            }
        }
    }
}
