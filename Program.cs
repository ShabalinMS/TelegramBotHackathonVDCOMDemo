using TelegramBotHackathonVDCOMDemo.Constants;
using TelegramBotHackathonVDCOMDemo.DB;
using TelegramBotHackathonVDCOMDemo.Handler.Telegram;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
ConfigurationApp.SetSettings = app;
BotHelper.Launch();
SQLiteHelper.InitDB();
app.Run();
