FROM mcr.microsoft.com/dotnet/sdk:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["tests/Directory.Build.props", "tests/Directory.Build.props"]
COPY ["tests/advent-code-2022Tests/advent-code-2022Tests.csproj", "tests/advent-code-2022Tests/"]
COPY ["src/Directory.Build.props", "src/Directory.Build.props"]
COPY ["src/2022-csharp/advent-code-2022.csproj", "src/2022-csharp/"]
COPY ["src/common-csharp/common.csproj", "src/common-csharp/"]
RUN dotnet restore "tests/advent-code-2022Tests/advent-code-2022Tests.csproj"
COPY . .
WORKDIR "/src/tests/advent-code-2022Tests"
RUN dotnet build "advent-code-2022Tests.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "advent-code-2022Tests.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "test", "advent-code-2022Tests.dll"]
