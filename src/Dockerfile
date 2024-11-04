FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

COPY ["./InventoryManagement.csproj", "src/"]
WORKDIR /src
RUN dotnet restore "./InventoryManagement.csproj"
COPY . .
RUN dotnet build "./InventoryManagement.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./InventoryManagement.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 8080
EXPOSE 8081

ENTRYPOINT ["dotnet", "InventoryManagement.dll"]