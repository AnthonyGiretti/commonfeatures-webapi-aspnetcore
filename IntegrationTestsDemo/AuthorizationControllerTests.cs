using FluentAssertions;
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
        public async Task WhenGetMethodWithAvalidTokenIsInvoked_GetShouldAnswerCorrectly()
        {
            var response = await _fixture.Client.GetAsync("/api/DemoAuthorization/5");

            var responseContent = await response.Content.ReadAsStringAsync();

            responseContent
            .Should()
            .Be("Hello");
            
        }
    }
}
