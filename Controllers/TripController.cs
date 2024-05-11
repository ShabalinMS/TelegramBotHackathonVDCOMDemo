using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Sqlite.Update.Internal;
using TelegramBotHackathonVDCOMDemo.DB;
using TelegramBotHackathonVDCOMDemo.Model;

namespace TelegramBotHackathonVDCOMDemo.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TripController : Controller
	{
		[HttpGet]
		public async Task<IEnumerable<TripModel>> Index()
		{
			return await SQLiteHelper.Index();
		}

		[HttpGet("{chatId}")]
		public async Task<IEnumerable<TripModel>> Get(string chatId)
		{
			return await SQLiteHelper.Get(chatId);
		}

		[HttpPost]
		public async Task<TripModel> Post(TripModel trip)
		{
			return await SQLiteHelper.Post(trip);
		}
	}
}
