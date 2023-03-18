FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Api/WeatherApp.Api.csproj", "Api/"]
COPY ["Client/WeatherApp.Client.csproj", "Client/"]
COPY ["Shared/WeatherApp.Shared.csproj", "Shared/"]
RUN dotnet restore "Api/WeatherApp.Api.csproj"
COPY . .
WORKDIR "/src/Api"
RUN dotnet build "WeatherApp.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WeatherApp.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WeatherApp.Api.dll"]