using MaelstormApi;
using MaelstormApi.Services.Implementations;
using Xunit;

namespace MaelstormApiTests
{
    [Collection("3")]
    public class SessionTests
    {
        [Fact]
        public void GetSessionsTest()
        {
            var api = ApiClient.Instance;
            var sessions = api.Sessions.GetSessionsAsync().Result;
            Assert.NotNull(sessions);
            Assert.NotEmpty(sessions);
        }
    }
}