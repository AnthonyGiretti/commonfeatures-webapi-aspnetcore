using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTestsDemo
{
    public class AuthorizationControllerTests : IClassFixture<TestServerFixture>
    {
        [Fact]
        public async Task WhenGetMethodWithAvalidTokenIsInvoked_GetShouldAnswerCorrectly()
        {
            using (TestServerFixture fixture = new TestServerFixture())
            {
                var response = await fixture.Client.GetAsync("/api/values/5");

                var responseContent = await response.Content.ReadAsStringAsync();

                responseContent
                .Should()
                .Be("value");
            }
        }
    }
}
