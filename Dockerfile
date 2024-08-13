# Use the official .NET 6 SDK Alpine image as the build environment
FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY DockerDotnetConfigPOC/DockerDotnetConfigPOC.csproj ./DockerDotnetConfigPOC/
RUN dotnet restore DockerDotnetConfigPOC/DockerDotnetConfigPOC.csproj

# Copy everything else and build
COPY DockerDotnetConfigPOC/ ./DockerDotnetConfigPOC/
RUN dotnet publish DockerDotnetConfigPOC/DockerDotnetConfigPOC.csproj -c Release -o out

# Build runtime image using .NET runtime Alpine
FROM mcr.microsoft.com/dotnet/runtime:6.0-alpine
WORKDIR /app
COPY --from=build-env /app/out .
COPY Common /app/Common

# Command to run the application
ENTRYPOINT ["dotnet", "DockerDotnetConfigPOC.dll"]
