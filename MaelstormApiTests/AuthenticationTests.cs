using System.ComponentModel;
using maelstorm_api;
using Xunit;

[assembly: TestCollectionOrderer("MaelstormApiTests.DisplayNameOrderer", "MaelstormApiTests")]
[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace MaelstormApiTests
{
    [Collection("1")]
    public class AuthenticationTests
    {
        [Fact]
        public void AuthenticationTest()
        {
            var result = Client.AuthenticateAsync("chep", "1234567890").Result;
            Assert.True(result);
        }
    }
}