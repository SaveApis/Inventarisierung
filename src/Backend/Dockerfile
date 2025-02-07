FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

RUN --mount=type=secret,id=github_user \
    --mount=type=secret,id=github_token \
    --mount=type=secret,id=hangfire_ace_url \
    --mount=type=secret,id=hangfire_ace_user \
    --mount=type=secret,id=hangfire_ace_token \
    --mount=type=secret,id=hangfire_pro_url \
    --mount=type=secret,id=hangfire_pro_user \
    --mount=type=secret,id=hangfire_pro_token \
    dotnet nuget add source https://nuget.pkg.github.com/SaveApis/index.json --name GitHub --username $(cat /run/secrets/github_user) --password $(cat /run/secrets/github_token) --store-password-in-clear-text && \
    dotnet nuget add source $(cat /run/secrets/hangfire_ace_url) --name HangfireAce --username $(cat /run/secrets/hangfire_ace_user) --password $(cat /run/secrets/hangfire_ace_token) --store-password-in-clear-text && \
    dotnet nuget add source $(cat /run/secrets/hangfire_pro_url) --name HangfirePro --username $(cat /run/secrets/hangfire_pro_user) --password $(cat /run/secrets/hangfire_pro_token) --store-password-in-clear-text

ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Backend/Backend.csproj", "src/Backend/"]
RUN dotnet restore "src/Backend/Backend.csproj"
COPY . .
WORKDIR "/src/src/Backend"
RUN dotnet build "Backend.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Backend.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Backend.dll"]
