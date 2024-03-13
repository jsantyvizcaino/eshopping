#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Catalogo/Catalogo.API/Catalogo.API.csproj", "Services/Catalogo/Catalogo.API/"]
COPY ["Catalogo/Catalogo.Application/Catalogo.Application.csproj", "Services/Catalogo/Catalogo.Application/"]
COPY ["Catalogo/Catalogo.Core/Catalogo.Core.csproj", "Services/Catalogo/Catalogo.Core/"]
COPY ["Catalogo/Catalogo.Infrestructure/Catalogo.Infrestructure.csproj", "Services/Catalogo/Catalogo.Infrestructure/"]

RUN dotnet restore "Services/Catalogo/Catalogo.API/Catalogo.API.csproj"
COPY . .
WORKDIR "/src/Catalogo/Catalogo.API"
RUN dotnet build "Catalogo.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Catalogo.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalogo.API.dll"]