using System.Linq;
using maelstorm_api;
using maelstorm_api.Models;
using Xunit;

namespace MaelstormApiTests
{
    [Collection("2")]
    public class DialogTests
    {
        [Fact]
        public void GetDialogsAndSendMessageTest()
        {
            var dialogs = Dialogs.GetDialogsAsync().Result;
            Assert.NotNull(dialogs);
            Assert.NotEmpty(dialogs);
            var dialog = dialogs.FirstOrDefault();
            var message = new Message()
            {
                Text = "Hello!"
            };
            dialog.SendMessage(message).Wait();
            Assert.NotEqual(0, message.Id);
        }
    }
}