using TradingBot.Api.Services;
using TradingBot.Domain.Extensions;
using TradingBot.Infrastructure.Infrastruture.Bot;
using TradingBot.Infrastructure.Infrastruture.Transaction;
using TradingBot.Infrastructure.Interfaces.Bot;
using TradingBot.Infrastructure.Interfaces.Common;
using TradingBot.Objects.Bot;
using TradingBot.Objects.Transaction;
using TradingBot.ORM.Extension;
using TradingBot.ORM.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOrmHelper();
builder.Services.RunBotTrader();
//move these to extension later on

builder.Services.AddSingleton<IBotOrder, BotOrderInfrastructure>();
builder.Services.AddSingleton<IRepository<Transactions>, TransactionInfrastruture>();

builder.Services.AddHostedService<BotService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
