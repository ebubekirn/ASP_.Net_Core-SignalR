using SignalR.API.Models;

namespace SignalR.API.Hubs
{
	public interface IProductHub
	{
		Task ReceiveProduct(Product p);
	}
}
