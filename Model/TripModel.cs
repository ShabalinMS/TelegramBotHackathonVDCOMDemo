using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace TelegramBotHackathonVDCOMDemo.Model
{
	public class TripModel
	{
		public string Id { get; set; } = null!;

		[JsonProperty("city")]
		public string City { get; set; } = null!;

		[JsonProperty("description")]
		public string? Description { get; set; }

		public string ChatId { get; set; } = null!;
	}
}
