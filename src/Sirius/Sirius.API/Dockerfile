FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.sln .
COPY Sirius.API/*.csproj ./Sirius.API/
COPY Modules/Scraping/Sirius.Scraping.Application/*.csproj ./Modules/Scraping/Sirius.Scraping.Application/
COPY Modules/Scraping/Sirius.Scraping.Domain/*.csproj ./Modules/Scraping/Sirius.Scraping.Domain/
COPY Modules/Scraping/Sirius.Scraping.Infrastructure/*.csproj ./Modules/Scraping/Sirius.Scraping.Infrastructure/
COPY Modules/Common/*.csproj ./Modules/Common/
RUN dotnet restore

# Copy everything else and build
COPY Sirius.API/ ./Sirius.API/
COPY Modules/Scraping/Sirius.Scraping.Application/. ./Modules/Scraping/Sirius.Scraping.Application/
COPY Modules/Scraping/Sirius.Scraping.Domain/. ./Modules/Scraping/Sirius.Scraping.Domain/
COPY Modules/Scraping/Sirius.Scraping.Infrastructure/. ./Modules/Scraping/Sirius.Scraping.Infrastructure/
COPY Modules/Common/. ./Modules/Common/

WORKDIR /app/Sirius.API
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS runtime

WORKDIR /app

COPY --from=build /app/Sirius.API/out .

ENTRYPOINT ["dotnet", "Sirius.API.dll"]