using System.Collections.Generic;
using System.Linq;
using maelstorm_api;
using maelstorm_api.Models;
using Xunit;

namespace MaelstormApiTests
{
    [Collection("2")]
    public class DialogTests
    {
        private List<Dialog> dialogs;

        [Fact]
        public void GetDialogsTest()
        {
            dialogs = Dialogs.GetDialogsAsync().Result;
            Assert.NotNull(dialogs);
            Assert.NotEmpty(dialogs);
        }        
        
        [Fact]
        public void GetDialogsAndSendMessageTest()
        {
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