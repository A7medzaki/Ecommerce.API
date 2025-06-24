# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy everything
COPY . ./

# Restore dependencies
RUN dotnet restore

# Publish the app
RUN dotnet publish -c Release -o /app/out

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copy build output
COPY --from=build /app/out .

# Tell ASP.NET to listen on port 8080 (required for Railway)
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# Start the app
ENTRYPOINT ["dotnet", "Ecommerce.API.dll"]