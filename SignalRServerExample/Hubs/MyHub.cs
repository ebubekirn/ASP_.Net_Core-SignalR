using Microsoft.AspNetCore.SignalR;
using SignalRServerExample.Interfaces;

namespace SignalRServerExample.Hubs
{
	public class MyHub : Hub<IMessageClient>
	{
		static List<string> clients = new();
		//public async Task SendMessageAsync(string message)
		//{
		//	await Clients.All.SendAsync("receiveMessage", message); // receiveMessage isimli bir fonksiyonu client ta tetikle ve client ın bana message olarak gönderdiği parametreyi diğer tüm client lara gönder.
		//}

		public override async Task OnConnectedAsync()
		{
			clients.Add(Context.ConnectionId);
			//await Clients.All.SendAsync("clients", clients);
			//await Clients.All.SendAsync("userJoined", Context.ConnectionId);
			await Clients.All.Clients(clients);
			await Clients.All.UserJoined(Context.ConnectionId);
		}

		public override async Task OnDisconnectedAsync(Exception? exception)
		{
			clients.Remove(Context.ConnectionId);
			//await Clients.All.SendAsync("clients", clients);
			//await Clients.All.SendAsync("userLeaved", Context.ConnectionId);
			await Clients.All.Clients(clients);
			await Clients.All.UserLeaved(Context.ConnectionId);

		}
	}
}
