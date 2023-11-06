using _02.SimpleChatApp.Models.Message;
using Microsoft.AspNetCore.Mvc;

namespace _02.SimpleChatApp.Controllers
{
	public class ChatController : Controller
	{
		private static readonly List<KeyValuePair<string,string>> messages = new List<KeyValuePair<string, string>>();

		public IActionResult Show()
		{
			if (messages.Count < 1)
			{
				return View(new ChatViewModel());
			}

			var chatModel = new ChatViewModel()
			{
				Messages = messages
				.Select(m => new MessageViewModel()
				{
					Sender = m.Key,
					Message = m.Value
				})
				.ToList()

			};

			return View(chatModel);
		}

		[HttpPost]
		public IActionResult Send(ChatViewModel chatViewModel)
		{
			if (!ModelState.IsValid)
			{
				return RedirectToAction("Show");
			}

			var newMessage = chatViewModel.CurrentMessage;

			messages.Add(new KeyValuePair<string,string>(newMessage.Sender, newMessage.Message));

			return RedirectToAction("Show");
			
		}
	}
}
