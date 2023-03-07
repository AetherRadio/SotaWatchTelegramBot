# Use the official .NET SDK image as the base image
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Make a working dir
WORKDIR /app

# Copy the .NET project file to the container
COPY . .

# Run the build
RUN dotnet publish -c Release

# Create the final image with the application
FROM mcr.microsoft.com/dotnet/runtime:7.0 AS runtime

WORKDIR /app

COPY --from=build /app/BotApp/bin/Release/net7.0/publish ./

ENTRYPOINT ["dotnet", "BotApp.dll"]
