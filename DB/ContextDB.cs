using Microsoft.EntityFrameworkCore;
using TelegramBotHackathonVDCOMDemo.Constants;
using TelegramBotHackathonVDCOMDemo.Model;

namespace TelegramBotHackathonVDCOMDemo.DB
{
	public class ContextDB : DbContext
	{
		public DbSet<ContactModel> Contact { get; set; } = null!;
		public DbSet<TripModel> Trip { get; set; } = null!;

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite(ConfigurationApp.ConnectionStringDB);
		}
	}
}
