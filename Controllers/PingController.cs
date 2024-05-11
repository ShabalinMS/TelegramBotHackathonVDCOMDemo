using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TelegramBotHackathonVDCOMDemo.Constants;

namespace TelegramBotHackathonVDCOMDemo.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PingController : Controller
	{ 
		[HttpGet]
		public async Task<string> Get()
		{
			return "Pong";
		}
	}
}
