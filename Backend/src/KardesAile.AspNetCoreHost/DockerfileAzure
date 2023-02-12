FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
COPY . .
RUN dotnet restore "./Backend/src/KardesAile.AspNetCoreHost/KardesAile.AspNetCoreHost.csproj"
COPY . .
RUN dotnet build "./Backend/src/KardesAile.AspNetCoreHost/KardesAile.AspNetCoreHost.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./Backend/src/KardesAile.AspNetCoreHost/KardesAile.AspNetCoreHost.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KardesAile.AspNetCoreHost.dll"]
