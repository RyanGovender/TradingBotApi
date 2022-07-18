#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TradingBotApi/TradingBot.Api.csproj", "TradingBotApi/"]
COPY ["TradingBot.Objects/TradingBot.Objects.csproj", "TradingBot.Objects/"]
COPY ["TradingBot.Infrastructure/TradingBot.Infrastructure.csproj", "TradingBot.Infrastructure/"]
COPY ["TradingBot.Domain/TradingBot.Domain.csproj", "TradingBot.Domain/"]
RUN dotnet restore "TradingBotApi/TradingBot.Api.csproj"
COPY . .
WORKDIR "/src/TradingBotApi"
RUN dotnet build "TradingBot.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TradingBot.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TradingBot.Api.dll"]