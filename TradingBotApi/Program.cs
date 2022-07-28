using TradingBot.Api.Services;
using TradingBot.Domain.Extensions;
using TradingBot.Infrastructure.Infrastruture.Bot;
using TradingBot.Infrastructure.Infrastruture.Order;
using TradingBot.Infrastructure.Infrastruture.Transaction;
using TradingBot.Infrastructure.Infrastruture.UnitOfWork;
using TradingBot.Infrastructure.Interfaces.Bot;
using TradingBot.Infrastructure.Interfaces.Common;
using TradingBot.Infrastructure.Interfaces.Order;
using TradingBot.Infrastructure.Interfaces.Transaction;
using TradingBot.Infrastructure.Interfaces.UnitOfWork;
using TradingBot.Objects.Bot;
using TradingBot.Objects.Exchange;
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
builder.Services.AddSingleton<ITransaction, TransactionInfrastruture>();
//builder.Services.AddSingleton<IRepository<BotOrder>, BotOrderInfrastructure>();
builder.Services.AddSingleton<IBotOrderTransaction, BotOrderTransactionInfrastructure>();
builder.Services.AddSingleton<IRepository<Symbol>, ExchangeInfrastructure>();
builder.Services.AddSingleton<IOrderInfrastruture, OrderInfrastructure>();

builder.Services.AddSingleton<IUnitOfWork, UnitOfWork>();

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
