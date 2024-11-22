# Bruk en offisiell .NET runtime som base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Bruk en offisiell .NET SDK som build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Webapp1.csproj", "./"]
RUN dotnet restore "Webapp1.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "Webapp1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Webapp1.csproj" -c Release -o /app/publish

# Bruk base image for å kjøre applikasjonen
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Webapp1.dll"]