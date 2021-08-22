#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS base
WORKDIR /app
COPY ..
RUN dotnet test

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["test/Api_Fatoracao_UnitTests/Api_Fatoracao_UnitTests.csproj", "test/Api_Fatoracao_UnitTests/"]
COPY ["src/Api_Fatoracao/Api_Fatoracao.csproj", "src/Api_Fatoracao/"]
RUN dotnet restore "test/Api_Fatoracao_UnitTests/Api_Fatoracao_UnitTests.csproj"
COPY . .
WORKDIR "/src/test/Api_Fatoracao_UnitTests"
RUN dotnet build "Api_Fatoracao_UnitTests.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Api_Fatoracao_UnitTests.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api_Fatoracao_UnitTests.dll"]