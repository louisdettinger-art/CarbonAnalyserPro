# 1. Construction de l'app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copie des fichiers projet et restauration
COPY ["CarbonAnalyzer.csproj", "./"]
RUN dotnet restore "CarbonAnalyzer.csproj"

# Copie de tout le code source (dont le dossier wwwroot)
COPY . .
RUN dotnet publish "CarbonAnalyzer.csproj" -c Release -o /app/publish

# 2. Image finale pour lancer l'app
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# --- LA LIGNE MAGIQUE : On force la copie du dossier web ---
COPY --from=build /src/wwwroot ./wwwroot
# -----------------------------------------------------------

# Configuration pour Render
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "CarbonAnalyzer.dll"]