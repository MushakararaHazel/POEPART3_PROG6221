
using System.Windows.Controls;

namespace Part3POE.PART2
{
    public class UserInterface
    {
        private readonly ListBox _chatList;

        public UserInterface(ListBox chatList)
        {
            _chatList = chatList;
        }

        public void AddUserMessage(string message)
        {
            _chatList.Items.Add($"👤 You: {message}");
        }

        public void AddBotMessage(string message)
        {
            _chatList.Items.Add($"🤖 Bot: {message}");
        }

        public void AddBotTip(string message)
        {
            _chatList.Items.Add($"💡 Bot Tip: {message}");
        }
    }
}
