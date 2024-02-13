FROM mcr.microsoft.com/dotnet/sdk:7.0 as base
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT=Development
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /source
COPY ["src/FoodStock.Api/FoodStock.Api.csproj", "FoodStock.Api/"]
COPY ["src/FoodStock.Infrastructure/FoodStock.Infrastructure.csproj", "FoodStock.Infrastructure/"]
COPY ["src/FoodStock.Application/FoodStock.Application.csproj", "FoodStock.Application/"]
COPY ["src/FoodStock.Core/FoodStock.Core.csproj", "FoodStock.Core/"]
RUN dotnet restore "FoodStock.Api/FoodStock.Api.csproj"
COPY . .
WORKDIR "/source/src/FoodStock.Api"
RUN dotnet build "FoodStock.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FoodStock.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FoodStock.Api.dll"]
