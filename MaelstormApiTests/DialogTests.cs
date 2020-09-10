using System.Collections.Generic;
using System.Linq;
using MaelstormApi;
using MaelstormApi.Models;
using Xunit;

namespace MaelstormApiTests
{
    [Collection("2")]
    public class DialogTests
    {
        private List<Dialog> dialogs;

        [Fact]
        public void GetDialogTest()
        {
            var dialog = Dialogs.GetDialogAsync(2).Result;
            
            Assert.NotNull(dialog);
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