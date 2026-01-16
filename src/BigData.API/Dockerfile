# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution and project files
COPY ["BigData.sln", "./"]
COPY ["src/BigData.API/BigData.API.csproj", "src/BigData.API/"]
COPY ["src/BigData.Application/BigData.Application.csproj", "src/BigData.Application/"]
COPY ["src/BigData.Infrastructure/BigData.Infrastructure.csproj", "src/BigData.Infrastructure/"]
COPY ["src/BigData.Domain/BigData.Domain.csproj", "src/BigData.Domain/"]
COPY ["src/BigData.Core/BigData.Core.csproj", "src/BigData.Core/"]

# Restore dependencies
RUN dotnet restore "src/BigData.API/BigData.API.csproj"

# Copy all source files
COPY . .

# Build the application
WORKDIR "/src/src/BigData.API"
RUN dotnet build "BigData.API.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "BigData.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Create logs directory
RUN mkdir -p /app/logs

EXPOSE 8080
EXPOSE 8081

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "BigData.API.dll"]
