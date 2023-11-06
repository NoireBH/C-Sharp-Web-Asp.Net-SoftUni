namespace _02.SimpleChatApp.Models.Message
{
	public class ChatViewModel
	{
        public ChatViewModel()
        {
            Messages = new List<MessageViewModel>();
        }

        public MessageViewModel CurrentMessage { get; set; } = null!;

		public List<MessageViewModel> Messages { get; set; } = null!;
	}
}
