# ---------- Build Stage ----------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Install Node.js
RUN apt-get update \
 && apt-get install -y curl \
 && curl -fsSL https://deb.nodesource.com/setup_20.x | bash - \
 && apt-get install -y nodejs

WORKDIR /src

COPY . .

# Build Angular
WORKDIR /src/Portfolio/ClientApp

RUN npm install
RUN npm run build

# Publish .NET
WORKDIR /src/Portfolio

RUN dotnet publish -c Release -o /app/publish

# ---------- Runtime Stage ----------
FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:10000

EXPOSE 10000

ENTRYPOINT ["dotnet", "Portfolio.dll"]