
FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine AS base
RUN addgroup app && adduser -S -G app app
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT=Development
EXPOSE 5088
EXPOSE 7241

FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["dotnet-rpg.csproj", "dotnet-rpg/"]
RUN dotnet restore "./dotnet-rpg/dotnet-rpg.csproj"
WORKDIR /src/dotnet-rpg
COPY . .
RUN dotnet build "./dotnet-rpg.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./dotnet-rpg.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD [ "dotnet", "dotnet-rpg.dll"]