using MaelstormApi;
using Xunit;

namespace MaelstormApiTests
{
    [Collection("3")]
    public class SessionTests
    {
        [Fact]
        public void GetSessionsTest()
        {
            var sessions = Sessions.GetSessionsAsync().Result;
            Assert.NotNull(sessions);
            Assert.NotEmpty(sessions);
        }
    }
}