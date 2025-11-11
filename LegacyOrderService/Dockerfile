# ------------------------
# BUILD STAGE
# ------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build + publish
COPY . .
RUN dotnet publish -c Release -o /out

# ------------------------
# RUNTIME STAGE
# ------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copy the published output
COPY --from=build /out .

# Expose port
EXPOSE 8080

ENTRYPOINT ["dotnet", "LegacyOrderService.dll"]
