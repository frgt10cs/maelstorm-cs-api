using MaelstormApi;
using MaelstormAPI.Services.Implementations;
using MaelstormDTO.Requests;
using Xunit;

namespace MaelstormApiTests
{
    public class AccountTests
    {
        private string[] nicknames = {"chep", "chepa"};
        [Fact(Skip = "Already registered")]
        //[Fact]
        public void RegistrationTest()
        {
            var result = AccountService.RegistrationAsync(new RegistrationRequest()
            {
                Nickname = nicknames[0],
                Email = $"{nicknames[0]}@gmail.com",
                Password = "1234567890",
                ConfirmPassword = "1234567890"
            }).Result;
            Assert.True(result.Ok);
        }
    }
}