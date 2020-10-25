#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["BookingCinema/BookingCinema.csproj", "BookingCinema/"]
RUN dotnet restore "BookingCinema/BookingCinema.csproj"
COPY . .
WORKDIR "/src/BookingCinema"
RUN dotnet build "BookingCinema.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BookingCinema.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookingCinema.dll"]