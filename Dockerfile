# 1. On utilise l'image officielle Microsoft pour CONSTRUIRE l'app
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# On copie le projet et on télécharge les dépendances
COPY ["CarbonAnalyzer.csproj", "./"]
RUN dotnet restore "CarbonAnalyzer.csproj"

# On copie tout le reste du code et on construit
COPY . .
RUN dotnet publish "CarbonAnalyzer.csproj" -c Release -o /app/publish

# 2. On crée l'image FINALE (plus légère) pour lancer l'app
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/publish .

# Configuration pour Render (Port 8080)
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# La commande de démarrage
ENTRYPOINT ["dotnet", "CarbonAnalyzer.dll"]