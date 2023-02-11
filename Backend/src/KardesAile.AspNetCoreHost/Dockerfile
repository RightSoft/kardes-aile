FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "KardesAile.AspNetCoreHost/KardesAile.AspNetCoreHost.csproj"
COPY . .
WORKDIR "/src/KardesAile.AspNetCoreHost"
RUN dotnet build "KardesAile.AspNetCoreHost.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "KardesAile.AspNetCoreHost.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KardesAile.AspNetCoreHost.dll"]
