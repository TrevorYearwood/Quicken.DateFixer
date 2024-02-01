#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Quicken.DateFixer.MinApi/Quicken.DateFixer.MinApi.csproj", "Quicken.DateFixer.MinApi/"]
COPY ["Quicken.DateFixer.Services/Quicken.DateFixer.Services.csproj", "Quicken.DateFixer.Services/"]
COPY ["Quicken.DateFixer.Domain/Quicken.DateFixer.Domain.csproj", "Quicken.DateFixer.Domain/"]
RUN dotnet restore "./Quicken.DateFixer.MinApi/./Quicken.DateFixer.MinApi.csproj"
COPY . .
WORKDIR "/src/Quicken.DateFixer.MinApi"
RUN dotnet build "./Quicken.DateFixer.MinApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Quicken.DateFixer.MinApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Quicken.DateFixer.MinApi.dll"]