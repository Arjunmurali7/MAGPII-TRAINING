# Stage 1: Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Force HTTP inside container
ENV ASPNETCORE_URLS=http://+:80

# Stage 2: Build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["task7.csproj", "./"]
RUN dotnet restore "task7.csproj"
COPY . .
RUN dotnet publish "task7.csproj" -c Release -o /app/publish

# Stage 3: Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "task7.dll"]
