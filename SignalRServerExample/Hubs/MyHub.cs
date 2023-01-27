using Microsoft.AspNetCore.SignalR;

namespace SignalRServerExample.Hubs
{
	public class MyHub : Hub
	{
		public async Task SendMessageAsync(string message)
		{
			await Clients.All.SendAsync("receiveMessage", message); // receiveMessage isimli bir fonksiyonu client ta tetikle ve client ın bana message olarak gönderdiği parametreyi diğer tüm client lara gönder.
		}
	}
}
