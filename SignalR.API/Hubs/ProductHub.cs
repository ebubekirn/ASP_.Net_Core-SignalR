using Microsoft.AspNetCore.SignalR;
using SignalR.API.Models;

namespace SignalR.API.Hubs
{
	public class ProductHub : Hub<IProductHub>
	{
		public async Task SendProduct(Product p)
		{
			await Clients.All.ReceiveProduct(p);
		}
	}
}
