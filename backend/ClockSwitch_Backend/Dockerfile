#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ClockSwitch_Backend.csproj", "."]
RUN dotnet restore "./ClockSwitch_Backend.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "ClockSwitch_Backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ClockSwitch_Backend.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ClockSwitch_Backend.dll"]