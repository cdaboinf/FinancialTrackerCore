FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
#EXPOSE 8080
#EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["FinancialTrackerApi/FinancialTrackerApi.csproj", "FinancialTrackerApi/"]
RUN dotnet restore "FinancialTrackerApi/FinancialTrackerApi.csproj"
COPY . .
WORKDIR "/src/FinancialTrackerApi"
RUN dotnet build "FinancialTrackerApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "FinancialTrackerApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://*:6000
ENTRYPOINT ["dotnet", "FinancialTrackerApi.dll"]
