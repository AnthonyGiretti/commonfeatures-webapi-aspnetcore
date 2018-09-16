using FluentAssertions;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTestsDemo
{
    public class AuthorizationControllerTests : IClassFixture<TestServerFixture>
    {
        private readonly TestServerFixture _fixture;


        public AuthorizationControllerTests(TestServerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task WhenGetMethodIsInvokedWithoutAValidToken_GetShouldAnswerUnAuthorized()
        {
            var response = await _fixture.Client.GetAsync("/api/DemoAuthorization/5");

            //var responseContent = await response.Content.ReadAsStringAsync();

            response
            .StatusCode
            .Should()
            .Be(HttpStatusCode.Unauthorized);
            
        }

        [Fact]
        public async Task WhenGetMethodIsInvokedWithAValidToken_GetShouldAnswerOkWithExpectedData()
        {
            _fixture.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "put a token here");
            var response = await _fixture.Client.GetAsync("/api/DemoAuthorization/5");

            var responseContent = await response.Content.ReadAsStringAsync();

            responseContent
            .Should()
            .Be("Hello");

        }
    }
}
