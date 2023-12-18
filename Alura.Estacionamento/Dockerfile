FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Alura.Estacionamento.csproj", "Alura.Estacionamento/"]
WORKDIR "/src/Alura.Estacionamento"
RUN dotnet restore "Alura.Estacionamento.csproj"
COPY . .
RUN dotnet build "Alura.Estacionamento.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Alura.Estacionamento.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Alura.Estacionamento.dll"]
