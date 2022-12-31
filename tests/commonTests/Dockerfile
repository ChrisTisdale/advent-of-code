FROM mcr.microsoft.com/dotnet/sdk:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["tests/Directory.Build.props", "tests/Directory.Build.props"]
COPY ["tests/commonTests/commonTests.csproj", "tests/commonTests/"]
COPY ["src/Directory.Build.props", "src/Directory.Build.props"]
COPY ["src/common-csharp/common.csproj", "src/common-csharp/"]
RUN dotnet restore "tests/commonTests/commonTests.csproj"
COPY . .
WORKDIR "/src/tests/commonTests"
RUN dotnet build "commonTests.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "commonTests.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "test","commonTests.dll"]
