FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["oneparalyzer.ProjectManager.Api/oneparalyzer.ProjectManager.Api.csproj", "oneparalyzer.ProjectManager.Api/"]
RUN dotnet restore "oneparalyzer.ProjectManager.Api/oneparalyzer.ProjectManager.Api.csproj"
COPY . .
WORKDIR "/src/oneparalyzer.ProjectManager.Api"
RUN dotnet build "oneparalyzer.ProjectManager.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "oneparalyzer.ProjectManager.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "oneparalyzer.ProjectManager.Api.dll"]
