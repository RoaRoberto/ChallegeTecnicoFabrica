#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Api_Fabrica/Api_Fabrica.csproj", "Api_Fabrica/"]
RUN dotnet restore "Api_Fabrica/Api_Fabrica.csproj"
COPY . .
WORKDIR "/src/Api_Fabrica"
RUN dotnet build "Api_Fabrica.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Api_Fabrica.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api_Fabrica.dll"]