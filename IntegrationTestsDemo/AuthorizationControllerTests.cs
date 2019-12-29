using FluentAssertions;
using GST.Fake.Authentication.JwtBearer;
using System.Net;
using System.Net.Http;
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
            private const string _url = "/api/DemoAuthorization/5";
            private const string _email = "anthony.giretti@gmail.com";

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
                var response = await httpClient.GetAsync(_url);

                // Assert
                response
                .StatusCode
                .Should()
                .Be(HttpStatusCode.Unauthorized);
            }

            [Fact(Skip = "Not working")]
            public async Task WhenInvokedWithAValidToken_ShouldAnswerOkWithExpectedData()
            {
                // Arrange
                var httpClient = _fixture.CreateClient();
                dynamic data = new System.Dynamic.ExpandoObject();
                data.email = _email;
                httpClient.SetFakeBearerToken("Anthony Giretti", new string[] {}, (object)data);

                // Act
                var response = await httpClient.GetAsync(_url);
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

        public class PostTests : IClassFixture<WebApiTestsFactory>
        {
            private readonly WebApiTestsFactory _fixture;
            private readonly StringContent _content;
            private const string _url = "/api/DemoAuthorization/";
            private const string _email = "anthony.giretti@gmail.com";
            private const string _username = "Anthony Giretti";
            private const string _validRole = "SurveyCreator";
            private const string _invalidRole = "Testor";
            private const string _payload = "{\"gender\": \"mister\", \"firstname\": \"anthony\", \"lastname\": \"giretti\", \"sin\": \"510390115\"}";

            public PostTests(WebApiTestsFactory fixture)
            {
                _fixture = fixture;
                _content = new StringContent(_payload, Encoding.UTF8, "application/json");
            }

            [Fact]
            public async Task WhenInvokedWithoutAValidToken_ShouldAnswerUnAuthorized()
            {
                // Arrange
                var httpClient = _fixture.CreateClient();

                // Act
                var response = await httpClient.PostAsync(_url, _content);

                // Assert
                response
                .StatusCode
                .Should()
                .Be(HttpStatusCode.Unauthorized);
            }

            [Fact(Skip = "Not working")]
            public async Task WhenInvokedWithAValidTokenAndWithoutProperRole_ShouldAnswerForbidden()
            {
                // Arrange
                var httpClient = _fixture.CreateClient();

                dynamic data = new System.Dynamic.ExpandoObject();
                data.email = _email;
                httpClient.SetFakeBearerToken(_username, new[] { _invalidRole }, (object)data);

                // Act
                var response = await httpClient.PostAsync(_url, _content);

                // Assert
                response
                .StatusCode
                .Should()
                .Be(HttpStatusCode.Forbidden);
            }

            [Fact(Skip = "Not working")]
            public async Task WhenInvokedWithAValidTokenAndProperRole_ShouldAnswerOk()
            {
                // Arrange
                var httpClient = _fixture.CreateClient();
                dynamic data = new System.Dynamic.ExpandoObject();
                data.email = _email;
                httpClient.SetFakeBearerToken(_username, new[] { _validRole }, (object)data);

                // Act
                var response = await httpClient.PostAsync(_url, _content);

                // Assert
                response
                .StatusCode
                .Should()
                .Be(HttpStatusCode.OK);
            }
        }
    }
}
