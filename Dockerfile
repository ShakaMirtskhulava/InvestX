# Use the official .NET SDK image for building and testing the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copy everything and restore dependencies
COPY . ./
RUN dotnet restore

# Build the app
RUN dotnet publish GHotel.API/GHotel.API.csproj -c Release -o out

# Use the official .NET runtime image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build-env /app/out .

# Install .NET SDK for running tests
RUN apt-get update && apt-get install -y wget && \
    wget https://dot.net/v1/dotnet-install.sh && \
    chmod +x ./dotnet-install.sh && \
    ./dotnet-install.sh -c 6.0

ENTRYPOINT ["dotnet", "GHotel.API.dll"]
