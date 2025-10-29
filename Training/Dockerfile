# Use the official .NET 6 SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Set the working directory
WORKDIR /src

# Copy the project file and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the entire source code
COPY . ./

# Build the application
RUN dotnet publish -c Release -o /app/publish

# Use the official .NET 6 runtime image for running
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime

# Set the working directory
WORKDIR /app

# Install curl for health checks
RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/*

# Copy the published application
COPY --from=build /app/publish .

# Create the Files directory for file uploads
# RUN mkdir -p /app/Files

# Expose the port
EXPOSE 80

# Set the entry point
ENTRYPOINT ["dotnet", "Training.dll"]
