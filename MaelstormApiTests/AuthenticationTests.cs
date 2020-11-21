using MaelstormApi;
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
            var api = ApiClient.Instance;
            var result = api.Api.AuthenticateAsync("chepasa", "1234567890").Result;
            Assert.True(result);
        }
    }
}