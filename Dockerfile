# Use the official .NET SDK image as the base image
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Make a working dir
WORKDIR /workspace

# Copy the .NET project file to the container
COPY . .

# Run the build using NUKE
RUN ./build.sh

ENTRYPOINT ["./entrypoint.sh"]
