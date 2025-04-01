# Stage for runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Stage for build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
# Copy solution file if available
COPY *.sln ./
# Copy project directories
COPY EitanMedical/ EitanMedical/
COPY Base/ Base/

# (Optional) Debug: list all Base.csproj files to verify they exist in the container
RUN find /src -name Base.csproj

# Restore the project(s)
RUN dotnet restore "EitanMedical/EitanMedical.csproj"

WORKDIR "/src/EitanMedical"
RUN dotnet build "EitanMedical.csproj" -c Debug -o /app/build

# Stage for publish
FROM build AS publish
RUN dotnet publish "EitanMedical.csproj" -c Debug -o /app/publish

# Final stage for runtime
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EitanMedical.dll"]
