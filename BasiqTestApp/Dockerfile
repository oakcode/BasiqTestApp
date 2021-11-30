#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS base
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT=Development
ENV DOTNET_USE_POLLING_FILE_WATCHER=true  
ENV ASPNETCORE_URLS=http://+:5051  
EXPOSE 5051
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["./BasiqTestApp.csproj", "."]
RUN dotnet restore "./BasiqTestApp.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "BasiqTestApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BasiqTestApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BasiqTestApp.dll", "--key=<API_KEY>", "--loop"]