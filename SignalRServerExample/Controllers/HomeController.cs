using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRServerExample.Business;
using SignalRServerExample.Hubs;

namespace SignalRServerExample.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class HomeController : ControllerBase
	{
		readonly MyBusiness _myBusiness;
		readonly IHubContext<MyHub> _hubContext; // Ekstra metotlarımız yok ise hub ımıza bir sınıf üzerinden ulaşmamıza gerek yoktur. Burada olduğu gibi controller larda da IHubContext interface ini kullanabiliriz.

		public HomeController(MyBusiness myBusiness, IHubContext<MyHub> hubContext)
		{
			_myBusiness = myBusiness;
			_hubContext = hubContext;
		}

		[HttpGet("{message}")]
		public async Task<IActionResult> Index(string message)
		{
			await _myBusiness.SendMessageAsync(message);
			return Ok();
		}
	}
}
