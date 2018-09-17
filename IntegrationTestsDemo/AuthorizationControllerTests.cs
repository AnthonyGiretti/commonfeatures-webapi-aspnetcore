using FluentAssertions;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTestsDemo
{
    public class AuthorizationControllerTests //: IClassFixture<TestServerFixture>
    {
       // private readonly TestServerFixture _fixture;


        //public AuthorizationControllerTests(TestServerFixture fixture)
        //{
        //    _fixture = fixture;
        //}

        [Fact]
        public async Task WhenGetMethodIsInvokedWithoutAValidToken_GetShouldAnswerUnAuthorized()
        {
            using (TestServerFixture fixture = new TestServerFixture())
            {
                var response = await fixture.Client.GetAsync("/api/DemoAuthorization/5");

                response
                .StatusCode
                .Should()
                .Be(HttpStatusCode.Unauthorized);
            }

        }

        [Fact]
        public async Task WhenGetMethodIsInvokedWithAValidToken_GetShouldAnswerOkWithExpectedData()
        {
            using (TestServerFixture fixture = new TestServerFixture())
            {
                // Implement here your token obtaining
                fixture.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "JWT here");
                var response = await fixture.Client.GetAsync("/api/DemoAuthorization/5");

                var responseContent = await response.Content.ReadAsStringAsync();

                responseContent
                .Should()
                .Be("Hello");
            }

        }
    }
}
